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

        public QuestionBlockSprite(Texture2D texture)
        {
            this.Texture = texture;
            isVisible = true;
            rows = 1;
            columns = totalFrames = 3;
            width = texture.Width / columns;
            height = texture.Height / rows;
            currentFrame = 0;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime gametime)
        {
            throw new NotImplementedException();
        }

        public void SetVisible()
        {
            isVisible = !isVisible;
        }
    }
}
