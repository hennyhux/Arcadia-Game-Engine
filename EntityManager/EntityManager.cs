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

        private static List<IGameObjects> gameEntities = new List<IGameObjects>();

        public static int Count { get { return gameEntities.Count; } }

        public static void AddEntity(IGameObjects gameObject)
        {
            gameEntities.Add(gameObject);
        }

        public static void LoadList(List<IGameObjects> objectList)
        {
            gameEntities = objectList;
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (IGameObjects entity in gameEntities)
            {
                entity.Draw(spriteBatch);
            }
        }

        public static void Update(GameTime gametime)
        {
            foreach (IGameObjects entity in gameEntities)
            {
                entity.Update(gametime);
            }

            HandleAllCollisions();
        }

        public static void MoveBlock(int blockID, int direction)
        {

            IGameObjects temp;
            for (int i = 0; i < gameEntities.Count; i++)
            {
                if (i == blockID)
                {
                    temp = gameEntities.ElementAt<IGameObjects>(i);
                    temp.SetPosition(new Vector2(0, 2));
                }
            }
        }

        public static IGameObjects FindBlock(int blockID)
        {
            foreach (IGameObjects entity in gameEntities)
            {
                if (entity.ObjectID == blockID)
                {
                    return entity;
                }
            }
            return null; //null bad 
        }

        public static void ToggleCollisionBox()
        {
            foreach(IGameObjects entity in gameEntities)
            {
                entity.ToggleCollisionBoxes();
            }
        }

        #region Collision Detection
        private static bool IsColliding(IGameObjects a, IGameObjects b)
        {

            return a.CollisionBox.Intersects(b.CollisionBox); //sweep aabb
        }

        private static bool IsOutOfBounds(IGameObjects a)
        {
            return a.Position.X < -1 || a.Position.Y < -1;
        }

        //Super inefficent method of detection, will change for future sprints 
        private static void HandleAllCollisions()
        {
            for (int i = 0; i < gameEntities.Count; i++)
                for (int j = i + 1; j < gameEntities.Count; j++)
                {
                    if (IsColliding(gameEntities[i], gameEntities[j]))
                    {
                        gameEntities[i].HandleCollision(gameEntities[j]);
                        gameEntities[j].HandleCollision(gameEntities[i]);
                    }
                }

            foreach (IGameObjects entity in gameEntities)
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


