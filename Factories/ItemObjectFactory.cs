using GameSpace.GameObjects.ItemObjects;
using GameSpace.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Factories
{
    public class ItemObjectFactory
    {

        private protected readonly Game1 game;

        public ItemObjectFactory(Game1 game)
        {
            this.game = game;
        }

        public IItemObjects ReturnStarObject()
        {
            return new Star(game);
        }

        public IItemObjects ReturnCoinObject()
        {
            return new Coin(game);
        }

        public IItemObjects ReturnFireFlowerObject()
        {
            return new FireFlower(game);
        }

        public IItemObjects ReturnOneUpShroomObject()
        {
            return new OneUpShroom(game);
        }

        public IItemObjects ReturnSuperShroomObject()
        {
            return new SuperShroom(game);
        }

    }
}
