using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace
{
    public class AnimatedSprite : ISprite
    {
        private protected int currentFrame;
        private protected int totalFrames;
        private protected int frameHeight;
        private protected int frameWidth;
        private int startingPointX;
        private int startingPointY;
        private int viewportHeight;
        private int viewportWidth;

        private protected int timeSinceLastFrame;
        private protected int milliSecondsPerFrame;

        private bool isVisible;

        private Point frameOrigin;
        private Point frameSize;
        private Point atlasSize;
        private Point currentFramePoint;
        

        public Texture2D Texture { get; set; }

        public AnimatedSprite(Texture2D texture, int rows, int columns, int totalFrames, int startingPointX, 
            int startingPointY, GraphicsDevice screen)
        {
            Texture = texture;
            isVisible = false;
            currentFrame = 1;
            frameHeight = rows;
            frameWidth = columns;
            this.totalFrames = totalFrames;
            this.startingPointX = startingPointX;
            this.startingPointY = startingPointY;

            #region points
            currentFramePoint = new Point(startingPointX, startingPointY);
            frameOrigin = new Point(startingPointX, startingPointY);
            atlasSize = new Point(columns, rows);
            frameSize = new Point(Texture.Width / atlasSize.X, Texture.Height / atlasSize.Y);
            #endregion

            #region viewport
            viewportHeight = screen.Viewport.Height;
            viewportWidth = screen.Viewport.Width;
            #endregion

            #region time
            timeSinceLastFrame = 0;
            milliSecondsPerFrame = 260;
            #endregion
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
                Rectangle destinationRectangle = new Rectangle((int)(viewportWidth / 2 - frameSize.X / 2) - 40, 
                    (int)(viewportHeight / 2 - frameSize.Y / 2) - 40, width + 40 , height + 40);
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

                    //currentFramePoint = new Point(currentFrame % frameSize.X, currentFrame / frameSize.X);
                    if (currentFramePoint.X < totalFrames)currentFramePoint.X++;

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
