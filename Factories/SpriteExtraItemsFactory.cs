﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameSpace.Sprites;
using Microsoft.Xna.Framework;

namespace GameSpace.Factories
{
    public class SpriteExtraItemsFactory
    {
        #region sprites
        private Texture2D BigPipe;
        private Texture2D MediumPipe;
        private Texture2D SmallPipe;
        #endregion

        private static SpriteExtraItemsFactory instance;
        public static SpriteExtraItemsFactory GetInstance()
        {
            if (instance == null)
            {
                instance = new SpriteExtraItemsFactory();
            }

            return instance;
        }

        private SpriteExtraItemsFactory()
        {
        }
        public void LoadContent(ContentManager content)
        {
            BigPipe = content.Load<Texture2D>("ExtraItems/BigPipe");
            //MediumPipe = content.Load<Texture2D>("ExtraItems/MediumPipe");
            //SmallPipe = content.Load<Texture2D>("ExtraItems/SmallPipe");
        }

        public ISprite ReturnBigPipe()
        {
            return new BigPipeSprite(BigPipe, 1, 1, 1);
        }

       /* public ISprite ReturnMediumPipe()
        {
            return new MediumPipeSprite(BigPipe, 1, 1, 1);
        }

        public ISprite ReturnSmallPipe()
        {
            return new SmallPipeSprite(BigPipe, 1, 1, 1);
        }*/
    }
}
