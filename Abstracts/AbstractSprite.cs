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

        private static int hey = 0;

        protected int timeSinceLastFrame;
        protected int milliSecondsPerFrame;

        protected bool isVisible;

        protected Point frameOrigin;
        protected Point frameSize;
        protected Point atlasSize;
        protected Point currentFramePoint;

        protected Texture2D WhiteRect = SpriteBlockFactory.GetInstance().CreateBoundingBoxTexture();

        public AbstractSprite()
        {
            Facing = SpriteEffects.None;
        }

        public virtual Texture2D Texture { get; set; }
        public virtual void SetVisible() { isVisible = !isVisible; }
        public bool GetVisibleStatus() { return isVisible; }

        public SpriteEffects Facing { get; set; }


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
                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White, 0, new Vector2(0, 0), Facing, 0);
            }
        }

        public virtual void UpdateLocation(Vector2 location)
        {

        }

        public virtual void DrawBoundary(SpriteBatch spriteBatch, Rectangle destination)
        {
            spriteBatch.Draw(WhiteRect, destination, Color.Blue * 0.4f);
        }

        public virtual void FlipSprite()
        {
            Facing = SpriteEffects.FlipHorizontally;
        }

        public void RevertSprite()
        {
            Facing = SpriteEffects.None;
        }
    }
}
