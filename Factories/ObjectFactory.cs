using GameSpace.Abstracts;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.GameObjects.EnemyObjects;
using GameSpace.GameObjects.ExtraItemsObjects;
using GameSpace.GameObjects.ItemObjects;
using GameSpace.Interfaces;
using GameSpace.Objects.BlockObjects;
using GameSpace.Objects.EnemyObjects;
using GameSpace.Sprites.ExtraItems;
using Microsoft.Xna.Framework;
using System.Diagnostics;
namespace GameSpace.Factories
{
    public class ObjectFactory
    {

        private protected static GameRoot instanceGame;

        private static ObjectFactory instance;

        public static ObjectFactory GetInstance()
        {
            if (instance == null)
            {
                instance = new ObjectFactory();
            }
            return instance;
        }

        private ObjectFactory()
        {

        }


        #region BrickBlock
        public IGameObjects CreateBrickBlockObject(Vector2 location)
        {
            return new BrickBlock(location);
        }

        public IGameObjects CreateHiddenLevelBrickBlockObject(Vector2 location)
        {
            return new HiddenLevelBrickBlock(location);
        }

        public IGameObjects CreateBrickBlockWithItem(Vector2 location, IGameObjects item)
        {
            return new BrickBlockWithItem(location, (AbstractItem)item);
        }

        #endregion
        public IGameObjects CreateStairBlockObject(Vector2 location)
        {
            return new StairBlock(location);
        }

        public IGameObjects CreateFloorBlockObject(Vector2 location)
        {
            return new FloorBlock(location);
        }

        public IGameObjects CreateHiddenLevelFloorBlockObject(Vector2 location)
        {
            return new HiddenLevelFloorBlock(location);
        }

        #region Question Block

        public IGameObjects CreateQuestionBlockObject(Vector2 location)
        {
            return new QuestionBlock(location, null);
        }

        public IGameObjects CreateQuestionBlockCoin(Vector2 location)
        {
            return new QuestionBlock(location, (AbstractItem)CreateCoinObject(location));
        }

        public IGameObjects CreateQuestionBlockOneUpShroom(Vector2 location)
        {
            return new QuestionBlock(location, (AbstractItem)CreateOneUpShroomObject(location));
        }

        public IGameObjects CreateQuestionBlockShroom(Vector2 location)
        {
            return new QuestionBlock(location, (AbstractItem)CreateSuperShroomObject(location));
        }

        public IGameObjects CreateQuestionBlockFire(Vector2 location)
        {
            return new QuestionBlock(location, (AbstractItem)CreateFireFlowerObject(location));
        }

        public IGameObjects CreateQuestionBlockStar(Vector2 location)
        {
            return new QuestionBlock(location, (AbstractItem)CreateStarObject(location));
        }

        #endregion

        public IGameObjects CreateHiddenBlockObject(Vector2 location)
        {
            return new HiddenBlock(location);
        }

        public IGameObjects CreateUsedBlockObject(Vector2 location)
        {
            return new UsedBlock(location);
        }


        #region Enemies 
        public IGameObjects CreateGoombaObject(Vector2 location)
        {
            return new Goomba(location);
        }

        public IGameObjects CreateUberGoombaObject(Vector2 location)
        {
            return new UberGoomba(location);
        }
        public IGameObjects CreateGreenKoopaObject(Vector2 location)
        {
            return new GreenKoopa(location);
        }

        public IGameObjects CreateRedKoopaObject(Vector2 location)
        {
            return new RedKoopa(location);
        }

        public IGameObjects CreateUberKoopaObject(Vector2 location)
        {
            return new UberKoopa(location);
        }
        public IGameObjects CreatePlantObject(Vector2 location)
        {
            return new Plant(location);
        }

        public IGameObjects CreateVineObject(Vector2 location)
        {
            return new Vine(location);
        }

        public IGameObjects CreateVineHiddenBlockObject(Vector2 location)
        {
            return new HiddenBlockWithVine(location);
        }

        public IGameObjects CreateSpinyObject(Vector2 location)
        {
            return new SpinyRefactored(location);
        }

        #endregion

        #region Items
        public IGameObjects CreateCoinObject(Vector2 location)
        {
            return new Coin(location);
        }
        public IGameObjects CreateHiddenLevelCoinObject(Vector2 location)
        {
            return new HiddenLevelCoin(location);
        }

        public IGameObjects CreateHUDCoinObject(Vector2 location)
        {
            Debug.Print("HUDCOIN");
            return new HUDCoin(location);
        }
        public IGameObjects CreateStarObject(Vector2 location)
        {
            return new Star(location);
        }

        public IGameObjects CreateFireFlowerObject(Vector2 location)
        {
            return new FireFlower(location);
        }

        public IGameObjects CreateOneUpShroomObject(Vector2 location)
        {
            return new OneUpShroom(location);
        }

        public IGameObjects CreateSuperShroomObject(Vector2 location)
        {
            return new SuperShroom(location);
        }

        public IGameObjects CreateFireBallObject(Mario mario)
        {
            return new Fireball();
        }
        #endregion

        #region Items
        public IGameObjects CreateSmallPipeObject(Vector2 location)
        {
            return new SmallPipe(location);
        }

        public IGameObjects CreateMediumPipeObject(Vector2 location)
        {
            return new MediumPipe(location);
        }

        public IGameObjects CreateBigPipeObject(Vector2 location)
        {
            return new BigPipe(location);
        }

        public IGameObjects CreateFlagPoleObject(Vector2 location)
        {
            return new FlagPole(location);
        }

        public IGameObjects CreateCastleObject(Vector2 location)
        {
            return new Castle(location);
        }

        public IGameObjects CreateWarpPipeHead(Vector2 location)
        {
            return new WarpPipeHead(location);
        }

        public IGameObjects CreateWarpPipeHeadWithMob(Vector2 location)
        {
            return new WarpPipeHeadMob(location);
        }

        public IGameObjects CreateHiddenLevelHorizontalPipe(Vector2 location)
        {
            return new HiddenLevelHorizontalPipe(location);
        }

        public IGameObjects CreateHiddenLevelVerticalPipe(Vector2 location)
        {
            return new HiddenLevelVerticalPipe(location);
        }

        public IGameObjects CreateBlackWindow(Vector2 location)
        {
            return new BlackWindow(location);
        }

        public IGameObjects CreateWarpPipeHeadRoom(Vector2 location)
        {
            return new WarpPipeHeadRoom(location);
        }
        public IGameObjects CreateWarpVineBlock(Vector2 location)
        {
            return new WarpVineBlock(location);
        }


        public IGameObjects CreateWarpPipeBody(Vector2 location)
        {
            return new WarpPipeBody(location);
        }

        public IGameObjects CreateWarpPipeHeadBack(Vector2 location)
        {
            return new WarpPipeHeadBack(location);
        }

        #endregion
    }
}
