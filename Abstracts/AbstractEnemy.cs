using GameSpace.Camera2D;
using GameSpace.EntitiesManager;
using GameSpace.EntityManaging;
using GameSpace.Enums;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.Abstracts
{
    public class AbstractEnemy : IGameObjects
    {
        public ISprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public Rectangle CollisionBox { get; set; }
        public int ObjectID { get; set; }

        internal bool hasCollidedOnTop;
        internal bool drawBox;
        internal int countDown;
        public int direction { get; set; }
        internal IEnemyState state;
        public Rectangle ExpandedCollisionBox { get; set; }

        public virtual void Draw(SpriteBatch spritebatch)
        {
            state.Draw(spritebatch, Position);
            if (drawBox)
            {
                state.DrawBoundaries(spritebatch, CollisionBox);
            }
        }
        public virtual void Update(GameTime gametime)
        {
            state.Update(gametime);
            UpdatePosition(Position, gametime);
        }

        public virtual void Trigger()
        {

        }

        public virtual void UpdatePosition(Vector2 location, GameTime gameTime)
        {
            if (ColliderMachine.GetInstance().IsGoingToFall(this))
            {
                Acceleration = new Vector2(0, 400);
            }

            else
            {
                Acceleration = new Vector2(0, 0);
                if (direction == (int)eFacing.RIGHT)
                {
                    Velocity = new Vector2(85, 0);
                }

                if (direction == (int)eFacing.LEFT)
                {
                    Velocity = new Vector2(-85, 0);
                }
            }

            Velocity += Acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            UpdateCollisionBox(Position);
        }

        internal virtual void UpdateCollisionBox(Vector2 location)
        {
            CollisionBox = new Rectangle((int)location.X, (int)location.Y,
                state.StateSprite.Texture.Width, state.StateSprite.Texture.Height * 2);

            ExpandedCollisionBox = new Rectangle((int)location.X, (int)location.Y,
                state.StateSprite.Texture.Width, (state.StateSprite.Texture.Height * 2) + 6);
        }
        internal bool IsInview()
        {
            Camera copyCam = FinderMachine.GetInstance().FindCameraCopy();
            return (Position.X > copyCam.Position.X && Position.X < copyCam.Position.X + 800);
        }

        public virtual void ToggleCollisionBoxes()
        {
            drawBox = !drawBox;
        }

        public virtual bool IsCurrentlyColliding()
        {
            return false;
        }

        public virtual void HandleCollision(IGameObjects entity)
        {
            switch (entity.ObjectID)
            {
                case (int)AvatarID.MARIO:
                    ColliderMachine.GetInstance().EnemyToMarioCollision(this, entity);
                    break;

                case (int)BlockID.USEDBLOCK:
                case (int)BlockID.QUESTIONBLOCK:
                case (int)BlockID.FLOORBLOCK:
                case (int)BlockID.STAIRBLOCK:
                case (int)BlockID.COINBRICKBLOCK:
                case (int)BlockID.BRICKBLOCK:
                case (int)ItemID.BIGPIPE:
                case (int)ItemID.MEDIUMPIPE:
                case (int)ItemID.SMALLPIPE:
                    ColliderMachine.GetInstance().EnemyToBlockCollision(this, entity);
                    break;

                case (int)ItemID.FIREBALL:
                    CollisionWithFireball(entity);
                    break;
            }
        }


        internal virtual void CollisionWithMario(IGameObjects mario)
        {
            if (EntityManager.DetectCollisionDirection(this, mario) == (int)CollisionDirection.UP)
            {
                Trigger();
                CollisionBox = new Rectangle(1, 1, 0, 0);
                if (!hasCollidedOnTop)
                {
                    hasCollidedOnTop = true;
                }
            }

            else
            {

            }
        }

        internal virtual void CollisionWithFireball(IGameObjects fireball)
        {
            Trigger();
            CollisionBox = new Rectangle(1, 1, 0, 0);
            if (!hasCollidedOnTop)
            {
                hasCollidedOnTop = true;
            }
        }

        internal void HaltAllMotion()
        {
            Velocity = new Vector2(0, 0);
            Acceleration = new Vector2(0, 0);
        }

        internal void PreformBounce()
        {
            Position = new Vector2(Position.X, Position.Y - 4);
        }

        public bool RevealItem()
        {
            throw new System.NotImplementedException();
        }
    }
}
