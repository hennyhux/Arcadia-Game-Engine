﻿using GameSpace.Factories;
using GameSpace.GameObjects.EnemyObjects;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace GameSpace.States
{
    public class StateLakituLeft : IEnemyState
    {
        public ISprite StateSprite { get; set; }
        public bool CollidedWithMario { get; set; }
        private readonly Lakitu Lakitu;


        public StateLakituLeft(Lakitu lakitu)
        {
            Debug.WriteLine("CREATE LEFT SPRITE: ,");
            //StateSprite = SpriteEnemyFactory.GetInstance().CreateLakituSprite();
            CollidedWithMario = false;
            Lakitu = lakitu;
            Lakitu.state = this;
            Lakitu.Velocity = lakitu.Velocity;
            lakitu.Sprite = SpriteEnemyFactory.GetInstance().CreateLakituSprite();
        }

        public void Draw(SpriteBatch spritebatch, Vector2 location)
        {
            //StateSprite.Draw(spritebatch, location);
        }

        public void Update(GameTime gametime)
        {
           // StateSprite.Update(gametime);
        }

        public void Trigger()
        {
        }

        public void DrawBoundaries(SpriteBatch spritebatch, Rectangle destination)
        {
            //StateSprite.DrawBoundary(spritebatch, destination);
        }
    }
}