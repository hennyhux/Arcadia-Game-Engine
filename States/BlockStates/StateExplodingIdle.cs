using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.States
{
    public class StateExplodingIdleBlock : IObjectState
    {
        private IGameObjects block;

        public StateExplodingIdleBlock(IGameObjects block)
        {
            this.block = block;
            if (block.Sprite.Texture.Name.Equals("Blocks/BrickBlock"))
            {
                block.Sprite = new BumpAnimation(SpriteBlockFactory.GetInstance().ReturnShatterBlock().Texture, (int)block.Position.X, (int)block.Position.Y, 48);
            }

            else
            {
                block.Sprite = new BumpAnimation(block.Sprite.Texture, (int)block.Position.X, (int)block.Position.Y, 48);
            }
        }
        public void Update(GameTime gametime)
        {

        }
    }
}
