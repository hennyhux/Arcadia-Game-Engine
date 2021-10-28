using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Abstracts
{
    public abstract class AbstractBlockStates : IBlockStates
    {
        internal ISprite sprite;
        public virtual void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch, location);
        }

        public virtual void DrawBounds(SpriteBatch spriteBatch, Rectangle CollisionBox)
        {
            sprite.DrawBoundary(spriteBatch, CollisionBox);
        }

        public virtual void Update(GameTime gameTime)
        {
            sprite.Update(gameTime);
        }
    }
}
