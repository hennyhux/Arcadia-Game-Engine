using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Interfaces
{
    public interface IEnemyObjects
    {
        public void Draw(SpriteBatch spritebatch, Vector2 location);
        public void Update(GameTime gametime);
        public void Trigger();
    }
}
