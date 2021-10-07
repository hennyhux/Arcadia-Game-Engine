using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

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
}
