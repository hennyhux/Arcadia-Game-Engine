using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.States.BlockStates
{
    public class UsedBlockState : IBlockStates
    {
        private ISprite sprite;
        private BlockSpriteFactory blockFactory;
        private bool triggered;

        public UsedBlockState(Game1 game)
        {
            blockFactory = game.BlockFactory;
            this.sprite = blockFactory.ReturnUsedBlock(new Vector2(0, 0));
            triggered = false;
        }

        public void Initiate()
        {

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 Location)
        {
            sprite.Draw(spriteBatch, new Vector2(150, 150));
        }

        public void Update(GameTime gametime)
        {
            sprite.Update(gametime);
        }
    }
}
