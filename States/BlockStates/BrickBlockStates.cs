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
        private bool triggered;
        private readonly Game1 game;

        public BrickBlockStates(Game1 game)
        {
            this.game = game;
            this.sprite = BlockSpriteFactory.GetInstance().ReturnBrickBlock();
            triggered = false;
        }

        public void Initiate()
        {
            triggered = true;
            this.sprite = BlockSpriteFactory.GetInstance().ReturnUsedBlock(new Vector2(100, 150));
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            if (!triggered) sprite.Draw(spriteBatch, new Vector2(100, 150));

            else sprite.Draw(spriteBatch, location);
        }

        public void Update(GameTime gametime)
        {
            if (triggered)sprite.Update(gametime);
        }

    }
}
