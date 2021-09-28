using GameSpace.GameObjects.BlockObjects;
using GameSpace.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Factories
{
    public class BlockObjectFactory
    {

        private static protected Game1 instanceGame;

        private static BlockObjectFactory instance;

        public static BlockObjectFactory GetInstance(Game1 game)
        {
            if (instance == null)
            {
                instanceGame = game;
                instance = new BlockObjectFactory();
            }
            return instance;

        }

        private BlockObjectFactory()
        {

        }

        public IBlockObjects ReturnBrickBlockObject()
        {
            return new BrickBlock(instanceGame);
        }

        public IBlockObjects ReturnStairBlockObject()
        {
            return new StairBlock(instanceGame);
        }

        public IBlockObjects ReturnFloorBlockObject()
        {
            return new FloorBlock(instanceGame);
        }

        public IBlockObjects ReturnQuestionBlockObject()
        {
            return new QuestionBlock(instanceGame);
        }

        public IBlockObjects ReturnHiddenBlockObject()
        {
            return new HiddenBlock(instanceGame);
        }

        public IBlockObjects ReturnUsedBlockObject()
        {
            return new UsedBlock(instanceGame);
        }

    }
}
