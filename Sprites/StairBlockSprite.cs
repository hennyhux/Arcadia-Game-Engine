using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Sprites
{
    public class StairBlockSprite : ISprite
    {
        public Texture2D Texture { get; set; }
        private protected int frameWidth;
        private protected int frameHeight;
        private protected int rows;
        private protected int columns;
        private protected int totalFrames;
        private protected int currentFrame;
        private Boolean isVisible;
        public void SetVisible() { isVisible = !isVisible; }

        public StairBlockSprite(Texture2D texture, int rows, int columns, int totalFrames)
        {
            this.Texture = texture;
            isVisible = true;
            this.rows = rows;
            this.columns = columns;
            frameWidth = columns;
            frameHeight = rows;
            currentFrame = 0;
            this.totalFrames = totalFrames;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            if (isVisible)
            {
                int width = Texture.Width / frameWidth;
                int height = Texture.Height / frameHeight;
                int row = currentFrame / frameWidth;
                int column = currentFrame % frameWidth;

                Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
                Rectangle destinationRectangle = new Rectangle(200, 150, width * 2, height * 2);

                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            }
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
