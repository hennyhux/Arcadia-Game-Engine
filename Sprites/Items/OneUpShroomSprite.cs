using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Sprites
{
    class OneUpShroomSprite : ISprite
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

        public OneUpShroomSprite(Texture2D texture, int rows, int columns, int totalFrames, int startingPointX,
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
            currentFramePoint = new Point(startingPointX, startingPointY);
            frameOrigin = new Point(startingPointX, startingPointY);
            atlasSize = new Point(columns, rows);
            frameSize = new Point(Texture.Width / atlasSize.X, Texture.Height / atlasSize.Y);
            timeSinceLastFrame = 0;
            milliSecondsPerFrame = 275;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            if (isVisible)
            {
                int width = Texture.Width / frameWidth;
                int height = Texture.Height / frameHeight;

                Rectangle sourceRectangle = new Rectangle(frameOrigin.X + currentFramePoint.X * frameSize.X,
                    frameOrigin.Y + currentFramePoint.Y * frameSize.Y,
                    frameSize.X,
                    frameSize.Y);
                Rectangle destinationRectangle = new Rectangle(50, 150, width * 2, height * 2);
                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            }
        }

        public void Update(GameTime gameTime)
        {
            if (isVisible && totalFrames > 1)
            {
                timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
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

        public void SetVisible()
        {
            isVisible = !isVisible;
        }

        public void UpdateLocation(int dx, int dy)
        {
            throw new System.NotImplementedException();
        }
    }
}
