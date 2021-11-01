using GameSpace.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.Sprites
{
    public abstract class AbstractSprite : ISprite
    {

        protected int currentFrame;
        protected int totalFrames;
        protected int frameHeight;
        protected int frameWidth;
        protected int startingPointX;
        protected int startingPointY;
        protected int offsetX;
        protected int rows;
        protected int columns;
        protected Vector2 location;

        protected int timeSinceLastFrame;
        protected int milliSecondsPerFrame;

        protected bool isVisible;

        protected Point frameOrigin;
        protected Point frameSize;
        protected Point atlasSize;
        protected Point currentFramePoint;

        protected Texture2D WhiteRect = SpriteBlockFactory.GetInstance().CreateBoundingBoxTexture();

        public virtual Texture2D Texture { get; set; }
        public virtual void SetVisible() { isVisible = !isVisible; }
        public bool GetVisibleStatus() { return isVisible; }


        public virtual void Update(GameTime gametime)
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

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            if (isVisible)
            {
                int width = Texture.Width / frameWidth;
                int height = Texture.Height / frameHeight;
                int row = currentFrame / frameWidth;
                int column = currentFrame % frameWidth;

                Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
                Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width * 2, height * 2);
                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            }
        }

        public virtual void UpdateLocation(Vector2 location)
        {

        }

        public virtual void DrawBoundary(SpriteBatch spriteBatch, Rectangle destination)
        {
            spriteBatch.Draw(WhiteRect, destination, Color.Blue * 0.4f);
        }
    }
}
