using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Interfaces
{
    public interface IDIsplayPanel
    {
        bool IsEnabled { get; set; }

        void Update();

        void Draw(SpriteBatch spritebatch);
    }
}
