using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.Sprites
{
    public class FloorBlockSprite : Sprite
    {

        public FloorBlockSprite(Texture2D texture, int rows, int columns, int totalFrames)
        {
            Texture = texture;
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
