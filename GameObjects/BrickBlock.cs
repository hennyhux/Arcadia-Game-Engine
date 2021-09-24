using GameSpace.Interfaces;
using GameSpace.States.BlockStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.GameObjects
{
    public class BrickBlock
    {
        private IBlockStates state;

        public BrickBlock(Game1 game)
        {
            this.state = new BrickBlockStates(game);
        }

        public void Draw(SpriteBatch spritebatch, Vector2 location)
        {
            state.Draw(spritebatch, location);
        }

        public void Update(GameTime gametime)
        {
            state.Update(gametime);
        }

        public void Trigger()
        {
            state.Initiate();
        }

    }
}
