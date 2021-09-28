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
        private bool triggered;
        public HiddenBlockState(Game1 game)
        {
            this.sprite = BlockSpriteFactory.GetInstance().ReturnHiddenBlock();
            triggered = false;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 Location)
        {
            sprite.Draw(spriteBatch, new Vector2(500, 150));
        }

        public void Initiate()
        {
            triggered = true;
            sprite.SetVisible();

        }

        public void Update(GameTime gametime)
        {
            if(triggered)sprite.Update(gametime);
        }
    }
}
