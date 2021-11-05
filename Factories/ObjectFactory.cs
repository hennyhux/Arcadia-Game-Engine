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

        public IGameObjects CreateQuestionBlockObject(Vector2 location)
        {
            return new QuestionBlock(location);
        }

        public IGameObjects CreateHiddenBlockObject(Vector2 location)
        {
            return new HiddenBlock(location);
        }

        public IGameObjects CreateUsedBlockObject(Vector2 location)
        {
            return new UsedBlock(location);
        }

        public IGameObjects CreateStarBrickBlock(Vector2 location)
        {
            return new BrickBlockStar(location);
        }

        public IGameObjects CreateSuperShroomBrickBlock(Vector2 location)
        {
            return new BrickBlockSuperShroom(location);
        }

        public IGameObjects CreateOneUpShroomBrickBlock(Vector2 location)
        {
            return new BrickBlockOneShroom(location);
        }

        public IGameObjects CreateQuestionBlockStar(Vector2 location)
        {
            return new QuestionBlockStar(location);
        }

        public IGameObjects CreateQuestionBlockCoin(Vector2 location)
        {
            return new QuestionBlock(location);
        }

        public IGameObjects CreateQuestionBlockOneUpShroom(Vector2 location)
        {
            return new QuestionBlockOneUpShroom(location);
        }

        public IGameObjects CreateQuestionBlockShroom(Vector2 location)
        {
            return new QuestionBlockOneShroom(location);
        }

        public IGameObjects CreateQuestionBlockFire(Vector2 location)
        {
            return new QuestionBlockFire(location);
        }


        #region Enemies 
        public IGameObjects CreateGoombaObject(Vector2 location)
        {
            return new Goomba(location);
        }

        public IGameObjects CreateGreenKoopaObject(Vector2 location)
        {
            return new GreenKoopa(location);
        }

        public IGameObjects CreateRedKoopaObject(Vector2 location)
        {
            return new RedKoopa(location);
        }

        public IGameObjects CreatePlantObject(Vector2 location)
        {
            return new Plant(location);
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
            return new WarpPipeHeadWithMob(location);
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

        #endregion
    }
}
