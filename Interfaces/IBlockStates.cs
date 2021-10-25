using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Interfaces
{
    public interface IBlockStates
    {
        public void Draw(SpriteBatch spriteBatch, Vector2 location);
        public void Update(GameTime gameTime);
        public void DrawBounds(SpriteBatch spriteBatch, Rectangle CollisionBox);
    }
}
