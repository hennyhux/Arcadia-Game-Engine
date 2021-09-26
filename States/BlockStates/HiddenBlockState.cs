using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;


namespace GameSpace.States.BlockStates
{
    public class HiddenBlockState : IBlockStates
    {
        private ISprite sprite;
        private BlockSpriteFactory blockFactory;
        private bool triggered;
        public HiddenBlockState(Game1 game)
        {
            this.sprite = game.BlockFactory.ReturnHiddenBlock();
            triggered = false;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 Location)
        {
            if (!triggered)
            {
                sprite.Draw(spriteBatch, new Vector2(650, 200));
            }
        }

        public void Initiate()
        {
            triggered = !triggered;
        }

        public void Update(GameTime gametime)
        {
            sprite.Update(gametime);
        }
    }
}
