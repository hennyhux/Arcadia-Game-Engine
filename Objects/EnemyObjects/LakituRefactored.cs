using GameSpace.Abstracts;
using GameSpace.EntityManaging;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.GameObjects.EnemyObjects;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.Objects.EnemyObjects
{
    public class LakituRefactored : Enemy
    {
        public LakituRefactored(Vector2 initalPosition)
        {
            state = new LakituIdleState(this);
            Position = initalPosition;
            //drawBox = ((Mario)FinderHandler.GetInstance().FindItem((int)AvatarID.MARIO)).ReturnDrawCollisionBoxes();
            ObjectID = (int)EnemyID.LAKITU;
            Direction = (int)MarioDirection.LEFT;
        }

    }

    public abstract class LakituState : IMobState
    {
        public ISprite StateSprite { get; set; }
        protected internal LakituRefactored enemy;

        protected LakituState(LakituRefactored enemy)
        {
            this.enemy = enemy;
        }

        public virtual void Draw(SpriteBatch spritebatch, Vector2 position)
        {
            StateSprite.Draw(spritebatch, position);
        }

        public virtual void DrawBoundingBox(SpriteBatch spritebatch, Rectangle collisionBox)
        {
            StateSprite.DrawBoundary(spritebatch, collisionBox);
        }

        public abstract void Trigger();

        public virtual void Update(GameTime gametime)
        {
            StateSprite.Update(gametime);
            UpdatePosition(enemy.Position, gametime);
            UpdateCollisionBox(enemy.Position);
            UpdateSpeed();
        }

        internal virtual void UpdatePosition(Vector2 location, GameTime gametime)
        {
            enemy.Velocity += enemy.Acceleration * (float) gametime.ElapsedGameTime.TotalSeconds;
            enemy.Position += enemy.Velocity * (float) gametime.ElapsedGameTime.TotalSeconds;
        }

        internal virtual void UpdateCollisionBox(Vector2 location)
        {
            enemy.CollisionBox = new Rectangle((int) location.X, (int) location.Y,
                StateSprite.Texture.Width / 8, StateSprite.Texture.Height / 2);

            enemy.ExpandedCollisionBox = new Rectangle((int) location.X, (int) location.Y,
                StateSprite.Texture.Width / 8, (StateSprite.Texture.Height / 2) + 6);
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
                if (enemy.Direction == (int) MarioDirection.RIGHT)
                {
                    enemy.Velocity = new Vector2(75, 0);
                }

                else if (enemy.Direction == (int) MarioDirection.LEFT)
                {
                    enemy.Velocity = new Vector2(-75, 0);
                }
            }
        }
    }

    public class LakituIdleState : LakituState
    {
        public LakituIdleState(LakituRefactored enemy) : base(enemy)
        {
            StateSprite = SpriteEnemyFactory.GetInstance().CreateLakituSprite();
        }

        public override void Trigger()
        {
        }
    }
}