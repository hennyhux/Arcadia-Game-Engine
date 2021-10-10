using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Interfaces
{
    public interface IEnemyStates
    {
        public void Draw(SpriteBatch spritebatch, Texture2D texture);
        public void Update(GameTime gametime);
    }
}