using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.States
{
    public class StateBlockBumped : IBlockState
    {
        private IGameObjects block;

        public StateBlockBumped(IGameObjects block)
        {
            this.block = block;
            if (block.Sprite.Texture.Name.Equals("Blocks/QuestionBlock"))
            {
                block.Sprite = new BumpAnimation(BlockSpriteFactory.GetInstance().ReturnUsedBlock().Texture, (int)block.Position.X, (int)block.Position.Y);
            }

            else
            {
                block.Sprite = new BumpAnimation(block.Sprite.Texture, (int)block.Position.X, (int)block.Position.Y);
            }
        }
        public void Update(GameTime gametime)
        {
            
        }
    }
}
