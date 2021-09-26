using GameSpace.Factories;
using GameSpace.Interfaces;
using GameSpace.States.BlockStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.GameObjects.BlockObjects
{
    public class HiddenBlock : IBlockObjects
    {
        private readonly IBlockStates state;

        public HiddenBlock(Game1 game)
        {
            this.state = new HiddenBlockState(game);
        }

        public void Draw(SpriteBatch spritebatch, Vector2 location)
        {
            this.state.Draw(spritebatch, location);
        }

        public void Trigger()
        {
            this.state.Initiate();
        }

        public void Update(GameTime gametime)
        {
            this.state.Update(gametime);
        }
    }
}
