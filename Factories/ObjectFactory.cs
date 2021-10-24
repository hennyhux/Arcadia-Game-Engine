using GameSpace.GameObjects.BlockObjects;
using GameSpace.GameObjects.EnemyObjects;
using GameSpace.GameObjects.ItemObjects;
using GameSpace.GameObjects.ExtraItemsObjects;
using GameSpace.Interfaces;
using GameSpace.Objects.BlockObjects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Factories
{
    public class ObjectFactory
    {

        private static protected GameRoot instanceGame;

        private static ObjectFactory instance;

        public static ObjectFactory GetInstance()
        {
            if (instance == null)
            {
                instance = new ObjectFactory();
            }
            return instance;
        }

        private ObjectFactory()
        {

        }

        #region Blocks
        public IGameObjects CreateBrickBlockObject(Vector2 location)
        {
            return new BrickBlock(location);
        }

        public IGameObjects CreateStairBlockObject(Vector2 location)
        {
            return new StairBlock(location);
        }

        public IGameObjects CreateFloorBlockObject(Vector2 location)
        {
            return new FloorBlock(location);
        }

        public IGameObjects CreateQuestionBlockObject(Vector2 location)
        {
            return new QuestionBlock(location);
        }

        public IGameObjects CreateHiddenBlockObject(Vector2 location)
        {
            return new HiddenBlock(location);
        }

        public IGameObjects CreateUsedBlockObject(Vector2 location)
        {
            return new UsedBlock(location);
        }

        public IGameObjects CreateCoinBrickBlock(Vector2 location)
        {
            return new CoinBrickBlock(location);
        }

        #endregion

        #region Enemies 
        public IGameObjects CreateGoombaObject(Vector2 location)
        {
            return new Goomba(location);
        }

        public IGameObjects CreateGreenKoopaObject(Vector2 location)
        {
            return new GreenKoopa(location);
        }

        public IGameObjects CreateRedKoopaObject(Vector2 location)
        {
            return new RedKoopa(location);
        }

        #endregion

        #region Items
        public IGameObjects CreateCoinObject(Vector2 location)
        {
            return new Coin(location);
        }

        public IGameObjects CreateStarObject(Vector2 location)
        {
            return new Star(location);
        }

        public IGameObjects CreateFireFlowerObject(Vector2 location)
        {
            return new FireFlower(location);
        }

        public IGameObjects CreateOneUpShroomObject(Vector2 location)
        {
            return new OneUpShroom(location);
        }

        public IGameObjects CreateSuperShroomObject(Vector2 location)
        {
            return new SuperShroom(location);
        }
        #endregion

        #region Items
        public IGameObjects CreateBigPipeObject(Vector2 location)
        {
            return new BigPipe(location);
        }
        #endregion
    }
}
