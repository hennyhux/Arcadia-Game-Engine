using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Sprites
{
    public class FloorBlockSprite : ISprite
    {
        public Texture2D Texture { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public FloorBlockSprite(Texture2D texture)
        {

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            throw new NotImplementedException();
        }

        public void SetVisible()
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime gametime)
        {
            throw new NotImplementedException();
        }
    }
}
