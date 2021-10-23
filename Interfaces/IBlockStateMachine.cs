using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Interfaces
{
    public interface IBlockStateMachine
    {
        public ISprite FindSprite();
        public void SetSprite(ISprite sprite);
        public void Trigger();
        public void Draw(SpriteBatch spriteBatch, Vector2 location);
        public void Update(GameTime gametime);
        public void SetLocationRectangle();
        public void SetLocationVector();
    }
}
