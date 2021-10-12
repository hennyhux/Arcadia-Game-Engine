using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.Interfaces;
using GameSpace.States.MarioStates;
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

        #region Entity Managing
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

        #region Moving and Finding Entities
        public static void MoveItem(int ID, int direction)
        {
            if (direction == (int)ControlDirection.UP) FindItem(ID).SetPosition(new Vector2(0, -1));
            if (direction == (int)ControlDirection.DOWN) FindItem(ID).SetPosition(new Vector2(0, 1));
            if (direction == (int)ControlDirection.RIGHT) FindItem(ID).SetPosition(new Vector2(1, 0));
            if (direction == (int)ControlDirection.LEFT) FindItem(ID).SetPosition(new Vector2(-1, 0));
        }

        private static bool SweeptAABBLeft(int ID)
        {
            for (int i = 0; i < EntityManager.Count; i++)
            {
                if (FindItem((int)ID).Position.X - 1 >= gameEntities[i].Position.X)
                {
                    FindItem((int)ID).Position = (new Vector2(FindItem((int)ID).Position.X - 1, FindItem((int)ID).Position.Y));
                }

                else
                {
                    FindItem(ID).SetPosition(new Vector2(-1, 0));
                }
            }

            return true;
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

        public static IMarioActionStates GetCurrentMarioState()
        {
            Mario mario = (Mario)EntityManager.FindItem((int)AvatarID.MARIO);
            return mario.marioActionState;
        }

        public static bool IsCurrentlyBigMario()
        {
            Mario mario = (Mario)EntityManager.FindItem((int)AvatarID.MARIO);
            return (mario.marioActionState is BigMarioFallingState ||
                    mario.marioActionState is BigMarioJumpingState ||
                    mario.marioActionState is BigMarioRunningState ||
                    mario.marioActionState is BigMarioStandingState ||
                    mario.marioActionState is FireMarioFallingState ||
                    mario.marioActionState is FireMarioJumpingState ||
                    mario.marioActionState is FireMarioRunningState ||
                    mario.marioActionState is FireMarioStandingState);
        }

        #endregion

        #region Collision Detection
        private static bool IntersectAABB(IGameObjects a, IGameObjects b)
        {

            if (a.CollisionBox.X + a.CollisionBox.Width < b.CollisionBox.X || a.CollisionBox.X > b.CollisionBox.X + b.CollisionBox.Width)
            {
                return false;
            }

            if (a.CollisionBox.Y + a.CollisionBox.Height < b.CollisionBox.Y || a.CollisionBox.Y > b.CollisionBox.Y + b.CollisionBox.Height)
            {
                return false; 
            }


            else { return a.CollisionBox.Intersects(b.CollisionBox);  } 
        }

        public static bool SweeptAABB(IGameObjects a, IGameObjects b)
        {
            return
                (a.CollisionBox.X + a.CollisionBox.Width + 10 >= b.CollisionBox.X
                || a.CollisionBox.X - 10 <= b.CollisionBox.X + b.CollisionBox.Width
                || a.CollisionBox.Y + a.CollisionBox.Height + 10 >= b.CollisionBox.Y
                || a.CollisionBox.Y - 10 <= b.CollisionBox.Y + b.CollisionBox.Height);
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
                        if (IntersectAABB(gameEntities[i], gameEntities[j]))
                        {
                            gameEntities[i].HandleCollision(gameEntities[j]);
                            gameEntities[j].HandleCollision(gameEntities[i]);
                        }
                }
        }
        #endregion
    }
}


