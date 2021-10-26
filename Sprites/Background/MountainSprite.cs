using System;
using System.Collections.Generic;
using System.Text;
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
    public class MountainSprite : AbstractSprite
    {
        public Texture2D Texture;

        public Vector2 Position;

        public MountainSprite(Texture2D texture)
        {
            Texture = texture;
        }

        public MountainSprite(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
        }

        public MountainSprite()
        {

        }

        public void Update()
        {

        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            //Rectangle sourceRectangle = new Rectangle(((int)location.X * (int)parallax.X) % Texture.Width, 0, 1200, Texture.Height);//1 repeating row of pictures
            //Rectangle destinationRectangle = new Rectangle((int)location.X * (int)parallax.X, 0, 1200, Texture.Height);
            //spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, Vector2 parallax)
        {

            Rectangle sourceRectangle = new Rectangle(((int)location.X * (int)parallax.X) % Texture.Width, 0, 1200, Texture.Height);//1 repeating row of pictures
            Rectangle destinationRectangle = new Rectangle((int)location.X * (int)parallax.X, 0, 1200, Texture.Height);
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
           
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = new Rectangle((int)location.X, 0, 800, Texture.Height);//1 repeating row of pictures
            Rectangle destinationRectangle = new Rectangle((int)location.X, 0, 800, Texture.Height);
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
        }

    }
}
