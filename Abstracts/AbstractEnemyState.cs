using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameSpace.Abstracts
{
    public abstract class AbstractEnemyState : IEnemyState
    {
        public ISprite StateSprite { get; set; }
        public bool CollidedWithMario { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public virtual void Draw(SpriteBatch spritebatch, Vector2 location)
        {
            StateSprite.Draw(spritebatch, location);
        }

        public virtual void DrawBoundaries(SpriteBatch spritebatch, Rectangle destination)
        {
            StateSprite.DrawBoundary(spritebatch, destination);
        }

        public virtual void Trigger()
        {

        }

        public virtual void Update(GameTime gametime)
        {
            StateSprite.Update(gametime);
        }
    }
}
