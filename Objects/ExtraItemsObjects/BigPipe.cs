using System;
using System.Collections.Generic;
using System.Text;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using GameSpace.States;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.Objects.ExtraItemsObjects
{
    public class BigPipe : IGameObjects
    {
        private IObjectState state;
        public ISprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Rectangle CollisionBox { get; set; }
        public Vector2 Acceleration { get; set; }

        public Vector2 Location => throw new NotImplementedException();

        public int ObjectID { get; set; }
        private Boolean hasCollided;
        private Boolean drawBox;

        public BigPipe(Vector2 initalPosition)
        {
            this.ObjectID = (int)ItemID.BIGPIPE;
            this.Sprite = SpriteExtraItemsFactory.GetInstance().ReturnBigPipe();
            this.Position = initalPosition;
            //this.CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);
            drawBox = false;
            Debug.WriteLine(Sprite.Texture.Width + " " + Sprite.Texture.Height);
            Debug.WriteLine("ExtraItem AT " + "(" + this.Position.X + ", " + this.Position.Y + ")");
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

        void IGameObjects.Trigger()
        {
            //Pipe does nothing.
        }

        void IGameObjects.SetPosition(Vector2 location)
        {
            //Pipe doesn't move.
        }

        void IGameObjects.HandleCollision(IGameObjects entity)
        {
            hasCollided = true;
        }

        void IGameObjects.ToggleCollisionBoxes()
        {
            drawBox = !drawBox;
        }

        bool IGameObjects.IsCurrentlyColliding()
        {
            throw new NotImplementedException();
        }
    }
}
