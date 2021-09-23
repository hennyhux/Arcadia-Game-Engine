﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;
using GameSpace.Sprites;

namespace GameSpace.Factories
{
    public class EnemyFactory
    {

        #region sprites
        private Texture2D Goomba;
        private Texture2D GreenKoopa;
        private Texture2D RedKoopa;
        #endregion

        private static EnemyFactory instance = new EnemyFactory();
        public static EnemyFactory Instance => instance;

        public EnemyFactory()
        {

        }

        public void LoadContent(ContentManager content)
        {
            Goomba = content.Load<Texture2D>("Enemies/Goombas");
        }

        public ISprite ReturnGoomba()
        {
            return new GoombaSprite(Goomba, 1, 1, 1);
        }
    }
}