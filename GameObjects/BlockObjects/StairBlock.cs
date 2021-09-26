using GameSpace.Interfaces;
using GameSpace.States.BlockStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.GameObjects.BlockObjects
{
    public class StairBlock : IBlockObjects
    {
        private IBlockStates state;

        public StairBlock(Game1 game)
        {
            this.state = new StairBlockState(game);
        }

        public void Draw(SpriteBatch spritebatch, Vector2 location)
        {
            state.Draw(spritebatch, new Vector2(200, 150));
        }

        public void Trigger()
        {
            state.Initiate();
        }

        public void Update(GameTime gametime)
        {
            state.Update(gametime);
        }
    }
}
