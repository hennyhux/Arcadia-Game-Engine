using GameSpace.Abstracts;
using GameSpace.Camera2D;
using GameSpace.EntitiesManager;
using GameSpace.Enums;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.GameObjects.EnemyObjects;
using GameSpace.GameObjects.ExtraItemsObjects;
using GameSpace.GameObjects.ItemObjects;
using GameSpace.Interfaces;
using GameSpace.Machines;
using GameSpace.Sprites.ExtraItems;
using GameSpace.States.BlockStates;
using GameSpace.States.MarioStates;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using System.Linq;
using static GameSpace.GameObjects.EnemyObjects.GreenKoopa;

namespace GameSpace.EntityManaging
{
    public class CollisionHandler : AbstractHandler
    {
        private static readonly CollisionHandler instance = new CollisionHandler();

        public static CollisionHandler GetInstance()
        {
            return instance;
        }

        private CollisionHandler()
        {

        }

        public void UpdateCollision()
        {
            SweepAndPrune();
        }

        #region Collision Algorithms
        protected internal void SweepAndPrune()
        {
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
                    if (CollisionHandler.GetInstance().IntersectAABB(prunedList[i], prunedList[j]))
                    {
                        prunedList[i].HandleCollision(prunedList[j]);
                        prunedList[j].HandleCollision(prunedList[i]);
                    }
                }
            }
            //Debug.WriteLine("SIZE OF PRUNED LIST " + prunedList.Count);
            //Debug.WriteLine("SIZE OF OG LIST " + gameEntityList.Count);
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

        #endregion

        #region Item Collision
        public void ItemToMarioCollison(BigPipe pipe)
        {
            switch (DetectCollisionDirection(mario, pipe))
            {
                case (int)CollisionDirection.UP:

                    break;
            }
        }
        public void ItemToMarioCollison(WarpPipeHead pipe)
        {
            switch (DetectCollisionDirection(mario, pipe))
            {
                case (int)CollisionDirection.DOWN:
                    pipe.TimesCollided++;
                    MarioHandler.GetInstance().BounceMario();
                    if (pipe.TimesCollided == 2)
                    {
                        MarioHandler.GetInstance().SetMarioStateToWarp();
                    }
                    break;
            }
        }
        public void ItemToMarioCollison(WarpPipeHeadWithMob pipe)
        {
           if (mario.Position.X >= pipe.expandedCollisionBox.X)
            {
                pipe.RevealItem();
            }
        }


        #endregion

        #region Enemy Collision
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

        public void EnemyToBlockCollision(AbstractEnemy enemy, IGameObjects block)
        {

            if (EntityManager.DetectCollisionDirection(enemy, block) == (int)CollisionDirection.LEFT)
            {
                enemy.Direction = (int)eFacing.LEFT;
                if (enemy is GreenKoopa)
                {
                    enemy.state = new StateGreenKoopaAliveFaceLeft();
                }
            }

            else if (EntityManager.DetectCollisionDirection(enemy, block) == (int)CollisionDirection.RIGHT)
            {
                enemy.Direction = (int)eFacing.RIGHT;
                if (enemy is GreenKoopa)
                {
                    enemy.state = new StateGreenKoopaAliveFaceRight();
                }
            }
        }

        public void EnemyToMarioCollision(AbstractEnemy enemy, IGameObjects mario)
        {
            if (EntityManager.DetectCollisionDirection(enemy, mario) == (int)CollisionDirection.UP)
            {
                enemy.Trigger();
                enemy.CollisionBox = new Rectangle(1, 1, 0, 0);
            }
        }
        #endregion

        #region Mario Collision
        public void ChangeMarioStatesUponCollision(IGameObjects entity)
        {
            switch (DetectCollisionDirection(mario, entity))
            {
                case (int)CollisionDirection.LEFT:
                case (int)CollisionDirection.RIGHT:
                    mario.StandingTransition();
                    break;

                case (int)CollisionDirection.UP:
                    mario.FallingTransition();
                    break;

                case (int)CollisionDirection.DOWN:
                    if (mario.marioActionState is SmallMarioFallingState ||
                        mario.marioActionState is BigMarioFallingState || 
                        mario.marioActionState is FireMarioFallingState)
                    {
                        mario.DownTransition();
                    }
                    break;
            }
        }

