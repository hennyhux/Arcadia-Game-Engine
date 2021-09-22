using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameSpace
{
    public class MovingStaticSprite : ISprite
    {
        private int currentFrame;
        private int totalFrames;
        private int frameHeight;
        private int frameWidth;
        private int offsetX;
        private int offsetY;
        public Texture2D Texture { get; set; }
        public Boolean IsVisible { get; set; }
        public void SetVisible() { IsVisible = !IsVisible; }
        public MovingStaticSprite(Texture2D texture, int rows, int columns, int frameCounts)
        {
            Texture = texture;
            frameHeight = rows;
            frameWidth = columns;
            currentFrame = 0;
            totalFrames = frameCounts;
            IsVisible = false;
            offsetX = 0;
            offsetY = 0;
        }

        public void Update(GameTime gameTime)
        {
            if (IsVisible)
            {
                currentFrame++;
                if (currentFrame == totalFrames)
                    currentFrame = 0;
            }
        }


        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            if (IsVisible)
            {
                int width = Texture.Width / frameWidth;
                int height = Texture.Height / frameHeight;
                int row = currentFrame / frameWidth;
                int column = currentFrame % frameWidth;

                Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
                Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y + offsetY, width * 3, height * 3);
                UpdateOffset();
                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            }

        }

        private void UpdateOffset()
        {
            if (offsetY != 150) offsetY++;

            else offsetY = 0;
        }

        public void UpdateLocation(int dx, int dy)
        {
            throw new NotImplementedException();
        }
    }
}
