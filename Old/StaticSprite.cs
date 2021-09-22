using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameSpace
{
    public class StaticSprite : ISprite
    {
        private int currentFrame;
        private int totalFrames;
        private int frameHeight;
        private int frameWidth;
        private Vector2 location; 
        public Texture2D Texture { get; set; }
        public Boolean IsVisible { get; set; }
        public void SetVisible() { IsVisible = !IsVisible; }
  
        public StaticSprite(Texture2D texture, int rows, int columns, int frameCounts, Vector2 initialLocation)
        {
            Texture = texture;
            frameHeight = rows;
            frameWidth = columns;
            currentFrame = 0;
            totalFrames = frameCounts;
            IsVisible = true;

            this.location = initialLocation;

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


        public void Draw(SpriteBatch spriteBatch, Vector2 hello)
        {
            if (IsVisible)
            {
                int width = Texture.Width / frameWidth;
                int height = Texture.Height / frameHeight;
                int row = currentFrame / frameWidth;
                int column = currentFrame % frameWidth;

                Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
                Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width * 3, height * 3);

                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            }

        }

        public void UpdateLocation(int dx, int dy)
        {
            location.X += dx;
            location.Y += dy;
        }
    }
}
