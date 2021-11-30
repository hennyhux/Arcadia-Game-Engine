using GameSpace.Camera2D;
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
        public int Direction { get; set; }
        internal IEnemyState state;

        public AbstractEnemy()
        {
            hasCollidedOnTop = false;
        }

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
            UpdateSpeed();
            UpdateCollisionBox(Position);
            UpdatePosition(Position, gametime);
        }

        public virtual void Trigger()
        {
            hasCollidedOnTop = true;
        }

        public virtual void UpdateSpeed()
        {
            if (CollisionHandler.GetInstance().IsGoingToFall(this))
            {
                Acceleration = new Vector2(0, 400);
            }

            else
            {
                Acceleration = new Vector2(0, 0);
                if (Direction == (int)MarioDirection.RIGHT)
                {
                    Velocity = new Vector2(75, 0);
                }

                else if (Direction == (int)MarioDirection.LEFT)
                {
                    Velocity = new Vector2(-75, 0);
                }
            }
        }

        public virtual void UpdatePosition(Vector2 location, GameTime gameTime)
        {
            Velocity += Acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public virtual void UpdateCollisionBox(Vector2 location)
        {
            CollisionBox = new Rectangle((int)location.X, (int)location.Y,
                state.StateSprite.Texture.Width, state.StateSprite.Texture.Height * 2);

            ExpandedCollisionBox = new Rectangle((int)location.X, (int)location.Y,
                state.StateSprite.Texture.Width, (state.StateSprite.Texture.Height * 2) + 4);
        }
        private protected bool IsInview()
        {
            Camera copyCam = FinderHandler.GetInstance().FindCameraCopy();
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
                    CollisionHandler.GetInstance().EnemyToMarioCollision(this);
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
                case (int)ItemID.WARPPIPEBODY:
                case (int)ItemID.WARPPIPEHEAD:
                case (int)ItemID.WARPPIPEHEADWITHMOB:
                case (int)ItemID.WARPVINEWITHBLOCK:
                case (int)ItemID.WARPPIPEROOM:
                    CollisionHandler.GetInstance().EnemyToBlockCollision(this, entity);
                    break;

                case (int)ItemID.FIREBALL:
                    Trigger();
                    break;
            }
        }

        public bool RevealItem()
        {
            return false;
        }

        public void DeleteCollisionBox()
        {
            CollisionBox = new Rectangle(0, 0, 0, 0);
        }
    }
}
