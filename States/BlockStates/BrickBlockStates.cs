using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameSpace;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.States.BlockStates
{
    public class BrickBlockStates : IBlockStates
    {
        private ISprite sprite;
        private BlockFactory blockFactory;
        private bool triggered;

        public BrickBlockStates(Game1 game)
        {
            blockFactory = game.BlockFactory;
            this.sprite = blockFactory.ReturnBrickBlock();
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
