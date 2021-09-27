using GameSpace.GameObjects.EnemyObjects;
using GameSpace.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Factories
{
    public class EnemyObjectFactory
    {
        private static EnemyObjectFactory instance;
        public static EnemyObjectFactory GetInstance()
        {
            if (instance == null)
            {
                instance = new EnemyObjectFactory();
            }

            return instance;
        }

        private EnemyObjectFactory()
        {
            
        }

        public IEnemyObjects ReturnGoombaObject()
        {
            return new Goomba();
        }

        public IEnemyObjects ReturnGreenKoopaObject()
        {
            return new GreenKoopa();
        }

        public IEnemyObjects ReturnRedKoopaObject()
        {
            return new RedKoopa();
        }

    }
}
