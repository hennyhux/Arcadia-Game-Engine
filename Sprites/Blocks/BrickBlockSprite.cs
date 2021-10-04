using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Sprites
{
    public class BrickBlockSprite : AbstractSprite
    {
        public BrickBlockSprite(Texture2D texture, int rows, int columns, int totalFrames, Vector2 initalPosition)
        {
            this.Texture = texture;
            isVisible = true;
            this.rows = rows;
            this.columns = columns;
            frameWidth = columns;
            frameHeight = rows;
            currentFrame = 0;
            this.totalFrames = totalFrames;
            location = initalPosition;
        }

        public override void Update(GameTime gametime)
        {
           
        }

    }
}
