using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.Sprites
{
    public class StairBlockSprite : AbstractSprite
    {

        public StairBlockSprite(Texture2D texture, int rows, int columns, int totalFrames)
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
    }
}
