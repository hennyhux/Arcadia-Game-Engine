﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;
using GameSpace.Sprites;

namespace GameSpace.Factories
{
    public class SpriteEnemyFactory
    {

        #region sprites
        private Texture2D Goomba;
        private Texture2D GreenKoopa;
        private Texture2D RedKoopa;
        #endregion

        private static SpriteEnemyFactory instance;
        public static SpriteEnemyFactory GetInstance()
        {
            if (instance == null)
            {
                instance = new SpriteEnemyFactory();
            }

            return instance;
        }

        private SpriteEnemyFactory()
        {

        }

        public void LoadContent(ContentManager content)
        {
            Goomba = content.Load<Texture2D>("Enemies/Goombas");
            RedKoopa = content.Load<Texture2D>("Enemies/RedKoopas");
            GreenKoopa = content.Load<Texture2D>("Enemies/GreenKoopas");
        }

        public ISprite ReturnGoomba()
        {
            return new GoombaSprite(Goomba, 1, 2, 2, 0, 0);
        }

        public ISprite ReturnRedKoopa()
        {
            return new RedKoopaSprite(RedKoopa, 1, 2, 2, 0, 0);
        }

        public ISprite ReturnGreenKoopa()
        {
            return new GreenKoopaSprite(GreenKoopa, 1, 2, 2, 0, 0);
        }
    }
}