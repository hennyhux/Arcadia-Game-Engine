using GameSpace.Abstracts;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.Interfaces;
using GameSpace.Objects.BlockObjects;
using Microsoft.Xna.Framework;

namespace GameSpace.Factories
{
    public class BlockObjectsFactory
    {
        private static BlockObjectsFactory instance;

        public static BlockObjectsFactory GetInstance()
        {
            if (instance == null)
            {
                instance = new BlockObjectsFactory();
            }
            return instance;
        }

        private BlockObjectsFactory()
        {

        }


        public IGameObjects CreateBrickBlockObject(Vector2 location)
        {
            return new BrickBlock(location);
        }

        public IGameObjects CreateBrickBlockWithItem(Vector2 location, IGameObjects item)
        {
            return new BrickBlockWithItem(location, (Item)item);
        }
        public IGameObjects CreateQuestionBlockObject(Vector2 location)
        {
            return new QuestionBlock(location, null);
        }

        public IGameObjects CreateQuestionBlockWithItem(Vector2 location, IGameObjects item)
        {
            return new QuestionBlock(location, (Item)item);
        }

        public IGameObjects CreateFloorBlockObject(Vector2 location)
        {
            return new FloorBlock(location);
        }

        public IGameObjects CreateStairBlockObject(Vector2 location)
        {
            return new StairBlock(location);
        }

        public IGameObjects CreateUsedBlockObject(Vector2 location)
        {
            return new UsedBlock(location);
        }

        public IGameObjects CreateHiddenBlockObject(Vector2 location)
        {
            return new HiddenBlock(location);
        }

        public IGameObjects CreateHiddenLevelBrickBlockObject(Vector2 location)
        {
            return new HiddenLevelBrickBlock(location);
        }

        public IGameObjects CreateHiddenLevelFloorBlockObject(Vector2 location)
        {
            return new HiddenLevelFloorBlock(location);
        }


    }
}