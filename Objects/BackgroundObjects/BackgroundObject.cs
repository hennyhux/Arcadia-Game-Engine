using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.Objects.BackgroundObjects
{
    public abstract class BackgroundObject : IGameObjects
    {
        public ISprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public Rectangle CollisionBox { get; set; }

        public int ObjectID { get; set; }
        private readonly bool hasCollidedOnTop;
        private readonly bool drawBox;
        private readonly int countDown;
        private readonly int direction;

        public Vector2 Parallax { get; set; }

        protected BackgroundObject()
        {

        }

        public abstract void Draw(SpriteBatch spritebatch, Vector2 position);

        public void Draw(SpriteBatch spritebatch) { }
        public abstract void Update(GameTime gametime);


        public abstract void Trigger();


        public abstract void UpdatePosition(Vector2 location, GameTime gameTime);


        public abstract void HandleCollision(IGameObjects entity);

        public abstract void ToggleCollisionBoxes();

        public abstract bool IsCurrentlyColliding();

    }
}
