using GameSpace.Abstracts;
using GameSpace.EntityManaging;
using GameSpace.Enums;
using GameSpace.GameObjects.EnemyObjects;
using GameSpace.Interfaces;
using GameSpace.Objects.EnemyObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Handlers
{
    public class CollisionEnemyHandler : Handler
    {
        private static readonly CollisionEnemyHandler instance = new CollisionEnemyHandler();

        public static CollisionEnemyHandler GetInstance()
        {
            return instance;
        }

        private CollisionEnemyHandler()
        {

        }

        public bool IsGoingToFall(Enemy enemy)
        {
            bool gonnaFall = true;
            foreach (IGameObjects entity in copyPrunedList)
            {
                if (enemy.ExpandedCollisionBox.Intersects(entity.CollisionBox) &&
                    !(entity is Item) &&
                    !(entity is Enemy) &&
                    entity.ObjectID != (int)AvatarID.MARIO)
                {
                    gonnaFall = false;
                    break;
                }
            }
            return gonnaFall;
        }

        public void HandleBlockCollision(Enemy enemy, IGameObjects block)
        {
            if (CollisionHandler.GetInstance().DetectCollisionDirection(enemy, block) == (int)CollisionDirection.LEFT)
            {
                enemy.Direction = (int)MarioDirection.LEFT;
            }

            else if (CollisionHandler.GetInstance().DetectCollisionDirection(enemy, block) == (int)CollisionDirection.RIGHT)
            {
                enemy.Direction = (int)MarioDirection.RIGHT;
            }
        }

        public void HandleBlockCollision(Koopa enemy, IGameObjects block)
        {
            if (CollisionHandler.GetInstance().DetectCollisionDirection(enemy, block) == (int)CollisionDirection.LEFT)
            {
                enemy.Direction = (int)MarioDirection.LEFT;
                enemy.state = new StateKoopaAliveLeft(enemy);
            }

            else if (CollisionHandler.GetInstance().DetectCollisionDirection(enemy, block) == (int)CollisionDirection.RIGHT)
            {
                enemy.Direction = (int)MarioDirection.RIGHT;
                enemy.state = new StateKoopaAliveRight(enemy);
            }
        }

        public void HandleBlockCollision(UberKoopa enemy, IGameObjects block)
        {
            if (CollisionHandler.GetInstance().DetectCollisionDirection(enemy, block) == (int)CollisionDirection.LEFT)
            {
                enemy.Direction = (int)MarioDirection.LEFT;
                enemy.state = new StateUberKoopaAliveLeft(enemy);
            }

            else if (CollisionHandler.GetInstance().DetectCollisionDirection(enemy, block) == (int)CollisionDirection.RIGHT)
            {
                enemy.Direction = (int)MarioDirection.RIGHT;
                enemy.state = new StateUberKoopaAliveRight(enemy);
            }
        }

        public void HandleBlockCollision(SpinyRefactored enemy, IGameObjects block)
        {
            if (CollisionHandler.GetInstance().DetectCollisionDirection(enemy, block) == (int)CollisionDirection.LEFT)
            {
                enemy.Direction = (int)MarioDirection.LEFT;
                enemy.state = new StateSpinyAliveLeft(enemy);
            }

            else if (CollisionHandler.GetInstance().DetectCollisionDirection(enemy, block) == (int)CollisionDirection.RIGHT)
            {
                enemy.Direction = (int)MarioDirection.RIGHT;
                enemy.state = new StateSpinyAliveRight(enemy);
            }
        }

        public void HandleMarioCollision(Enemy enemy)
        {
            if (CollisionHandler.GetInstance().DetectCollisionDirection(enemy, mario) == (int)CollisionDirection.UP)
            {
                mario.score += 100;
                enemy.Trigger();
            }
        }

        public void HandleMarioCollision(Lakitu enemy)
        {
            if (CollisionHandler.GetInstance().DetectCollisionDirection(enemy, mario) == (int)CollisionDirection.UP)
            {
                mario.score += 100;
                enemy.Trigger();
            }
        }

        public void HandleFireBallCollision(Enemy enemy)
        {
            enemy.Trigger();
            mario.score += 100;
        }

    }
}
