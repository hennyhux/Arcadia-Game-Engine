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

        public IGameObjects ReturnBrickBlockObject()
        {
            return new BrickBlock(instanceGame);
        }

        public IGameObjects ReturnStairBlockObject()
        {
            return new StairBlock(instanceGame);
        }

        public IGameObjects ReturnFloorBlockObject()
        {
            return new FloorBlock(instanceGame);
        }

        public IGameObjects ReturnQuestionBlockObject()
        {
            return new QuestionBlock(instanceGame);
        }

        public IGameObjects ReturnHiddenBlockObject()
        {
            return new HiddenBlock(instanceGame);
        }

        public IGameObjects ReturnUsedBlockObject()
        {
            return new UsedBlock(instanceGame);
        }
    }
}
