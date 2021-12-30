using GameSpace.Interfaces;
using GameSpace.Objects.EnemyObjects;
using Microsoft.Xna.Framework;

namespace GameSpace.Factories
{
    public class EnemyFactory
    {
        private static EnemyFactory instance;

        public static EnemyFactory GetInstance()
        {
            if (instance == null)
            {
                instance = new EnemyFactory();
            }
            return instance;
        }

        private EnemyFactory()
        {

        }

        public IGameObjects CreateGreenKoopaObject(Vector2 location)
        {
            return new Koopa(location);
        }
    }
}
