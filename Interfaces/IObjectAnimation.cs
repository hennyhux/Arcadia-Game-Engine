using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Interfaces
{
    public interface IObjectAnimation
    {
 
        public void Update(GameTime gametime);
        public void Draw(SpriteBatch spriteBatch);
    }
}
