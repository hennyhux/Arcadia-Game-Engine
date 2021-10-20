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
        private static List<IGameObjects> prunedList = new List<IGameObjects>();
        private static IGameObjects mario;
        private static Vector2 marioCurrentLocation;
        private static IGameObjects[,] gameSpace = new IGameObjects[15, 25];


        #region Entity Managing
        public static void AddEntity(IGameObjects gameObject)
        {
            gameEntities.Add(gameObject);
        }

        public static void LoadList(List<IGameObjects> objectList)
        {
            gameEntities = objectList;
            
        }

        public static List<IGameObjects> GetEntityList()
        {
            return gameEntities;
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (IGameObjects entity in gameEntities)
            {
                entity.Draw(spriteBatch);
            }
            SweepAndPrune();
            //HandleAllCollisions();
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

            {
                if (direction == (int)ControlDirection.UP) FindItem(ID).SetPosition(new Vector2(0, -1));
                if (direction == (int)ControlDirection.DOWN) FindItem(ID).SetPosition(new Vector2(0, 1));
                if (direction == (int)ControlDirection.RIGHT) FindItem(ID).SetPosition(new Vector2(1, 0));
                if (direction == (int)ControlDirection.LEFT) FindItem(ID).SetPosition(new Vector2(-1, 0));
            }
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

        public static void SweepAndPrune()
        {
            mario = EntityManager.FindItem((int)AvatarID.MARIO);
            marioCurrentLocation = mario.Position;
            Debug.WriteLine("SIZE OF DEEZ  " + mario.Position.X + "   "+ mario.Position.Y);
            foreach (IGameObjects entity in gameEntities)
            {
                if (entity.Position.X - 100 >= marioCurrentLocation.X ||
                    entity.Position.X + 100 >= marioCurrentLocation.X ||
                    entity.Position.Y - 100 >= marioCurrentLocation.Y ||
                    entity.Position.Y + 100 >= marioCurrentLocation.Y) prunedList.Add(entity);
            }

            for (int i = 0; i < prunedList.Count; i++)
                for (int j = i + 1; j < prunedList.Count; j++)
                {
                    if (IntersectAABB(prunedList[i], prunedList[j]))
                    {
                        prunedList[i].HandleCollision(prunedList[j]);
                        prunedList[j].HandleCollision(prunedList[i]);
                    }
                }
            Debug.WriteLine("SIZE OF DEEZ NUTS " + prunedList.Count);
            prunedList.Clear();

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

            else { return a.CollisionBox.Intersects(b.CollisionBox); }
        }

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


    public class EntityFinder
    {
        private static readonly EntityFinder Instance = new EntityFinder();
        public static EntityFinder GetInstance()
        {
            return Instance;
        }

        private EntityFinder()
        {
            
        }

        public IGameObjects FindItem(int ItemID)
        {
            return null;
        }

    }
}



