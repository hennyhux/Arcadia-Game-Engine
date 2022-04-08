using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.Sprites
{
    internal class AnimatedSprite : Sprite
    {

        public AnimatedSprite(Texture2D texture, int rows, int columns, int totalFrames, int startingPointX,
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

        public override void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            if (isVisible)
            {
                int width = Texture.Width / frameWidth;
                int height = Texture.Height / frameHeight;

                Rectangle sourceRectangle = new Rectangle(frameOrigin.X + currentFramePoint.X * frameSize.X,
                    frameOrigin.Y + currentFramePoint.Y * frameSize.Y,
                    frameSize.X,
                    frameSize.Y);
                Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width * 2, height * 2);
                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            }
        }

        public override void Update(GameTime gameTime)
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

                    if (currentFramePoint.X < totalFrames)
                    {
                        currentFramePoint.X++;
                    }

                    if (currentFramePoint.X >= totalFrames)
                    {
                        currentFramePoint.X = startingPointX;
                    }
                }
            }
        }
    }
}
