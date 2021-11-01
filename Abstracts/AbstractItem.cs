using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

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
        internal Boolean drawBox;
        public virtual void Draw(SpriteBatch spritebatch)
        {
            Sprite.Draw(spritebatch, Position); //this shouldnt be hardcoded anymore 
            if (drawBox) Sprite.DrawBoundary(spritebatch, CollisionBox);
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
        }
        public virtual void UpdatePosition(Vector2 location, GameTime gametime)
        {

        }
    }
}
