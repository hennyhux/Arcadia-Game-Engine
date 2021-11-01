using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.Interfaces
{
    internal interface IMario
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; } //later possible use 
        public Rectangle CollisionBox { get; set; }
        public int ObjectID { get; }
        public void Draw(SpriteBatch spritebatch, Vector2 location);
        public void Update(GameTime gametime);
        //public void Trigger();
    }
}
