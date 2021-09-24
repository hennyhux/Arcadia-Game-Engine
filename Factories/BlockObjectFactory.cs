using GameSpace.GameObjects;
using GameSpace.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Factories
{
    public class BlockObjectFactory
    {

        private protected readonly Game1 game;

        public BlockObjectFactory(Game1 game)
        {
            this.game = game;
        }

        public IBlockObjects ReturnBrickBlockObject()
        {
            return new BrickBlock(game);
        }

        public IBlockObjects ReturnStairBlockObject()
        {
            return new StairBlock(game);
        }

    }
}
