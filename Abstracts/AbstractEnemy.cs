using GameSpace.Camera2D;
using GameSpace.EntitiesManager;
using GameSpace.Enums;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

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

        internal Boolean hasCollidedOnTop;
        internal Boolean drawBox;
        internal int countDown;
        internal int direction;
        internal IEnemyState state;
        public Rectangle ExpandedCollisionBox { get; set; }

        public virtual void Draw(SpriteBatch spritebatch)
        {
            state.Draw(spritebatch, Position);
            if (drawBox) state.DrawBoundaries(spritebatch, CollisionBox);
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

        }

        internal virtual void UpdateCollisionBox(Vector2 location)
        {
            CollisionBox = new Rectangle((int)location.X + state.StateSprite.Texture.Width / 4, (int)Position.Y,
                state.StateSprite.Texture.Width, state.StateSprite.Texture.Height * 2);

            ExpandedCollisionBox = new Rectangle((int)location.X + state.StateSprite.Texture.Width / 4, (int)Position.Y,
                state.StateSprite.Texture.Width, (state.StateSprite.Texture.Height * 2) + 3);
        }
        internal bool IsInview()
        {
            Camera copyCam = EntityManager.Camera;
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
                    CollisionWithMario(entity);
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
                    CollisionWithBlock(entity);
                    break;

                case (int)ItemID.FIREBALL:
                    CollisionWithFireball(entity);
                    break;
            }
        }

        internal virtual void CollisionWithBlock(IGameObjects block)
        {
            if (EntityManager.DetectCollisionDirection(this, block) == (int)CollisionDirection.LEFT)
            {
                direction = (int)eFacing.LEFT;
            }

            else if (EntityManager.DetectCollisionDirection(this, block) == (int)CollisionDirection.RIGHT)
            {
                direction = (int)eFacing.RIGHT;
            }

            else if (EntityManager.DetectCollisionDirection(this, block) == (int)CollisionDirection.UP)
            {
                PreformBounce();
                HaltAllMotion();
            }
        }

        internal virtual void CollisionWithMario(IGameObjects mario)
        {
            if (EntityManager.DetectCollisionDirection(this, mario) == (int)CollisionDirection.UP)
            {
                Trigger();
                CollisionBox = new Rectangle(1, 1, 0, 0);
                if (!hasCollidedOnTop) hasCollidedOnTop = true;
            }

            else
            {

            }
        }

        internal virtual void CollisionWithFireball(IGameObjects fireball)
        {
            Trigger();
            CollisionBox = new Rectangle(1, 1, 0, 0);
            if (!hasCollidedOnTop) hasCollidedOnTop = true;
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

    }
}
