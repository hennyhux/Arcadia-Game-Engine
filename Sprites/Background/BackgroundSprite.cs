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
            if (Texture != null)
            {
                Rectangle sourceRectangle;
                Rectangle destinationRectangle;
                if (Texture.Width == 257)
                {
                     sourceRectangle = new Rectangle((int)location.X, 0, 1200, Texture.Height);//1 repeating row of pictures
                     destinationRectangle = new Rectangle((int)location.X, 0, 1200, Texture.Height);
                     spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
                }
                else
                {
                    sourceRectangle = new Rectangle((int)location.X % Texture.Width, 0, Texture.Width, Texture.Height);//1 repeating row of pictures
                    destinationRectangle = new Rectangle((int)location.X, 0, Texture.Width, Texture.Height);
                    spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
                }

            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, Vector2 parallax)
        {
            if (Texture != null)
            {
                Rectangle sourceRectangle;
                Rectangle destinationRectangle;
                if (Texture.Width == 257)
                {
                    sourceRectangle = new Rectangle(((int)location.X * (int)parallax.X) % Texture.Width, 0, 1200, Texture.Height);//1 repeating row of pictures
                    destinationRectangle = new Rectangle((int)location.X * (int)parallax.X, 0, 1200, Texture.Height);
                    spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
                }
                if (Texture.Width == 182)
                {
                    sourceRectangle = new Rectangle(((int)location.X * (int)parallax.X) % Texture.Width, 0, 1200, Texture.Height);//1 repeating row of pictures
                    destinationRectangle = new Rectangle((int)location.X * (int)parallax.X, 0, 1200, Texture.Height);
                    spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
                }
                else
                {
                    sourceRectangle = new Rectangle(((int)location.X * (int)parallax.X) % Texture.Width, 0, 1200, Texture.Height);//1 repeating row of pictures
                    destinationRectangle = new Rectangle((int)location.X * (int)parallax.X, 200, 1200, Texture.Height);
                    spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
                }

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Texture != null)
            {
                /*if (Texture.Width > 1000)
                {
                    Rectangle sourceRectangle = new Rectangle(0, 0, 800, 480);
                    //Rectangle sourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
                    Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, 800, 480);
                    spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
                    //spriteBatch.Draw(Texture, location, Color.White);
                }
                else
                {
                    spriteBatch.Draw(Texture, Vector2.Zero, Color.White);
                }//*/
                Rectangle sourceRectangle = new Rectangle((int)location.X, 0, 800, Texture.Height);//1 repeating row of pictures
                //Rectangle sourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
                Rectangle destinationRectangle = new Rectangle((int)location.X, 0, 800, Texture.Height);

                /*Rectangle sourceRectangle = new Rectangle((int)location.X, 0, 800, 480);
                //Rectangle sourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
                Rectangle destinationRectangle = new Rectangle((int)location.X, 0, 800, 480);*/

                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            }
                //spriteBatch.Draw(Texture, Position, Color.White);
            //spriteBatch.Draw(Texture, Vector2.Zero, Color.White);

        }


        public override void DrawBoundary(SpriteBatch spriteBatch, Rectangle destination)
        {
            spriteBatch.Draw(WhiteRect, destination, Color.Green * 0.4f);
        }
    }
}
