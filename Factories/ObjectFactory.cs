using GameSpace.GameObjects.BlockObjects;
using GameSpace.GameObjects.EnemyObjects;
using GameSpace.GameObjects.ItemObjects;
using GameSpace.Interfaces;
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


        //    public IGameObjects CreateStarObject()
        //    {
        //        return new Star(instanceGame);
        //    }

        //    public IGameObjects CreateCoinObject()
        //    {
        //        return new Coin(instanceGame);
        //    }

        //    public IGameObjects CreateFireFlowerObject()
        //    {
        //        return new FireFlower(instanceGame);
        //    }

        //    public IGameObjects CreateOneUpShroomObject()
        //    {
        //        return new OneUpShroom(instanceGame);
        //    }

        //    public IGameObjects CreateSuperShroomObject()
        //    {
        //        return new SuperShroom(instanceGame);
        //    }

        //}
    }
}
