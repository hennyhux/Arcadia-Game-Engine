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

        public IItemObject ReturnStarObject()
        {
            return new Star(game);
        }

        public IItemObject ReturnCoinObject()
        {
            return new Coin(game);
        }

        public IItemObject ReturnFireFlowerObject()
        {
            return new FireFlower(game);
        }

        public IItemObject ReturnOneUpShroomObject()
        {
            return new OneUpShroom(game);
        }

        public IItemObject ReturnSuperShroomObject()
        {
            return new SuperShroom(game);
        }

    }
}
