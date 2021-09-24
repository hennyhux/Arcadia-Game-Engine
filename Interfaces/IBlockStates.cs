using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Interfaces
{
    public interface IBlockStates
    {
        public void Initiate();
        public void Draw(SpriteBatch spriteBatch, Vector2 Location);
        public void Update(GameTime gametime);

    }
}
