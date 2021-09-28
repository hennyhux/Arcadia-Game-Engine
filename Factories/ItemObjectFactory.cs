using GameSpace.GameObjects.ItemObjects;
using GameSpace.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Factories
{
    public class ItemObjectFactory
    {

        private static protected Game1 instanceGame;

        private static ItemObjectFactory instance;

        public static ItemObjectFactory GetInstance(Game1 game)
        {
            if (instance == null)
            {
                instanceGame = game;
                instance = new ItemObjectFactory();
            }
            return instance;

        }
        private ItemObjectFactory()
        {

        }

        public IItemObjects ReturnStarObject()
        {
            return new Star(instanceGame);
        }

        public IItemObjects ReturnCoinObject()
        {
            return new Coin(instanceGame);
        }

        public IItemObjects ReturnFireFlowerObject()
        {
            return new FireFlower(instanceGame);
        }

        public IItemObjects ReturnOneUpShroomObject()
        {
            return new OneUpShroom(instanceGame);
        }

        public IItemObjects ReturnSuperShroomObject()
        {
            return new SuperShroom(instanceGame);
        }

    }
}
