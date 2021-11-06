using GameSpace.Camera2D;
using GameSpace.Enums;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.GameObjects.EnemyObjects;
using GameSpace.GameObjects.ItemObjects;
using GameSpace.Interfaces;
using GameSpace.States.MarioStates;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace GameSpace.EntitiesManager
{
    // DO NOT USE!
    public static class EntityManager
    {
        private static readonly List<IGameObjects> gameEntities = new List<IGameObjects>();
        private static readonly List<IGameObjects> prunedList = new List<IGameObjects>();
        private static List<IGameObjects> copyPrunedList = new List<IGameObjects>();
        private static IGameObjects mario;
        private static Vector2 marioCurrentLocation;
        public static Camera Camera { get; set; }



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


        #endregion


        #region Collision Detection

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



