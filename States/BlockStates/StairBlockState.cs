using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.States.BlockStates
{
    public class StairBlockState : IBlockStates
    {
        private ISprite sprite;
        private BlockSpriteFactory blockFactory;
        private bool triggered;

        public StairBlockState(Game1 game)
        {
            blockFactory = game.BlockFactory;
            this.sprite = blockFactory.ReturnStairBlock();
            triggered = false;
        }

        public void Initiate()
        {
            this.sprite = blockFactory.ReturnFloorBlock();
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 Location)
        {
            sprite.Draw(spriteBatch, new Microsoft.Xna.Framework.Vector2(150, 150));
        }

        public void Update(GameTime gametime)
        {
            sprite.Update(gametime);
        }


    }
}
