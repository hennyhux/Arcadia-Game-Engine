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

        #region Draw and Sprite Managing
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

            HandleAllCollisions();
        }

        public static void Update(GameTime gametime)
        {
            foreach (IGameObjects entity in gameEntities)
            {
                entity.Update(gametime);
            }
        }
        public static void ToggleCollisionBox()
        {
            foreach (IGameObjects entity in gameEntities)
            {
                entity.ToggleCollisionBoxes();
            }
        }
        #endregion

        #region Testing Methods
        public static void MoveItem(int blockID, int direction)
        {
            if (direction == (int)ControlDirection.UP) FindItem(blockID).SetPosition(new Vector2(0 , -1));
            if (direction == (int)ControlDirection.DOWN) FindItem(blockID).SetPosition(new Vector2(0, 1));
            if (direction == (int)ControlDirection.RIGHT) FindItem(blockID).SetPosition(new Vector2(1, 0));
            if (direction == (int)ControlDirection.LEFT) FindItem(blockID).SetPosition(new Vector2(-1, 0));
        }

        public static IGameObjects FindItem(int ItemID)
        {
            foreach (IGameObjects entity in gameEntities)
            {
                if (entity.ObjectID == ItemID)
                {
                    return entity;
                }
            }
            return null; //lets try not to return null
        }

        #endregion

        #region Collision Detection
        private static bool IsColliding(IGameObjects a, IGameObjects b)
        {
            if (a.Position.X + 5 >= b.Position.X || a.Position.X + 5 <= b.Position.X ||
                a.Position.Y + 5 >= b.Position.Y || a.Position.Y - 5 <= b.Position.Y)
            {
                return a.CollisionBox.Intersects(b.CollisionBox);
            }

            else { return false; } 
        }

        public static float SweeptAABB(IGameObjects a, IGameObjects b)
        {
            return 0f;
        }
        
        public static int DetectCollisionDirection(IGameObjects a, IGameObjects b)
        {
            Rectangle overLappedRectangle = Rectangle.Intersect(a.CollisionBox, b.CollisionBox);
            int direction = 0;

            if (!overLappedRectangle.IsEmpty)
            {
                if (overLappedRectangle.Width > overLappedRectangle.Height && a.Position.Y < b.Position.Y)
                {
                    direction = (int)CollisionDirection.DOWN;
                }

                if (overLappedRectangle.Width > overLappedRectangle.Height && a.Position.Y > b.Position.Y)
                {
                    direction = (int)CollisionDirection.UP;
                }

                if (overLappedRectangle.Height > overLappedRectangle.Width && a.Position.X > b.Position.X)
                {
                    direction = (int)CollisionDirection.RIGHT;
                }

                if (overLappedRectangle.Height > overLappedRectangle.Width && a.Position.X < b.Position.X)
                {
                    direction = (int)CollisionDirection.LEFT;
                }
            }

            return direction;
        }
        private static bool IsOutOfBounds(IGameObjects a)
        {
            return a.Position.X < 0 || a.Position.Y > 600;
        }

        //Super inefficent method of detection, if the amount of entites in the list is huge, it will take a lot of resources 
        //will change for future sprints  
        //need to also take in consideration the DIRECTION of collision...
        //if the overlapped rectange has a longer width than height, then it has either collided on top or bottom
        //if the overlapped rectangle has a taller height than width, then it has either collided on left or right 
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


