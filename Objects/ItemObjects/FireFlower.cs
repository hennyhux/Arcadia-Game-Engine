using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameSpace.GameObjects.ItemObjects
{
    public class FireFlower : IGameObjects
    {
        private readonly IObjectState state;
        public ISprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public Rectangle CollisionBox { get; set; }
        public int ObjectID { get; set; }

        private readonly bool hasCollided;
        private bool drawBox;

        public FireFlower(Vector2 initialPosition)
        {
            ObjectID = (int)ItemID.FIREFLOWER;
            Sprite = SpriteItemFactory.GetInstance().CreateFireFlower();
            Position = initialPosition;
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2 / 4, Sprite.Texture.Height * 2);
            hasCollided = false;
            drawBox = false;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            Sprite.Draw(spritebatch, Position);
            if (drawBox && Sprite.GetVisibleStatus())
            {
                Sprite.DrawBoundary(spritebatch, CollisionBox);
            }
        }

        public void Update(GameTime gametime)
        {
            Sprite.Update(gametime);
        }

        public void Trigger()
        {
            Sprite.SetVisible();
            CollisionBox = new Rectangle(1, 1, 0, 0);
        }

        public void HandleCollision(IGameObjects entity)
        {
            switch (entity.ObjectID)
            {
                case (int)AvatarID.MARIO:
                    Trigger();
                    break;
            }
        }

        public void UpdatePosition(Vector2 location, GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void ToggleCollisionBoxes()
        {
            drawBox = !drawBox;
        }

        public bool IsCurrentlyColliding()
        {
            throw new NotImplementedException();
        }
    }
}
