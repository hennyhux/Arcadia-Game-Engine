using System;
using System.Collections.Generic;
using System.Text;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using GameSpace.States.ItemStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.GameObjects.ItemObjects
{
    public class Coin : IGameObjects
    {
        private IBlockState state;
        public ISprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public Rectangle CollisionBox { get; set; }
        public int ObjectID {get; set;}

        private Boolean hasCollided;
        private Boolean drawBox;

        public Coin(Vector2 initalPosition)
        {
            this.ObjectID = (int)ItemID.COIN;
            this.Sprite = SpriteItemFactory.GetInstance().CreateCoin();
            this.Position = initalPosition;
            this.CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2 / 4, Sprite.Texture.Height * 2);
            drawBox = false;
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

        }

        public void HandleCollision(IGameObjects entity)
        {
            throw new NotImplementedException();
        }
        public void SetPosition(Vector2 location)
        {
            throw new NotImplementedException();
        }

        public void ToggleCollisionBoxes()
        {
            drawBox = !drawBox;
        }
    }
}
