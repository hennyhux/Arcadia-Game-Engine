using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.States.BlockStates
{
    public class FloorBlockStates : IBlockStates
    {
        private ISprite sprite;
        private BlockSpriteFactory blockFactory;
        private bool triggered;

        public FloorBlockStates(Game1 game)
        {
            blockFactory = game.BlockFactory;
            this.sprite = blockFactory.ReturnFloorBlock();
            triggered = false;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 Location)
        {
            sprite.Draw(spriteBatch, new Vector2(250, 150));
        }

        public void Initiate()
        {
            
        }

        public void Update(GameTime gametime)
        {
            sprite.Update(gametime);
        }
    }
}
