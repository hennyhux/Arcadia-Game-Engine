using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*Currently, this method of checking every element in the list is an ineffective and inefficent way of collision detection,
 in the future, will use quadtrees, sweep and prune, or BSP trees. */
namespace GameSpace.EntitiesManager
{
    /*If a list is modified while it is being iterated over it will cause an exception*/
    static class EntityManager
    {
        private static List<IGameObjects> gameObjects = new List<IGameObjects>();

        public static int Count { get { return gameObjects.Count; } }

        public static void AddEntity(IGameObjects gameObject)
        {
            gameObjects.Add(gameObject);
        }

        public static void CopyList(List<IGameObjects> objectList)
        {
            gameObjects = objectList;
        }

        public static IGameObjects AccessItem(int index)
        {
            return gameObjects.ElementAt<IGameObjects>(index);
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (IGameObjects entity in gameObjects)
            {
                entity.Draw(spriteBatch);
            }
        }
        
        public static void Update(GameTime gametime)
        {
            foreach (IGameObjects entity in gameObjects)
            {
                entity.Update(gametime);
            }
        }

        private static bool IsColliding(IGameObjects a, IGameObjects b)
        {
            if (a.Position.Y > b.Position.Y && a.Position.X == b.Position.X)
            {
                return true;
            }

            return false;
        }

        public static void HandleCollisions()
        {
            for (int i = 0; i < gameObjects.Count; i++)
                for (int j = i + 1; j < gameObjects.Count; j++)
                {
                    if (IsColliding(gameObjects[i], gameObjects[j]))
                    {
                        gameObjects[i].HandleCollision(gameObjects[j]);
                        gameObjects[j].HandleCollision(gameObjects[i]);
                    }
                }
        }
    }
}
