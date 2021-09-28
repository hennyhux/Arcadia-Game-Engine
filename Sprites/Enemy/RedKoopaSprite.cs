using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Sprites
{ 
    public class RedKoopaSprite : ISprite
    {
        private protected int currentFrame;
        private protected int totalFrames;
        private protected int frameHeight;
        private protected int frameWidth;
        private int startingPointX;
        private int startingPointY;
        private int offsetX;

        private protected int timeSinceLastFrame;
        private protected int milliSecondsPerFrame;

        private bool isVisible;

        private Point frameOrigin;
        private Point frameSize;
        private Point atlasSize;
        private Point currentFramePoint;

        public Texture2D Texture { get; set; }

        public void SetVisible() { isVisible = !isVisible; }

        public RedKoopaSprite(Texture2D texture, int rows, int columns, int totalFrames, int startingPointX,
                int startingPointY)
        {
            Texture = texture;
            isVisible = true;
            currentFrame = 1;
            frameHeight = rows;
            frameWidth = columns;
            this.totalFrames = totalFrames;
            this.startingPointX = startingPointX;
            this.startingPointY = startingPointY;
            offsetX = 0;

            #region points
            currentFramePoint = new Point(startingPointX, startingPointY);
            frameOrigin = new Point(startingPointX, startingPointY);
            atlasSize = new Point(columns, rows);
            frameSize = new Point(Texture.Width / atlasSize.X, Texture.Height / atlasSize.Y);
            #endregion

            #region time
            timeSinceLastFrame = 0;
            milliSecondsPerFrame = 275; 
            #endregion
        }

        public void Update(GameTime gametime)
        {
            if (isVisible && totalFrames > 1)
            {
                timeSinceLastFrame += gametime.ElapsedGameTime.Milliseconds;
                if (timeSinceLastFrame > milliSecondsPerFrame)
                {
                    timeSinceLastFrame -= milliSecondsPerFrame;

                    currentFrame += 1;

                    if (currentFrame >= totalFrames)
                    {
                        currentFrame = 0;
                    }

                    if (currentFramePoint.X < totalFrames) currentFramePoint.X++;

                    if (currentFramePoint.X >= totalFrames) currentFramePoint.X = startingPointX;
                }
            }
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
                Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width * 2, height * 2); //change coordinates

                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            }
        }

        public void UpdateLocation(int dx, int dy)
        {
            throw new NotImplementedException();
        }

    }
    
}
