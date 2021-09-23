using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Factories
{
    public class BackgroundFactory
    {
        private BackgroundFactory instance;
        public BackgroundFactory Instance => instance;

        public BackgroundFactory()
        {

        }

        public void LoadContent(ContentManager content)
        {
            content.Load<Texture2D>("Backgrounds/RegularBackground");
        }
        

    }
}
