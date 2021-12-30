using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace GameSpace.Sprites
{
    public class FlagPoleSprite : Sprite
    {

        public FlagPoleSprite(Texture2D texture, int rows, int columns, int totalFrames, int startingPointX,
            int startingPointY)
        {
            Texture = texture;
            isVisible = true;
            this.rows = rows;
            this.columns = columns;
            frameWidth = columns;
            frameHeight = rows;
            currentFrame = 0;
            this.totalFrames = totalFrames;
            this.startingPointX = startingPointX;
            this.startingPointY = startingPointY;
            offsetX = 0;
            currentFramePoint = new Point(startingPointX, startingPointY);
            frameOrigin = new Point(startingPointX, startingPointY);
            atlasSize = new Point(columns, rows);
            frameSize = new Point(Texture.Width / atlasSize.X, Texture.Height / atlasSize.Y);
            timeSinceLastFrame = 0;
            milliSecondsPerFrame = 500;
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
        public void Update(GameTime gametime, bool hasCollided)
        {
            if (isVisible && hasCollided && totalFrames > 1)
            {
                timeSinceLastFrame += gametime.ElapsedGameTime.Milliseconds;
                if (timeSinceLastFrame > milliSecondsPerFrame)
                {
                    timeSinceLastFrame -= milliSecondsPerFrame;

                    currentFrame -= 1;
                    Debug.Print("currentFramePointX : {0}, Y: {1}", currentFramePoint.X, currentFramePoint.Y);
                    if (currentFrame <= 0)
                    {
                        //currentFrame = totalFrames-1;
                    }

                    if (currentFramePoint.X > 0)
                    {
                        currentFramePoint.X--;
                    }

                    if (currentFramePoint.X <= 0)
                    {
                        //currentFramePoint.X = totalFrames-1;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, bool thing)
        {
            if (isVisible)
            {
                int width = Texture.Width / frameWidth;
                int height = Texture.Height / frameHeight;

                Rectangle sourceRectangle = new Rectangle(frameOrigin.X + 4 * frameSize.X,
                    frameOrigin.Y + 0 * frameSize.Y,
                    frameSize.X,
                    frameSize.Y);
                Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width * 2, height * 2);
                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            }
        }

        public override void DrawBoundary(SpriteBatch spriteBatch, Rectangle destination)
        {
            spriteBatch.Draw(WhiteRect, destination, Color.Green * 0.4f);
        }
    }
}
