using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

/*Currently, this method of checking every element in the list is an ineffective and inefficent way of collision detection,
 in the future, will use quadtrees, sweep and prune, or BSP trees. */
namespace GameSpace.EntitiesManager
{
    /*If a list is modified while it is being iterated over it will cause an exception*/
    public static class EntityManager
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

            HandleAllCollisions();
        }

        public static void MoveBlock(int blockID, int direction)
        {
            IGameObjects temp;
            for (int i = 0; i < gameObjects.Count; i++)
            {
                if (i == blockID)
                {
                    temp = gameObjects.ElementAt<IGameObjects>(i);
                    temp.SetPosition(new Vector2(-1, 0));
                }
            }
        }

        public static IGameObjects FindBlock(int blockID)
        {
            foreach (IGameObjects entity in gameObjects)
            {
                if (entity.ObjectID == blockID)
                {
                    return entity;
                }
            }
            return null; //null bad 
        }

        #region Collision Detection
        private static bool IsColliding(IGameObjects a, IGameObjects b)
        {

            return a.Rect.Intersects(b.Rect); //sweep aabb
        }

        private static bool IsOutOfBounds(IGameObjects a)
        {
            return a.Position.X < -1 || a.Position.Y < -1;
        }

        //Super inefficent method of detection, will change for future sprints 
        private static void HandleAllCollisions()
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

            foreach (IGameObjects entity in gameObjects)
            {
                if (IsOutOfBounds(entity))
                {
                    entity.HandleCollision(entity);
                }
            }
        }
        #endregion
    }
}


