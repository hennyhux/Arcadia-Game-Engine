using GameSpace.Camera2D;
using GameSpace.Enums;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.GameObjects.EnemyObjects;
using GameSpace.GameObjects.ItemObjects;
using GameSpace.Interfaces;
using GameSpace.States.MarioStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace GameSpace.EntitiesManager
{
    //ROADMAP: convert this static class into four other classes (singleton pattern) for sprint 4 
    public static class EntityManager
    {
        private static readonly List<IGameObjects> gameEntities = new List<IGameObjects>();
        private static readonly List<IGameObjects> prunedList = new List<IGameObjects>();
        private static List<IGameObjects> copyPrunedList = new List<IGameObjects>();
        private static readonly List<IObjectAnimation> animationList = new List<IObjectAnimation>();
        private static readonly List<IGameObjects> listOfWarpPipes = new List<IGameObjects>();
        //BackGround Stuff
        private static readonly List<IGameObjects> backgroundList = new List<IGameObjects>();
        private static IGameObjects mario;
        private static Vector2 marioCurrentLocation;
        public static Camera Camera { get; set; }


        #region Entity Managing
        public static void AddEntity(IGameObjects gameObject)
        {
            gameEntities.Add(gameObject);
        }



        public static List<IGameObjects> GetWarpPipes()
        {
            return listOfWarpPipes;
        }


        #endregion

        #region Moving and Finding Entities

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

        public static bool IsGoingToFall(Goomba enemy)
        {
            bool gonnaFall = true;
            foreach (IGameObjects entity in copyPrunedList)
            {
                if (enemy.ExpandedCollisionBox.Intersects(entity.CollisionBox) && entity.ObjectID != enemy.ObjectID && entity.ObjectID != (int)AvatarID.MARIO)
                {
                    gonnaFall = false;
                    break;
                }
            }
            return gonnaFall;
        }

        public static bool IsGoingToFall(RedKoopa enemy)
        {
            bool gonnaFall = true;
            foreach (IGameObjects entity in copyPrunedList)
            {
                if (enemy.ExpandedCollisionBox.Intersects(entity.CollisionBox) && entity.ObjectID != enemy.ObjectID && entity.ObjectID != (int)AvatarID.MARIO)
                {
                    gonnaFall = false;
                    break;
                }
            }
            return gonnaFall;
        }

        public static bool IsGoingToFall(GreenKoopa enemy)
        {
            bool gonnaFall = true;
            foreach (IGameObjects entity in copyPrunedList)
            {
                if (enemy.ExpandedCollisionBox.Intersects(entity.CollisionBox) && entity.ObjectID != enemy.ObjectID && entity.ObjectID != (int)AvatarID.MARIO)
                {
                    gonnaFall = false;
                    break;
                }
            }
            return gonnaFall;
        }

        public static bool IsGoingToFall(Mario player)
        {
            bool gonnaFall = true;
            foreach (IGameObjects entity in copyPrunedList)
            {
                if (player.ExpandedCollisionBox.Intersects(entity.CollisionBox) && entity.ObjectID == (int)AvatarID.MARIO)
                {
                    gonnaFall = false;
                    break;
                }
            }
            return gonnaFall;
        }

        public static bool IsGoingToFall(IGameObjects fallingObject)
        {
            bool gonnaFall = true;


            if (fallingObject is OneUpShroom)
            {
                OneUpShroom copy = (OneUpShroom)fallingObject;
                foreach (IGameObjects entity in copyPrunedList)
                {
                    if (copy.ExpandedCollisionBox.Intersects(entity.CollisionBox) && entity.ObjectID != copy.ObjectID)
                    {
                        gonnaFall = false;
                        break;
                    }
                }
            }




            if (fallingObject is SuperShroom)
            {
                SuperShroom copy = (SuperShroom)fallingObject;
                foreach (IGameObjects entity in copyPrunedList)
                {
                    if (copy.ExpandedCollisionBox.Intersects(entity.CollisionBox) && entity.ObjectID != copy.ObjectID)
                    {
                        gonnaFall = false;
                        break;
                    }
                }
            }





            return gonnaFall;
        }
        /* public static Boolean IsGoingToFall(Mario enemy)
         {
             Boolean gonnaFall = true;
             foreach (IGameObjects entity in copyPrunedList)
             {
                 if (enemy.ExpandedCollisionBox.Intersects(entity.CollisionBox) && entity.ObjectID != enemy.ObjectID && entity.ObjectID != (int)AvatarID.MARIO)
                 {
                     gonnaFall = false;
                     break;
                 }
             }
             return gonnaFall;
         } */

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

        public static bool IsCurrentlyFireMario()
        {
            Mario mario = (Mario)EntityManager.FindItem((int)AvatarID.MARIO);
            return (mario.marioActionState is FireMarioFallingState ||
                    mario.marioActionState is FireMarioJumpingState ||
                    mario.marioActionState is FireMarioRunningState ||
                    mario.marioActionState is FireMarioStandingState);
        }

        #endregion

        #region AnimationAndCameraManager

        public static void AddCamera(Camera camera)
        {
            Camera = camera;
        }


        #endregion

        #region Collision Detection
        public static void SweepAndPrune()
        {
            mario = EntityManager.FindItem((int)AvatarID.MARIO);
            marioCurrentLocation = mario.Position;
            //Debug.WriteLine("MARIO POSITION " + mario.Position.X + "   "+ mario.Position.Y);
            foreach (IGameObjects entity in gameEntities)
            {
                if (marioCurrentLocation.X + 800 >= entity.Position.X && entity.Position.X - 800 < marioCurrentLocation.X)
                {
                    prunedList.Add(entity);
                }
            }

            for (int i = 0; i < prunedList.Count; i++)
            {
                for (int j = i + 1; j < prunedList.Count; j++)
                {
                    if (IntersectAABB(prunedList[i], prunedList[j]))
                    {
                        prunedList[i].HandleCollision(prunedList[j]);
                        prunedList[j].HandleCollision(prunedList[i]);
                    }
                }
            }
            // Debug.WriteLine("SIZE OF PRUNED LIST " + prunedList.Count);
            //Debug.WriteLine("SIZE OF OG LIST " + gameEntities.Count);
            copyPrunedList = prunedList.ToList();
            prunedList.Clear();
            //Debug.WriteLine("SIZE OF PRUNED COPY LIST " + copyPrunedList.Count);
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


        #endregion
    }
}



