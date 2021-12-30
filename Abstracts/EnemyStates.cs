using GameSpace.EntityManaging;
using GameSpace.Enums;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.Abstracts
{
    public abstract class EnemyStates : IMobState
    {
        public ISprite StateSprite { get; set; }
        protected internal Enemy enemy;

        protected EnemyStates(Enemy enemy)
        {
            this.enemy = enemy;
        }

        public virtual void Draw(SpriteBatch spritebatch, Vector2 position)
        {
            StateSprite.Draw(spritebatch, position);
        }

        public virtual void Update(GameTime gametime)
        {
            StateSprite.Update(gametime);
            UpdatePosition(enemy.Position, gametime);
            UpdateCollisionBox(enemy.Position);
            UpdateSpeed();
        }

        public virtual void DrawBoundingBox(SpriteBatch spritebatch, Rectangle collisionBox)
        {
            StateSprite.DrawBoundary(spritebatch, collisionBox);
        }

        public virtual void Trigger()
        {
            //polymorphically change behavior when triggered 
        }

        internal virtual void UpdatePosition(Vector2 location, GameTime gametime)
        {
            enemy.Velocity += enemy.Acceleration * (float)gametime.ElapsedGameTime.TotalSeconds;
            enemy.Position += enemy.Velocity * (float)gametime.ElapsedGameTime.TotalSeconds;
        }

        internal virtual void UpdateCollisionBox(Vector2 location)
        {
            enemy.CollisionBox = new Rectangle((int)location.X, (int)location.Y,
                StateSprite.Texture.Width * 2 / StateSprite.GetTotalFrames(), StateSprite.Texture.Height * 2);

            enemy.ExpandedCollisionBox = new Rectangle((int)location.X, (int)location.Y,
                StateSprite.Texture.Width * 2 / StateSprite.GetTotalFrames(), (StateSprite.Texture.Height * 2) + 4);
        }

        internal virtual void UpdateSpeed()
        {
            if (EnemyCollisionHandler.GetInstance().IsGoingToFall(enemy))
            {
                enemy.Acceleration = new Vector2(0, 400);
            }

            else
            {
                enemy.Acceleration = new Vector2(0, 0);
                if (enemy.Direction == (int)MarioDirection.RIGHT)
                {
                    enemy.Velocity = new Vector2(65, 0);
                }

                else if (enemy.Direction == (int)MarioDirection.LEFT)
                {
                    enemy.Velocity = new Vector2(-65, 0);
                }
            }
        }

        protected internal void DestoryCollisionBox()
        {
            enemy.CollisionBox = new Rectangle(0, 0, 0, 0);
            enemy.ExpandedCollisionBox = new Rectangle(0, 0, 0, 0);
        }

        protected internal void HaltAllMotion()
        {
            enemy.Velocity = new Vector2(0, 0);
            enemy.Acceleration = new Vector2(0, 0);
        }
    }
}
