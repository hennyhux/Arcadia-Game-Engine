using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.States.BlockStates
{
    public class StateQuestionBlockIdle : IBlockStates
    {
        private readonly ISprite sprite;

        public StateQuestionBlockIdle()
        {
            sprite = SpriteBlockFactory.GetInstance().ReturnQuestionBlock();
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch, location);
        }

        public void DrawBounds(SpriteBatch spriteBatch, Rectangle CollisionBox)
        {
            sprite.DrawBoundary(spriteBatch, CollisionBox);
        }

        public void RevealItem()
        {
            throw new System.NotImplementedException();
        }

        public void Trigger()
        {
            throw new System.NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
            sprite.Update(gameTime);
        }
    }
}
