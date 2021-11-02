using GameSpace.GameObjects.BlockObjects;
using GameSpace.GameObjects.EnemyObjects;
using GameSpace.GameObjects.ExtraItemsObjects;
using GameSpace.GameObjects.ItemObjects;
using GameSpace.Interfaces;
using GameSpace.Objects.BlockObjects;
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

        #region Blocks
        public IGameObjects CreateBrickBlockObject(Vector2 location)
        {
            return new GameObjects.BlockObjects.BrickBlocks(location);
        }
        public IGameObjects CreateCoinBrickBlock(Vector2 location)
        {
            return new BrickBlockWithItem(location, ObjectFactory.GetInstance().CreateCoinObject(location));
        }

        public IGameObjects CreateFireBrickBlock(Vector2 location)
        {
            return new BrickBlockWithItem(location, ObjectFactory.GetInstance().CreateFireFlowerObject(location));
        }
        public IGameObjects CreateStairBlockObject(Vector2 location)
        {
            return new StairBlock(location);
        }

        public IGameObjects CreateFloorBlockObject(Vector2 location)
        {
            return new FloorBlock(location);
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

        #endregion

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

        #endregion

        #region Items
        public IGameObjects CreateCoinObject(Vector2 location)
        {
            return new Coin(location);
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
            return new Fireball(mario);
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
        #endregion
    }
}
