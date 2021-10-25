using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.States
{
    public class StateBrickBlockBumped : IBlockStates
    {
        private ISprite sprite;

        public StateBrickBlockBumped(IGameObjects block)
        {
            sprite = new BumpAnimation(SpriteBlockFactory.GetInstance().ReturnUsedBlock().Texture, (int)block.Position.X, (int)block.Position.Y, 24);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch, location);
        }

        public void DrawBounds(SpriteBatch spriteBatch, Rectangle CollisionBox)
        {
            sprite.DrawBoundary(spriteBatch, CollisionBox);
        }

        public void Update(GameTime gametime)
        {
            sprite.Update(gametime);
        }
    }
}
