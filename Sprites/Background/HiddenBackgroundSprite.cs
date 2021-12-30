using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace GameSpace.Sprites.Background
{
    public class HiddenBackgroundSprite : Sprite
    {
        public Texture2D Texture;

        public Vector2 Position;

        public HiddenBackgroundSprite(Texture2D texture)
        {
            Texture = texture;
        }

        public HiddenBackgroundSprite(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
        }

        public HiddenBackgroundSprite()
        {

        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 location)
        {

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 cameraLocation, Vector2 parallax)
        {
            //Debug.WriteLine("xPox {1}, camera Xpos: {0}", (int)cameraLocation.X * (int)parallax.X, ((int)cameraLocation.X * (int)parallax.X) % Texture.Width);
            //Debug.WriteLine("parallax {0}, camera abs X: {0}", (int)parallax.X, (int)cameraLocation.X);
            Rectangle sourceRectangle = new Rectangle((int)(cameraLocation.X * parallax.X) % Texture.Width, 0, 1200, Texture.Height);//1 repeating row of pictures
            Rectangle destinationRectangle = new Rectangle((int)(cameraLocation.X * parallax.X) + (int)Position.X, (int)cameraLocation.Y + (int)Position.Y, 1200, Texture.Height);//the 1200 could be a constant or some value based
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);                                                      // on window size
        }

        public override void DrawBoundary(SpriteBatch spriteBatch, Rectangle destination)
        {
            spriteBatch.Draw(WhiteRect, destination, Color.Green * 0.4f);
        }
    }
}
