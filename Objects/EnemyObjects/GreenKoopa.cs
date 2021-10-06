﻿using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.GameObjects.EnemyObjects
{
    public class GreenKoopa : IGameObjects
    {
        private IBlockState state;
        public ISprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }

        public Vector2 Location => throw new NotImplementedException();

        public Rectangle Rect { get; set; }

        public int ObjectID => throw new NotImplementedException();

        public GreenKoopa(Vector2 initalPosition)
        {
            //some initial state 
            this.Sprite = EnemySpriteFactory.GetInstance().ReturnGreenKoopa();
            this.Position = initalPosition;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            Sprite.Draw(spritebatch, Position); //this shouldnt be hardcoded anymore 
        }

        public void Update(GameTime gametime)
        {
            Sprite.Update(gametime);
        }

        public void Trigger()
        {
            //death when triggered
        }

        public void SetPosition(Rectangle destination)
        {
            throw new NotImplementedException();
        }

        public void SetPosition(Vector2 location)
        {
            throw new NotImplementedException();
        }

        public void HandleCollsion()
        {
            throw new NotImplementedException();
        }

        public void HandleCollision(IGameObjects entity)
        {
            throw new NotImplementedException();
        }
    }
}
