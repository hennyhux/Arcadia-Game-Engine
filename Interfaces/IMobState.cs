using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Interfaces
{
    public interface IMobState
    {
        public ISprite StateSprite { get; set; }
        public void Draw(SpriteBatch spritebatch, Vector2 position);
        public void DrawBoundingBox(SpriteBatch spritebatch, Rectangle collisionBox);
        public void Trigger();
        public void Update(GameTime gametime);
    }
}
