using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Sprites
{
    public class CastleSprite : AbstractSprite
    {

        public CastleSprite(Texture2D texture, int rows, int columns, int totalFrames)
        {
            this.Texture = texture;
            isVisible = true;
            this.rows = rows;
            this.columns = columns;
            frameWidth = columns;
            frameHeight = rows;
            currentFrame = 0;
            this.totalFrames = totalFrames;
        }

        public override void Update(GameTime gametime)
        {

        }
    }
}
