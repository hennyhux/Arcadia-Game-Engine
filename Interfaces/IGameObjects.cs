using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Interfaces
{
    public interface IGameObjects
    {
        public ISprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; } //later possible use 
        public Rectangle CollisionBox { get; set; }
        public int ObjectID { get; }
        public void Draw(SpriteBatch spritebatch);
        public void Update(GameTime gametime);
        public void Trigger();
        public void UpdatePosition(Vector2 location, GameTime gametime);
        public void HandleCollision(IGameObjects entity);
        public void ToggleCollisionBoxes();
        public bool IsCurrentlyColliding();
    }
}
