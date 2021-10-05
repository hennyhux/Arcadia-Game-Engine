using GameSpace.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.States
{
    //A reusable class that can show blocks being bumped 
    public class BumpAnimation : AbstractSprite
    {
        private Texture2D sprite;
        private protected int maxOffset;
        private protected int currentOffset;
        private protected Vector2 initLocation;

        public BumpAnimation(Texture2D texture, int x, int y, int rows = 1, int columns = 1, int totalFrames = 1)
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
            initLocation.X = x;
            initLocation.Y = y;
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
                Rectangle destinationRectangle = new Rectangle((int)initLocation.X, (int)initLocation.Y, width * 2, height * 2);

                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            }
        }

        public override void Update(GameTime gametime)
        {
            if (currentOffset < maxOffset / 2)
            {
                initLocation.Y -= 3;
                currentOffset += 3;
            }

            if (currentOffset >= maxOffset / 2 && currentOffset < maxOffset)
            {
                initLocation.Y += 3;
                currentOffset += 3;
            }
        }
    }
}