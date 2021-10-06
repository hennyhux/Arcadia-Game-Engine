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
        public IGameObjects CreateBrickBlockObject()
        {
            return new BrickBlock(new Vector2(100, 150));
        }

        public IGameObjects CreateStairBlockObject()
        {
            return new StairBlock(new Vector2(300, 150));
        }

        public IGameObjects CreateFloorBlockObject()
        {
            return new FloorBlock(new Vector2(200, 150));
        }

        public IGameObjects CreateQuestionBlockObject()
        {
            return new QuestionBlock(new Vector2(400, 150));
        }

        public IGameObjects CreateHiddenBlockObject()
        {
            return new HiddenBlock(new Vector2(500, 150));
        }

        public IGameObjects CreateUsedBlockObject()
        {
            return new UsedBlock(new Vector2(600, 150));
        }
        #endregion

        #region Enemies 
        public IGameObjects CreateGoombaObject()
        {
            return new Goomba(new Vector2(100, 250));
        }

        public IGameObjects CreateGreenKoopaObject()
        {
            return new GreenKoopa(new Vector2(100, 300));
        }

        public IGameObjects CreateRedKoopaObject()
        {
            return new RedKoopa(new Vector2(100, 350));
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
