using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Sprites
{
    class UsedBlockSprite : AbstractSprite
    {

        private protected int maxOffset;
        private protected int currentOffset;
        private protected Vector2 initLocation;

        public UsedBlockSprite(Texture2D texture, int rows, int columns, int totalFrames, Vector2 location)
        {
            this.Texture = texture;
            isVisible = true;
            this.rows = rows;
            this.columns = columns;
            frameWidth = columns;
            frameHeight = rows;
            currentFrame = 0;
            this.totalFrames = totalFrames;
            this.initLocation = location;
            maxOffset = 24;
            currentOffset = 0;
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
                Rectangle destinationRectangle = new Rectangle((int)initLocation.X, (int)initLocation.Y, width *2, height *2);

                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            }
        }

        public override void Update(GameTime gametime)
        {
            if (currentOffset < maxOffset/2)
            {
                initLocation.Y -= 3;
                currentOffset += 3;
            }

            if (currentOffset >= maxOffset/2 && currentOffset < maxOffset)
            {
                initLocation.Y += 3;
                currentOffset += 3;
            }
        }
    }
}
