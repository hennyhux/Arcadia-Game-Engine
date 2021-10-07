﻿using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.GameObjects.EnemyObjects
{
    public class RedKoopa : IGameObjects
    {
        private IBlockState state;
        public ISprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }

        public Vector2 Location => throw new NotImplementedException();

        public Rectangle CollisionBox { get; set; }

        public int ObjectID => throw new NotImplementedException();
        private Boolean hasCollided;
        private Boolean drawBox;

        public RedKoopa(Vector2 initalPosition)
        {
            //some initial state 
            this.Sprite = SpriteEnemyFactory.GetInstance().ReturnRedKoopa();
            this.Position = initalPosition;
            this.CollisionBox = new Rectangle((int)Position.X + Sprite.Texture.Width / 4 + 2, (int)Position.Y, Sprite.Texture.Width / 2, Sprite.Texture.Height * 2);
            drawBox = true;
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
            throw new NotImplementedException();
        }

        public void HandleCollision(IGameObjects entity)
        {
            
        }

        public void ToggleCollisionBoxes()
        {
            throw new NotImplementedException();
        }
    }
}