        public void MarioToBlockCollision(IGameObjects block)
        {
            switch (DetectCollisionDirection(mario, block))
            {
                case (int)CollisionDirection.LEFT:
                    mario.Position = new Vector2((int)block.Position.X - mario.CollisionBox.Width, (int)mario.Position.Y);
                    break;

                case (int)CollisionDirection.RIGHT:
                    mario.Position = new Vector2((int)block.Position.X + block.CollisionBox.Width, (int)mario.Position.Y);
                    break;

                case (int)CollisionDirection.UP:
                    mario.Velocity = new Vector2(mario.Velocity.X, 50);
                    break;

                case (int)CollisionDirection.DOWN:
                    mario.Position = new Vector2(mario.Position.X, (int)block.Position.Y - mario.CollisionBox.Height);
                    break;
            }

            if (block.ObjectID == (int)ItemID.BIGPIPE)
            {
                mario.WarpMario();
            }
        }

        public void MarioToHiddenBlockCollision(IGameObjects block)
        {
            HiddenBlock hBlock = (HiddenBlock)block;
            if (hBlock.hasCollided)
            {
                MarioToBlockCollision(block);
            }
        }

        public void MarioToEnemyCollision(IGameObjects enemy)
        {
            if (DetectCollisionDirection(mario, enemy) != (int)CollisionDirection.DOWN)
            {
                mario.Trigger(); 
            }
        }

        public void MarioToItemCollision(FireFlower item)
        {
            if (mario.marioPowerUpState is SmallMarioState)
            {
                mario.BigMarioTransformation();
            }
            else if (mario.marioPowerUpState is BigMarioState)
            {
                mario.FireMarioTransformation();
            }

            mario.score += 1000;
        }

        public void MarioToItemCollision(Star item)
        {
            //Don't have to implement star powerUp but should still receive points
            mario.score += 1000;
        }

        public void MarioToItemCollision(SuperShroom item)
        {
            if (!(mario.marioPowerUpState is FireMarioState))
            {
                mario.BigMarioTransformation();
            }
            mario.score += 1000;
        }

        public void MarioToItemCollision(OneUpShroom item)
        {
            ++mario.marioLives;
        }

        public void MarioToItemCollision(Coin coin)
        {
            ++mario.numCoinsCollected;
            mario.score += 200;
        }

        #endregion

        #region Block Collision
        public void BlockToMarioCollision(IGameObjects block)
        {
            switch (DetectCollisionDirection(mario, block))
            {
                case (int)CollisionDirection.UP:
                    block.Trigger();
                    break;
            }
        }
        #endregion

        #region Misc Collision
        public bool IsGoingToBeOutOfBounds(IGameObjects entity, Vector2 newLocation)
        {
            if (entity.Position.X + newLocation.X <= 0)
            {
                if (entity is Mario)mario.marioActionState.StandingTransition();
                return true;
            }

            if (entity.Position.Y + newLocation.Y <= 0) 
            {
                entity.Velocity = new Vector2(entity.Velocity.X, 50);
                if (entity is Mario) mario.marioActionState.FallingTransition();
                return true; 
            }

            if (entity.Position.X + (entity.CollisionBox.Width) + newLocation.X > ((Rectangle)cameraCopy.Limits).Width)
            {
                if (entity is Mario) mario.marioActionState.StandingTransition(); 
                return true; 
            }

            if (entity.Position.Y + newLocation.Y + entity.CollisionBox.Height >= ((Rectangle)cameraCopy.Limits).Height) 
            {
                if (entity is Mario) mario.marioPowerUpState.DeadTransition();
                return true; 
            }
            return false; 
        }
        #endregion

    }

}


