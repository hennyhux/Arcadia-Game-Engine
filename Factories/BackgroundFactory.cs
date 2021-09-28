using GameSpace.Sprites;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Factories
{
    public class BackgroundFactory
    {
        private Texture2D RegularBackground;

        private static BackgroundFactory instance = new BackgroundFactory();
        public static BackgroundFactory GetInstance ()
        {
            return instance;
        }

        private BackgroundFactory()
        {

        }

        public void LoadContent(ContentManager content)
        {
            RegularBackground = content.Load<Texture2D>("Background/Background");
        }

        public ISprite ReturnRegularBackground()
        {
            return new BackgroundRegularSprite(RegularBackground);
        }
        
    }
}
