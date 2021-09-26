using GameSpace.GameObjects.BlockObjects;
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

        public IBlockObjects ReturnFloorBlockObject()
        {
            return new FloorBlock(game);
        }

        public IBlockObjects ReturnQuestionBlockObject()
        {
            return new QuestionBlock(game);
        }

        public IBlockObjects ReturnHiddenBlockObject()
        {
            return new HiddenBlock(game);
        }

        public IBlockObjects ReturnUsedBlockObject()
        {
            return new UsedBlock(game);
        }

    }
}
