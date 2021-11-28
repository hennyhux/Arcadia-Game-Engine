using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.Sprites
{
    public class GoombaSprite : AbstractSprite
    {
        public GoombaSprite(Texture2D texture, int rows, int columns, int totalFrames, int startingPointX,
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

        public override void DrawBoundary(SpriteBatch spriteBatch, Rectangle destination)
        {
            spriteBatch.Draw(WhiteRect, destination, Color.Red * 0.4f);
        }

    }

    public class UberGoombaSprite : AbstractSprite
    {
        
        public UberGoombaSprite(Texture2D texture, int rows, int columns, int totalFrames, int startingPointX,
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
                int row = currentFrame / frameWidth;
                int column = currentFrame % frameWidth;

                Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
                Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width * 3, height * 3);
                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White, 0, new Vector2(0, 0), Facing, 0);
            }
        }
    }
}
