using GameSpace.Interfaces;
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

        public virtual Rectangle ExpandedCollisionBox { get; set; }

        public virtual void Draw(SpriteBatch spritebatch)
        {
            Sprite.Draw(spritebatch, Position); //this shouldnt be hardcoded anymore 
            if (drawBox)
            {
                Sprite.DrawBoundary(spritebatch, CollisionBox);
                Sprite.DrawBoundary(spritebatch, ExpandedCollisionBox);
            }
        }
        public abstract void HandleCollision(IGameObjects entity);
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

        }
        public virtual void Update(GameTime gametime)
        {
            Sprite.Update(gametime);
            UpdatePosition(Position, gametime);
        }
        public virtual void UpdatePosition(Vector2 location, GameTime gametime)
        {
            Velocity += Acceleration * (float)gametime.ElapsedGameTime.TotalSeconds;
            Position += Velocity * (float)gametime.ElapsedGameTime.TotalSeconds;
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
