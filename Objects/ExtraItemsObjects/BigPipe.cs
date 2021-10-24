using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using GameSpace.States;
using GameSpace.States.BlockStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GameSpace.GameObjects.ExtraItemsObjects
{
    public class BigPipe : IGameObjects
    {
        public ISprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Rectangle CollisionBox { get; set; }
        public Vector2 Acceleration { get; set; }

        public Vector2 Location => throw new NotImplementedException();

        public int ObjectID {get; set; }
        private Boolean hasCollided;
        private Boolean drawBox;

        public BigPipe(Vector2 initalPosition)
        {
            this.ObjectID = (int)ItemID.BIGPIPE;
            this.Sprite = SpriteExtraItemsFactory.GetInstance().ReturnBigPipe();
            this.Position = initalPosition;
            this.CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);
            drawBox = false;
            Debug.WriteLine("EXTRA ITEM AT " + "(" + this.Position.X + ", " + this.Position.Y + ")");
        }

        public void Draw(SpriteBatch spritebatch)
        {
            Sprite.Draw(spritebatch, Position); //this shouldnt be hardcoded anymore 
            if (drawBox) Sprite.DrawBoundary(spritebatch, CollisionBox);
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
