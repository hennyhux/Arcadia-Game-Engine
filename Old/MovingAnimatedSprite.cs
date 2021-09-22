using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace
{
    public class MovingAnimatedSprite : ISprite
    {
        private protected int currentFrame;
        private protected int totalFrames;
        private protected int frameHeight;
        private protected int frameWidth;
        private int startingPointX;
        private int startingPointY;
        private int viewportHeight;
        private int viewportWidth;
        private int offsetX;

        private protected int timeSinceLastFrame;
        private protected int milliSecondsPerFrame;

        private bool isVisible;

        private Point frameOrigin;
        private Point frameSize;
        private Point atlasSize;
        private Point currentFramePoint;


        public Texture2D Texture { get; set; }

        public MovingAnimatedSprite(Texture2D texture, int rows, int columns, int totalFrames, int startingPointX,
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
            offsetX = 0;

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
            milliSecondsPerFrame = 150;
            #endregion
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            if (isVisible)
            { 
                int width = Texture.Width / frameWidth;
                int height = Texture.Height / frameHeight;

                Rectangle sourceRectangle = new Rectangle(frameOrigin.X + currentFramePoint.X * frameSize.X - 10,
                    frameOrigin.Y + currentFramePoint.Y * frameSize.Y,
                    frameSize.X ,
                    frameSize.Y);
                Rectangle destinationRectangle = new Rectangle((int)(viewportWidth / 2 - frameSize.X / 2) - offsetX,
                    (int)(viewportHeight / 2 - frameSize.Y / 2), width + 40, height + 40);
                UpdateOffset();
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

        private void UpdateOffset()
        {
            if (offsetX != 300) offsetX++;

            else offsetX = 0;
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
