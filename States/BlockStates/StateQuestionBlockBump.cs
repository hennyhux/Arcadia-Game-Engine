using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.States.BlockStates
{
    public class StateQuestionBlockBump : IBlockStates
    {
        private ISprite sprite;

        public StateQuestionBlockBump(IGameObjects gameObjects)
        {
            sprite = new BumpAnimation(SpriteBlockFactory.GetInstance().ReturnUsedBlock().Texture, (int)gameObjects.Position.X, (int)gameObjects.Position.Y, 24);
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch, location);
        }

        public void DrawBounds(SpriteBatch spriteBatch, Rectangle CollisionBox)
        {
            sprite.DrawBoundary(spriteBatch, CollisionBox);
        }

        public void Update(GameTime gameTime)
        {
            sprite.Update(gameTime);
        }
    }
}
