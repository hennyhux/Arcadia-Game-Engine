using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
namespace GameSpace.Sprites.Background
{
    public class BackgroundSprite : AbstractSprite
    {
        public Texture2D Texture;

        public Vector2 Position;

        public BackgroundSprite(Texture2D texture)
        {
            Texture = texture;
        }

        public BackgroundSprite(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
        }

        public BackgroundSprite()
        {

        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 location)
        {

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 cameraLocation, Vector2 parallax)
        {
            Rectangle sourceRectangle = new Rectangle((int)cameraLocation.X, 0, 1200, Texture.Height);//1 repeating row of pictures
            Rectangle destinationRectangle = new Rectangle((int)cameraLocation.X, (int)cameraLocation.Y + (int)Position.Y, 1200, Texture.Height);//the 1200 could be a constant or some value based
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);                                                      // on window size
        }

        public override void DrawBoundary(SpriteBatch spriteBatch, Rectangle destination)
        {
            spriteBatch.Draw(WhiteRect, destination, Color.Green * 0.4f);
        }
    }
}
