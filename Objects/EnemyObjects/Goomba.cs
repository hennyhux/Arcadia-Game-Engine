using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GameSpace.GameObjects.EnemyObjects
{
    public class Goomba : IGameObjects
    {
        private IBlockState state;
        public ISprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }

        public Rectangle CollisionBox { get; set; }

        public int ObjectID { get; set; }
        private Boolean hasCollided;
        private Boolean drawBox;

        public Goomba(Vector2 initalPosition)
        {
            //some initial state 
            ObjectID = (int)EnemyID.GOOMBA;
            this.Sprite = SpriteEnemyFactory.GetInstance().ReturnGoomba();
            this.Position = initalPosition;
            //magic numbers to offset the weird texture atlas resoultion 
            this.CollisionBox = new Rectangle((int)Position.X + Sprite.Texture.Width / 4, (int)Position.Y, Sprite.Texture.Width / 2, Sprite.Texture.Height * 2);
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
            //death when triggered
        }

        public void SetPosition(Vector2 location)
        {
            Velocity = (float)5 * location;
            Position += Velocity;
            CollisionBox = new Rectangle((int)Position.X + Sprite.Texture.Width / 4, (int)Position.Y, Sprite.Texture.Width / 2, Sprite.Texture.Height * 2);
        }

        public void HandleCollision(IGameObjects entity)
        {
            if (!hasCollided) hasCollided = true;
            this.CollisionBox = new Rectangle((int)Position.X + Sprite.Texture.Width / 4, (int)Position.Y + 10, Sprite.Texture.Width / 2, Sprite.Texture.Height * 2);
            this.Position = new Vector2(this.Position.X, (int)this.Position.Y + 6);

        }

        public void ToggleCollisionBoxes()
        {
            drawBox = !drawBox;
        }
    }
}

