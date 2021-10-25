using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.States.BlockStates
{
    public class StateBrickBlockIdle : IBlockStates
    {
        private ISprite sprite;

        public StateBrickBlockIdle()
        {
            this.sprite = SpriteBlockFactory.GetInstance().ReturnBrickBlock();
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
