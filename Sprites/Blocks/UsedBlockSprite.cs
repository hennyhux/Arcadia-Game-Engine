using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Sprites
{
    public class UsedBlockSprite : AbstractSprite
    {

        private protected int maxOffset;
        private protected int currentOffset;
        private protected Vector2 initLocation;

        public UsedBlockSprite(Texture2D texture, int rows, int columns, int totalFrames)
        {
            this.Texture = texture;
            isVisible = true;
            this.rows = rows;
            this.columns = columns;
            frameWidth = columns;
            frameHeight = rows;
            currentFrame = 0;
            this.totalFrames = totalFrames;
            maxOffset = 24;
            currentOffset = 0;
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
