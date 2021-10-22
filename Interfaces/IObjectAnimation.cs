using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Interfaces
{
    public interface IObjectAnimation
    {
        public void PlayAnimation();
        public void Update();
        public void Draw(SpriteBatch spriteBatch);
    }
}
