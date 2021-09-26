using GameSpace.GameObjects.EnemyObjects;
using GameSpace.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Factories
{
    public class EnemyObjectFactory
    {
        private protected readonly Game1 game;

        public EnemyObjectFactory(Game1 game)
        {
            this.game = game;
        }

        public IEnemyObjects ReturnGoombaObject(Game1 game)
        {
            return new Goomba(game);
        }
    }
}
