using GameSpace.EntityManaging;
using GameSpace.Enums;
using GameSpace.Interfaces;
using GameSpace.Machines;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.Abstracts
{
    public abstract class AbstractItem : IGameObjects
    {
        public virtual ISprite Sprite { get; set; }
        public virtual Vector2 Position { get; set; }
        public virtual Vector2 Velocity { get; set; }
        public virtual Vector2 Acceleration { get; set; }
        public virtual Rectangle CollisionBox { get; set; }
        public virtual int ObjectID { get; set; }
        internal bool drawBox;

        internal bool hasCollided;

        public virtual Rectangle ExpandedCollisionBox { get; set; }

        public virtual void Draw(SpriteBatch spritebatch)
        {
            Sprite.Draw(spritebatch, Position); //this shouldnt be hardcoded anymore 
            if (drawBox && !hasCollided)
            {
                Sprite.DrawBoundary(spritebatch, CollisionBox);
                Sprite.DrawBoundary(spritebatch, ExpandedCollisionBox);
            }
        }
        public virtual void HandleCollision(IGameObjects entity)
        {
            switch (entity.ObjectID)
            {
                case (int)AvatarID.MARIO:
                    Trigger();
                    break;
            }
        }
        public virtual bool IsCurrentlyColliding()
        {
            return false; //future use 
        }
        public virtual void ToggleCollisionBoxes()
        {
            drawBox = !drawBox;
        }

        public virtual void Trigger()
        {
            hasCollided = true;
            Sprite.SetVisible();
            DeleteCollisionBox();
        }
        public virtual void Update(GameTime gametime)
        {
            Sprite.Update(gametime);
            if (!hasCollided)
            {
                UpdateSpeed();
                UpdatePosition(Position, gametime);
                UpdateCollisionBox();
            }
        }
        public virtual void UpdatePosition(Vector2 location, GameTime gametime)
        {
            Velocity += Acceleration * (float)gametime.ElapsedGameTime.TotalSeconds;
            Position += Velocity * (float)gametime.ElapsedGameTime.TotalSeconds;
        }

        public virtual void UpdateCollisionBox()
        {
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y,
              Sprite.Texture.Width, Sprite.Texture.Height * 2);

            ExpandedCollisionBox = new Rectangle((int)Position.X, (int)Position.Y,
                Sprite.Texture.Width, (Sprite.Texture.Height * 2) + 4);
        }
        internal virtual void UpdateSpeed()
        {
            if (CollisionHandler.GetInstance().IsGoingToFall(this))
            {
                Acceleration = new Vector2(0, 400);
            }

            else
            {
                Acceleration = new Vector2(0, 0);
                if (FinderHandler.GetInstance().FindMario().Facing == eFacing.RIGHT)
                {
                    Velocity = new Vector2(75, 0);
                }

                else if (FinderHandler.GetInstance().FindMario().Facing == eFacing.LEFT)
                {
                    Velocity = new Vector2(-75, 0);
                }
            }
        }
        public virtual bool RevealItem()
        {
            return false;
        }

        public void DeleteCollisionBox()
        {
            CollisionBox = new Rectangle(0, 0, 0, 0);
        }

        public virtual void AdjustLocationComingOutOfBlock()
        {
            Position = new Vector2(Position.X + 6, Position.Y - Sprite.Texture.Height * 2);
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2 / 4, Sprite.Texture.Height * 2);
        }
    }
}
