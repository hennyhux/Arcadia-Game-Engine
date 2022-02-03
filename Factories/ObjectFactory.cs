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
            return new Koopa(location);
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

        public IGameObjects CreateVineWarpObject(Vector2 location)
        {
            return new Vine(location, 0);
        }


        public IGameObjects CreateVineHiddenBlockObject(Vector2 location)
        {
            return new HiddenBlockWithVine(location);
        }

        public IGameObjects CreateSpinyObject(Vector2 location)
        {
            return new SpinyRefactored(location);
        }

        public IGameObjects CreateLakituObject(Vector2 location)
        {
            return new LakituRefactored(location);
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
