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
            this.sprite = BlockSpriteFactory.GetInstance().ReturnUsedBlock(new Vector2(400, 150));
            triggered = false;
        }

        public void Initiate()
        {

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            sprite.Draw(spriteBatch, location);
        }

        public void Update(GameTime gametime)
        {
            if(triggered)sprite.Update(gametime);
        }
    }
}
