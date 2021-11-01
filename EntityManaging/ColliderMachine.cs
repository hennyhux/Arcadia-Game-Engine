using GameSpace.Abstracts;
using GameSpace.EntitiesManager;
using GameSpace.Enums;
using GameSpace.GameObjects.EnemyObjects;
using GameSpace.GameObjects.ExtraItemsObjects;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using System.Linq;

namespace GameSpace.EntityManaging
{
    public class ColliderMachine : AbstractMachine
    {
        private static readonly ColliderMachine instance = new ColliderMachine();

        public static ColliderMachine GetInstance()
        {
            return instance;
        }

        private ColliderMachine()
        {

        }

        public void HandleMarioCollision(BigPipe pipe)
        {
            switch (DetectCollisionDirection(mario, pipe))
            {
                case (int)CollisionDirection.UP:

                    break;
            }
        }

        public void SweepAndPrune()
        {
            mario = FinderMachine.GetInstance().FindItem((int)AvatarID.MARIO);
            marioCurrentLocation = mario.Position;
            //Debug.WriteLine("MARIO POSITION " + mario.Position.X + "   "+ mario.Position.Y);
            foreach (IGameObjects entity in gameEntityList)
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
                    if (ColliderMachine.GetInstance().IntersectAABB(prunedList[i], prunedList[j]))
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

        private int DetectCollisionDirection(IGameObjects a, IGameObjects b)
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
        public bool IntersectAABB(IGameObjects a, IGameObjects b)
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


        #region HandleCollision
        public bool IsGoingToFall(AbstractEnemy enemy)
        {
            bool gonnaFall = true;
            foreach (IGameObjects entity in copyPrunedList)
            {
                if (enemy.ExpandedCollisionBox.Intersects(entity.CollisionBox) &&
                    entity.ObjectID != enemy.ObjectID &&
                    entity.ObjectID != (int)AvatarID.MARIO)
                {
                    gonnaFall = false;
                    break;
                }
            }
            return gonnaFall;
        }

        public void HandleBlockCollision(AbstractEnemy enemy, IGameObjects block)
        {

            if (EntityManager.DetectCollisionDirection(enemy, block) == (int)CollisionDirection.LEFT)
            {
                enemy.direction = (int)eFacing.LEFT;
                if (enemy is GreenKoopa)
                {
                    enemy.state = new StateGreenKoopaAliveFaceLeft();
                }
            }

            else if (EntityManager.DetectCollisionDirection(enemy, block) == (int)CollisionDirection.RIGHT)
            {
                enemy.direction = (int)eFacing.RIGHT;
                if (enemy is GreenKoopa)
                {
                    enemy.state = new StateGreenKoopaAliveFaceRight();
                }
            }
        }

        public void HandleMarioCollision(AbstractEnemy enemy, IGameObjects mario)
        {
            if (EntityManager.DetectCollisionDirection(enemy, mario) == (int)CollisionDirection.UP)
            {
                enemy.Trigger();
                enemy.CollisionBox = new Rectangle(1, 1, 0, 0);
            }
        }
    }
    #endregion
}


