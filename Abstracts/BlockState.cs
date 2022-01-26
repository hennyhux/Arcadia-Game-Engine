using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.Abstracts
{
    public abstract class BlockState : IBlockStates
    {
        public ISprite StateSprite { get; set; }
        public virtual void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            StateSprite.Draw(spriteBatch, location);
        }

        public virtual void DrawBounds(SpriteBatch spriteBatch, Rectangle CollisionBox)
        {
            StateSprite.DrawBoundary(spriteBatch, CollisionBox);
        }

        public virtual void Update(GameTime gameTime)
        {
            StateSprite.Update(gameTime);
        }

        public virtual void Trigger()
        {

        }
        public virtual void RevealItem()
        {

        }
    }
}
