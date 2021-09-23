using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Sprites
{
	public class GoombaSprite : ISprite
    {
        public Texture2D Texture { get; set; }
        private protected int frameWidth;
        private protected int frameHeight;
        private protected int Rows;
        private protected int Columns;
        private protected int TotalFrames;
        private protected int currentFrame;
        private Boolean isVisible;
        public void SetVisible() { isVisible = !isVisible; }

        public GoombaSprite(Texture2D texture, int rows, int columns, int totalFrames)
		{
            Texture = texture;
            Rows = rows;
            Columns = columns;
            TotalFrames = totalFrames;
            frameWidth = Columns;
            frameHeight = Rows;
            currentFrame = 0;
		}

        public void Update(Gametime gametime)
        {

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
                Rectangle destinationRectangle = new Rectangle(500, 150, width * 2, height * 2);

                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            }
        }

        public void UpdateLocation(int dx, int dy)
        {
            throw new NotImplementedException();
        }
    }
}
