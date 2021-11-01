using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace GameSpace.GameObjects.ExtraItemsObjects
{
    public class SmallPipe : IGameObjects
    {
        public ISprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Rectangle CollisionBox { get; set; }
        public Vector2 Acceleration { get; set; }

        public Vector2 Location => throw new NotImplementedException();

        public int ObjectID { get; set; }
        private bool hasCollided;
        private bool drawBox;

        public SmallPipe(Vector2 initalPosition)
        {
            ObjectID = (int)ItemID.SMALLPIPE;
            Sprite = SpriteExtraItemsFactory.GetInstance().ReturnSmallPipe();
            Position = initalPosition;
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);
            drawBox = false;
            Debug.WriteLine("EXTRA ITEM AT " + "(" + Position.X + ", " + Position.Y + ")");
        }

        public void Draw(SpriteBatch spritebatch)
        {
            Sprite.Draw(spritebatch, Position); //this shouldnt be hardcoded anymore 
            if (drawBox)
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
            //Pipe does nothing.
        }

        public void UpdatePosition(Vector2 location, GameTime gameTime)
        {
            //Pipe doesn't move.
        }

        public void HandleCollision(IGameObjects entity)
        {
            hasCollided = true;
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
