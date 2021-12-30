using GameSpace.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.States
{
    //A reusable class that can show blocks being bumped 
    public class BumpAnimation : Sprite
    {
        private readonly Texture2D sprite;
        private protected int maxOffset;
        private protected int currentOffset;
        private protected Vector2 initLocation;
        public bool animationFinished;

        public BumpAnimation(Texture2D texture, int x, int y, int MaxOffset, int rows = 1, int columns = 1, int totalFrames = 1)
        {
            Texture = texture;
            isVisible = true;
            this.rows = rows;
            this.columns = columns;
            frameWidth = columns;
            frameHeight = rows;
            currentFrame = 0;
            this.totalFrames = totalFrames;
            initLocation = location;
            maxOffset = MaxOffset;
            currentOffset = 0;
            initLocation.X = x;
            initLocation.Y = y;
            animationFinished = false;
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

            else if (currentOffset >= maxOffset / 2 && currentOffset < maxOffset)
            {
                initLocation.Y += 3;
                currentOffset += 3;
            }
            else
            {
                animationFinished = true;
                //Debug.WriteLine("animationFinished {0}", animationFinished);
            }
        }
    }
}