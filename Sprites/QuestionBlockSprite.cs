using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Sprites
{
    class QuestionBlockSprite : ISprite
    {
        public Texture2D Texture { get ; set; }
        private protected int width;
        private protected int height;
        private protected int rows;
        private protected int columns;
        private protected int totalFrames;
        private protected int currentFrame;
        private Boolean isVisible;

        private protected int timeSinceLastFrame;
        private protected int milliSecondsPerFrame;


        public QuestionBlockSprite(Texture2D texture, int rows, int columns, int totalFrames)
        {
            this.Texture = texture;
            isVisible = true;
            this.rows = rows;
            this.columns = columns;
            width = texture.Width / columns;
            height = texture.Height / rows;
            currentFrame = 0;
            this.totalFrames = totalFrames;

            timeSinceLastFrame = 0;
            milliSecondsPerFrame = 260;

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            if (isVisible)
            {
                
            }
        }

        public void Update(GameTime gametime)
        {
            throw new NotImplementedException();
        }

        public void SetVisible()
        {
            isVisible = !isVisible;
        }

        public void UpdateLocation(int dx, int dy)
        {
            throw new NotImplementedException();
        }
    }
}
