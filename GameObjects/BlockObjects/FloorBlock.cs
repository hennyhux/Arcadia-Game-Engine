using GameSpace.Interfaces;
using GameSpace.States.BlockStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.GameObjects.BlockObjects
{
    public class FloorBlock : IBlockObjects
    {
        private IBlockStates state;

        public FloorBlock(Game1 game)
        {
            this.state = new FloorBlockStates(game);
        }
        public void Draw(SpriteBatch spritebatch, Vector2 location)
        {
            state.Draw(spritebatch, new Vector2(0, 0));
        }

        public void Trigger()
        {
            this.state.Initiate();
        }

        public void Update(GameTime gametime)
        {
            state.Update(gametime);
        }
    }
}
