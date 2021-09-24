using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Sprites
{
    public class BackgroundRegularSprite : ISprite
    {
        private Texture2D background;
        public Texture2D Texture { get; set; }
        Rectangle sourceRectangle;
        private int blackBackgroundWidth = 1200;
        private int blackBackgroundHeight = 600;
        private int textureX = 0;
        private int textureY = 0;
        public BackgroundRegularSprite(Texture2D texture)
        {
            background = texture;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {

        }

        public void SetVisible()
        {

        }

        public void Update(GameTime gametime)
        {
            
        }

        public void UpdateLocation(int dx, int dy)
        {
            throw new NotImplementedException();
        }

 
    }
}
