using GameSpace.Abstracts;
using GameSpace.Enums;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.GameObjects.EnemyObjects;
using GameSpace.GameObjects.ExtraItemsObjects;
using GameSpace.GameObjects.ItemObjects;
using GameSpace.Interfaces;
using GameSpace.Machines;
using GameSpace.Objects.EnemyObjects;
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
            HandleAllCollisions();
        }

        #region Collision Algorithms
        private protected void HandleAllCollisions()
        {

            for (int i = 0; i < copyPrunedList.Count; i++)
            {
                for (int j = i + 1; j < copyPrunedList.Count; j++)
                {
                    if (IntersectAABB(copyPrunedList[i], copyPrunedList[j]))
                    {
                        copyPrunedList[i].HandleCollision(copyPrunedList[j]);
                        copyPrunedList[j].HandleCollision(copyPrunedList[i]);
                    }
                }
            }
        }

        //Still O(n^2) will change for sprint 5
        private protected void SweepAndPrune()
        {
            marioCurrentLocation = mario.Position;
            foreach (IGameObjects entity in gameEntityList)
            {
                if (entity.Position.X + 800 >= marioCurrentLocation.X && entity.Position.X - 800 < marioCurrentLocation.X)
                {
                    prunedList.Add(entity);
                }
            }
            //Debug.WriteLine("SIZE OF PRUNED LIST " + prunedList.Count);
            //Debug.WriteLine("SIZE OF OG LIST " + gameEntityList.Count);
            copyPrunedList = prunedList.ToList();
            prunedList.Clear();
            //Debug.WriteLine("SIZE OF PRUNED COPY LIST " + copyPrunedList.Count);
        }

        public int DetectCollisionDirection(IGameObjects a, IGameObjects b)
        {
            Rectangle overLappedRectangle = Rectangle.Intersect(a.CollisionBox, b.CollisionBox);
            int direction = 0;

            /*if (!overLappedRectangle.IsEmpty)
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
            }*/

            

            float player_bottom = a.Position.Y + a.CollisionBox.Height;
            float tiles_bottom = b.Position.Y + b.CollisionBox.Height;
            float player_right = a.Position.X + a.CollisionBox.Width;
            float tiles_right = b.Position.X + b.CollisionBox.Width;

            float b_collision = tiles_bottom - a.Position.Y;
            float t_collision = player_bottom - b.Position.Y;
            float l_collision = player_right - b.Position.X;
            float r_collision = tiles_right - a.Position.X;

            if (t_collision < b_collision && t_collision < l_collision && t_collision < r_collision)
            {
                //Top collision
                direction = (int)CollisionDirection.DOWN;
            }
            if (b_collision < t_collision && b_collision < l_collision && b_collision < r_collision)
            {
                //bottom collision
                direction = (int)CollisionDirection.UP;
            }
            if (l_collision < r_collision && l_collision < t_collision && l_collision < b_collision)
            {
                //Left collision
                direction = (int)CollisionDirection.LEFT;
            }
            if (r_collision < l_collision && r_collision < t_collision && r_collision < b_collision)
            {
                //Right collision
                direction = (int)CollisionDirection.RIGHT;
            }

            return direction;
        }



        private bool IntersectAABB(IGameObjects a, IGameObjects b)
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
        public bool IsGoingToFall(AbstractItem item)
        {
            bool gonnaFall = true;
            foreach (IGameObjects entity in copyPrunedList)
            {
                if (item.ExpandedCollisionBox.Intersects(entity.CollisionBox) &&
                    !(entity is AbstractItem) &&
                    !(entity is AbstractEnemy) &&
                    entity.ObjectID != (int)AvatarID.MARIO)
                {
                    gonnaFall = false;
                    break;
                }
            }
            return gonnaFall;
        }
        public void ItemToBlockCollision(AbstractBlock block, AbstractItem item)
        {
            switch (DetectCollisionDirection(item, block))
            {
                case (int)CollisionDirection.LEFT:
                case (int)CollisionDirection.RIGHT:
                    item.HasHalted = true;
                    break;
            }
        }

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


        public void ItemToMarioCollison(WarpPipeHeadMob pipe)
        {

            switch (DetectCollisionDirection(mario, pipe))
            {
                case (int)CollisionDirection.DOWN:
                case (int)CollisionDirection.LEFT:
                case (int)CollisionDirection.RIGHT:
                    pipe.HideItem();
                    break;
            }

        }
        public void ItemToMarioCollison(WarpPipeHeadRoom pipe)
        {
            switch (DetectCollisionDirection(mario, pipe))
            {
                case (int)CollisionDirection.DOWN:
                    pipe.TimesCollided++;
                    MarioHandler.GetInstance().BounceMario();
                    if (pipe.TimesCollided == 2)
                    {
                        MarioHandler.GetInstance().WarpMarioToHiddenRoom();
                    }
                    break;

            }
        }

        public void ItemToMarioCollison(WarpPipeHeadBack pipe)
        {
            switch (DetectCollisionDirection(mario, pipe))
            {
                case (int)CollisionDirection.DOWN:
                    pipe.TimesCollided++;
                    MarioHandler.GetInstance().BounceMario();
                    if (pipe.TimesCollided == 3)
                    {
                        MarioHandler.GetInstance().WarpMarioBackToStart();
                    }
                    break;
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

            if (DetectCollisionDirection(enemy, block) == (int)CollisionDirection.LEFT)
            {
                enemy.Direction = (int)MarioDirection.LEFT;
                if (enemy is GreenKoopa)
                {
                    GreenKoopa copy = (GreenKoopa)enemy;
                    copy.FlipSprite();
                }
            }

            else if (DetectCollisionDirection(enemy, block) == (int)CollisionDirection.RIGHT)
            {
                enemy.Direction = (int)MarioDirection.RIGHT;
                if (enemy is GreenKoopa)
                {
                    GreenKoopa copy = (GreenKoopa)enemy;
                    copy.FlipSprite();
                }
            }
        }


        public void ShellToBlockCollision(GreenKoopa enemy, IGameObjects block)
        {

            if (DetectCollisionDirection(enemy, block) == (int)CollisionDirection.LEFT)
            {
                enemy.Direction = (int)MarioDirection.LEFT;

            }

            else if (DetectCollisionDirection(enemy, block) == (int)CollisionDirection.RIGHT)
            {
                enemy.Direction = (int)MarioDirection.RIGHT;
            }
        }

        public void EnemyToMarioCollision(AbstractEnemy enemy)
        {

            if (DetectCollisionDirection(enemy, mario) == (int)CollisionDirection.UP)
            {
                mario.score += 100;
                enemy.Trigger();
            }
        }

        public void EnemyToMarioCollision(SpinyRefactored enemy)
        {

        }

        public void EnemyToMarioCollision(Spiny enemy)
        {

        }

        public void EnemyToMarioCollision(GreenKoopa enemy)
        {
            if (DetectCollisionDirection(enemy, mario) == (int)CollisionDirection.UP)
            {
                enemy.Trigger();
            }

            else
            {
                if (enemy.State is GreenKoopaShellState ||
                    enemy.State is GreenKoopaShellAndLegsState)
                {
                    enemy.Trigger();
                }
            }
        }


        public void EnemyToEnemyCollision(GreenKoopa enemy, Goomba enemyB)
        {
            if (enemy.state is StateGreenKoopaDeadMoving)
            {
                enemyB.Trigger();
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
                    if (mario.MarioActionState is SmallMarioFallingState ||
                        mario.MarioActionState is BigMarioFallingState ||
                        mario.MarioActionState is FireMarioFallingState)
                    {
                        mario.StandingTransition();
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
                    Debug.Print("marioY(math): {0}, block.Y: {1}", (int)block.Position.Y - mario.CollisionBox.Height, block.Position.Y);
                    break;
            }
        }

        public void MarioToHiddenBlockCollision(IGameObjects block)
        {
            HiddenBlock hBlock = (HiddenBlock)block;

                MarioToBlockCollision(block);
            
        }



        public void MarioToEnemyCollision(IGameObjects enemy)
        {
            if (DetectCollisionDirection(mario, enemy) != (int)CollisionDirection.DOWN)
            {
                mario.Trigger();
            }

            else
            {
                mario.LeapTransition();
            }
        }

        public void MarioToEnemyCollision(GreenKoopa koopa)
        {
            if (DetectCollisionDirection(mario, koopa) != (int)CollisionDirection.DOWN &&
                DetectCollisionDirection(mario, koopa) != 0)
            {
                if (koopa.State is GreenKoopaAliveState)mario.Trigger();
            }

            else
            {
                mario.LeapTransition();
            }
            
        }

        public void MarioToItemCollision(FireFlower item)
        {
            if (mario.MarioPowerUpState is SmallMarioState)
            {
                mario.BigMarioTransformation();
            }
            else if (mario.MarioPowerUpState is BigMarioState)
            {
                mario.FireMarioTransformation();
            }

            mario.score += 1000;
        }

        public void MarioToItemCollision(Vine item)
        {
            mario.StartClimbing();

        }

        

        public void MarioToItemCollision(Star item)
        {
            mario.score += 1000;
        }

        public void MarioToItemCollision(SuperShroom item)
        {
            if (!(mario.MarioPowerUpState is FireMarioState))
            {
                mario.BigMarioTransformation();
            }
            mario.score += 1000;
        }

        public void MarioToItemCollision(OneUpShroom item)
        {
            mario.MarioPowerUpState.BigMarioTransformation();
            MarioHandler.GetInstance().IncrementMarioLives();
        }

        public void MarioToItemCollision(Coin coin)
        {
            ++mario.numCoinsCollected;
            if (mario.numCoinsCollected == 100)
            {
                ++MarioHandler.marioLives;
            }
            mario.score += 200;
        }

        public void MarioToItemCollision(HiddenLevelCoin coin)
        {
            mario.numCoinsCollected++;
            if (mario.numCoinsCollected == 100)
            {
                ++MarioHandler.marioLives;
            }
            mario.score += 200;
        }
        public void MarioToItemCollision(Castle castle)
        {
            MarioHandler.GetInstance().EnterVictoryPanel();
        }

        public void MarioToItemCollision(FlagPole pole)
        {
            mario.Velocity = new Vector2(0, (500 - (mario.Position.Y + mario.CollisionBox.Height)) / (float)2.5);
            int marioY = (int)mario.Position.Y;
            int poleY = (int)pole.Position.Y;

            Debug.Print("poleY : {0}, deltaH {1}", poleY, poleY + pole.CollisionBox.Height);
            MarioHandler.GetInstance().CalculateFinalScore(marioY, poleY);
        }

        public bool IsGoingToFall()
        {
            bool gonnaFall = true;
            foreach (IGameObjects entity in copyPrunedList)
            {
                if (mario.ExpandedCollisionBox.Intersects(entity.CollisionBox) &&
                    !(entity is AbstractItem) &&
                    !(entity is AbstractEnemy) &&
                    entity.ObjectID != (int)AvatarID.MARIO)
                {
                    gonnaFall = false;
                    break;
                }
            }
            return gonnaFall;
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
                if (entity is Mario)
                {
                    mario.MarioActionState.StandingTransition();
                }

                return true;
            }

            if (entity.Position.Y + newLocation.Y <= 0)
            {
                entity.Velocity = new Vector2(entity.Velocity.X, 50);
                if (entity is Mario)
                {
                    mario.MarioActionState.FallingTransition();
                }

                return true;
            }

            if (entity.Position.X + (entity.CollisionBox.Width) + newLocation.X > ((Rectangle)cameraCopy.Limits).Width)
            {
                if (entity is Mario)
                {
                    mario.MarioActionState.StandingTransition();
                }

                return true;
            }

            if (entity.Position.Y + newLocation.Y + entity.CollisionBox.Height >= ((Rectangle)cameraCopy.Limits).Height)
            {
                if (entity is Mario)
                {
                    mario.MarioPowerUpState.DeadTransition();
                }

                return true;
            }
            return false;
        }
        #endregion
    }

    public class MarioCollisionHandler : AbstractHandler
    {
        private static readonly MarioCollisionHandler instance = new MarioCollisionHandler();

        public static MarioCollisionHandler GetInstance()
        {
            return instance;
        }

        private MarioCollisionHandler()
        {

        }
    }

    public class EnemyCollisionHandler : AbstractHandler
    {
        private static EnemyCollisionHandler instance = new EnemyCollisionHandler();

        public static EnemyCollisionHandler GetInstance()
        {
            return instance;
        }

        private EnemyCollisionHandler()
        {

        }

        public bool IsGoingToFall(Enemy enemy)
        {
            bool gonnaFall = true;
            foreach (IGameObjects entity in copyPrunedList)
            {
                if (enemy.ExpandedCollisionBox.Intersects(entity.CollisionBox) &&
                    !(entity is AbstractItem) &&
                    !(entity is AbstractEnemy) &&
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

    }
}


