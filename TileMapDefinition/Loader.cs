using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace GameSpace.TileMapDefinition
{
    public static class Loader
    {
        public static int boundaryX = 0;
        private static readonly ObjectFactory objectFactory = ObjectFactory.GetInstance();

        public static List<IGameObjects> Load(GameRoot game, string xmlFile, Vector2 checkPoint, bool levelRestart)
        {
            List<IGameObjects> objects = new List<IGameObjects>();
            List<LevelDefinition.Objects> fullList = new List<LevelDefinition.Objects>();
            XmlSerializer serializer = new XmlSerializer(typeof(List<LevelDefinition.Objects>), new XmlRootAttribute("Level"));
            using (XmlReader reader = XmlReader.Create(xmlFile))
            {
                fullList = (List<LevelDefinition.Objects>)serializer.Deserialize(reader);
            }
            foreach (LevelDefinition.Objects obstacles in fullList)
            {
                Vector2 location = new Vector2(obstacles.x, obstacles.y);
                if (boundaryX < obstacles.x)
                {
                    boundaryX = obstacles.x;
                }
                LoadBackground(objects, obstacles, location);
                LoadBlocks(objects, obstacles, location);
                LoadItems(objects, obstacles, location);
                LoadEnemies(objects, obstacles, location);
                if (levelRestart)
                {
                    LoadAvatar(game, objects, obstacles, location);
                }
                else
                {
                    LoadAvatar(game, objects, obstacles, checkPoint);
                }
            }
            return objects;
        }

        public static void LoadBackground(List<IGameObjects> objects, LevelDefinition.Objects obstacles, Vector2 location)
        {
            /*Camera camera = new Camera(GraphicsDevice.Viewport) { Limits = new Rectangle(0, 0, Loader.boundaryX, 480) };//Should be set to level's max X and Y
            EntityManager.AddCamera(camera);
            Layer layer = new Layer(camera, BackgroundFactory.GetInstance().CreateRegularBackground(), new Vector2(1.0f, 1.0f));*/
        }
        public static void LoadBlocks(List<IGameObjects> objects, LevelDefinition.Objects obstacles, Vector2 location)
        {
            switch (obstacles.block)
            {
                case BlockID.FLOORBLOCK:
                    for (int i = 0; i < obstacles.blockRow; i++)
                    {
                        objects.Add(objectFactory.CreateFloorBlockObject(new Vector2(obstacles.x + (32 * i), obstacles.y)));
                    }
                    break;
                case BlockID.HIDDENLEVELFLOORBLOCK:
                    for (int i = 0; i < obstacles.blockRow; i++)
                    {
                        objects.Add(objectFactory.CreateHiddenLevelFloorBlockObject(new Vector2(obstacles.x + (32 * i), obstacles.y)));
                    }
                    break;
                case BlockID.VINEBLOCK:
                    for (int i = 0; i < obstacles.blockRow; i++)
                    {
                        objects.Add(objectFactory.CreateVineBlockObject(new Vector2(obstacles.x + (32 * i), obstacles.y)));
                    }
                    break;
                case BlockID.HIDDENBLOCK:
                    objects.Add(objectFactory.CreateHiddenBlockObject(location));
                    break;

                case BlockID.USEDBLOCK: //NOT USED IN LEVEL 1-1
                    objects.Add(objectFactory.CreateUsedBlockObject(location));
                    break;

                case BlockID.STAIRBLOCK:
                    for (int i = 0; i < obstacles.blockRow; i++)
                    {
                        objects.Add(objectFactory.CreateStairBlockObject(new Vector2(obstacles.x + (32 * i), obstacles.y)));
                    }
                    break;

                #region Brick Blocks
                case BlockID.BRICKBLOCK:
                    for (int i = 0; i < obstacles.blockRow; i++)
                    {
                        objects.Add(objectFactory.CreateBrickBlockObject(new Vector2(obstacles.x + (32 * i), obstacles.y)));
                    }
                    break;
                case BlockID.HIDDENLEVELBRICKBLOCK:
                    for (int i = 0; i < obstacles.blockRow; i++)
                    {
                        objects.Add(objectFactory.CreateHiddenLevelBrickBlockObject(new Vector2(obstacles.x + (32 * i), obstacles.y)));
                    }
                    break;
                case BlockID.COINBRICKBLOCK:
                    objects.Add(objectFactory.CreateBrickBlockWithItem(location, objectFactory.CreateCoinObject(location)));
                    break;

                case BlockID.FIREBRICKBLOCK:
                    objects.Add(objectFactory.CreateBrickBlockWithItem(location, objectFactory.CreateFireFlowerObject(location)));
                    break;

                case BlockID.ONEUPSHROOMBRICKBLOCK:
                    objects.Add(objectFactory.CreateBrickBlockWithItem(location, objectFactory.CreateOneUpShroomObject(location)));
                    break;

                case BlockID.SUPERSHROOMBRICKBLOCK:
                    objects.Add(objectFactory.CreateBrickBlockWithItem(location, objectFactory.CreateSuperShroomObject(location)));
                    break;

                case BlockID.STARBRICKBLOCK:
                    objects.Add(objectFactory.CreateBrickBlockWithItem(location, objectFactory.CreateStarObject(location)));
                    break;
                #endregion

                case BlockID.VINEHIDDENBLOCK:
                    objects.Add(objectFactory.CreateVineHiddenBlockObject(location));
                    break;

                #region Question Blocks
                case BlockID.QUESTIONBLOCK: //NOT USED IN LEVEL 1-1
                    objects.Add(objectFactory.CreateQuestionBlockObject(location));
                    break;

                case BlockID.COINQUESTIONBLOCK:
                    objects.Add(objectFactory.CreateQuestionBlockCoin(location));
                    break;

                case BlockID.STARQUESTIONBLOCK:
                    objects.Add(objectFactory.CreateQuestionBlockStar(location));
                    break;

                case BlockID.ONEUPSHROOMQUESTIONBLOCK:
                    objects.Add(objectFactory.CreateQuestionBlockOneUpShroom(location));
                    break;

                case BlockID.SUPERSHROOMQUESTIONBLOCK:
                    objects.Add(objectFactory.CreateQuestionBlockShroom(location));
                    break;
                case BlockID.FIREFLOWERQUESTIONBLOCK:
                    objects.Add(objectFactory.CreateQuestionBlockFire(location));
                    break;
                    #endregion
            }
        }
        public static void LoadItems(List<IGameObjects> objects, LevelDefinition.Objects obstacles, Vector2 location)
        {
            switch (obstacles.item)
            {
                #region Cases only used for test cases.
                case ItemID.COIN: //NOT USED IN LEVEL 1-1
                    objects.Add(objectFactory.CreateCoinObject(location));
                    break;
                case ItemID.STAR: //NOT USED IN LEVEL 1-1
                    objects.Add(objectFactory.CreateStarObject(location));
                    break;
                case ItemID.ONEUPSHROOM: //NOT USED IN LEVEL 1-1
                    objects.Add(objectFactory.CreateOneUpShroomObject(location));
                    break;
                case ItemID.SUPERSHROOM: //NOT USED IN LEVEL 1-1
                    objects.Add(objectFactory.CreateSuperShroomObject(location));
                    break;
                case ItemID.FIREFLOWER: //NOT USED IN LEVEL 1-1
                    objects.Add(objectFactory.CreateFireFlowerObject(location));
                    break;
                case ItemID.FIREBALL: //NOT USED IN LEVEL 1-1
                    //Do nothing
                    break;
                #endregion
                case ItemID.HIDDENLEVELCOIN: //Used on Hidden Level
                    for (int i = 0; i < obstacles.itemRow; i++)
                    {
                        objects.Add(objectFactory.CreateHiddenLevelCoinObject(new Vector2(obstacles.x + (32 * i), obstacles.y)));
                    }
                    break;
                case ItemID.HUDCOIN: //Used on for Animated Coin in HUD
                    objects.Add(objectFactory.CreateHUDCoinObject(location));
                    break;
                case ItemID.SMALLPIPE:
                    objects.Add(objectFactory.CreateSmallPipeObject(location));
                    break;
                case ItemID.MEDIUMPIPE:
                    objects.Add(objectFactory.CreateMediumPipeObject(location));
                    break;
                case ItemID.BIGPIPE:
                    objects.Add(objectFactory.CreateBigPipeObject(location));
                    break;
                case ItemID.WARPPIPEHEAD:
                    objects.Add(objectFactory.CreateWarpPipeHead(location));
                    break;
                case ItemID.WARPPIPEHEADWITHMOB:
                    objects.Add(objectFactory.CreateWarpPipeHeadWithMob(location)); 
                    break;
                case ItemID.WARPVINEWITHBLOCK:
                    objects.Add(objectFactory.CreateVineWarpObject(location));
                    //objects.Add(objectFactory.CreateVineObject(location));
                    break;
                case ItemID.HIDDENLEVELHORIZONTALPIPE:
                    objects.Add(objectFactory.CreateHiddenLevelHorizontalPipe(location));
                    break;
                case ItemID.HIDDENLEVELVERTICALPIPE:
                    objects.Add(objectFactory.CreateHiddenLevelVerticalPipe(location));
                    break;
                case ItemID.WARPPIPEROOM:
                    objects.Add(objectFactory.CreateWarpPipeHeadRoom(location));
                    break;
                case ItemID.WARPPIPEBODY:
                    objects.Add(objectFactory.CreateWarpPipeBody(location));
                    break;
                case ItemID.WARPPIPEBACK:
                    objects.Add(objectFactory.CreateWarpPipeHeadBack(location));
                    break;
                case ItemID.FLAGPOLE:
                    objects.Add(objectFactory.CreateFlagPoleObject(location));
                    break;
                case ItemID.CASTLE:
                    objects.Add(objectFactory.CreateCastleObject(location));
                    break;
                case ItemID.BLACKWINDOW:
                    objects.Add(objectFactory.CreateBlackWindow(location));
                    break;
                case ItemID.VINE:
                    objects.Add(objectFactory.CreateVineObject(location));
                    break;
            }
        }

        public static void LoadEnemies(List<IGameObjects> objects, LevelDefinition.Objects obstacles, Vector2 location)
        {
            switch (obstacles.enemy)
            {
                case EnemyID.GOOMBA:
                    objects.Add(objectFactory.CreateGoombaObject(location));
                    break;
                case EnemyID.GREENKOOPA:
                    objects.Add(objectFactory.CreateGreenKoopaObject(location));
                    break;
                case EnemyID.REDKOOPA:
                    objects.Add(objectFactory.CreateRedKoopaObject(location));
                    break;
                case EnemyID.PLANT:
                    objects.Add(objectFactory.CreatePlantObject(location));
                    break;
                case EnemyID.LAKITU:
                    objects.Add(objectFactory.CreateLakituObject(location));
                    break;
                case EnemyID.SPINY:
                    objects.Add(objectFactory.CreateSpinyObject(location));
                    break;
                case EnemyID.UBERGOOMBA:
                    objects.Add(objectFactory.CreateUberGoombaObject(location));
                    break;
                case EnemyID.UBERKOOPA:
                    objects.Add(objectFactory.CreateUberKoopaObject(location));
                    break;
            }
        }

        public static void LoadAvatar(GameRoot game, List<IGameObjects> objects, LevelDefinition.Objects obstacles, Vector2 location)
        {
            MarioFactory marioFactory = MarioFactory.GetInstance();
            Mario avatar;
            switch (obstacles.avatar)
            {
                case AvatarID.MARIO:
                    if (obstacles.facing == MarioDirection.LEFT)
                    {
                        objects.Add(marioFactory.ReturnMario(location));
                    }
                    else if (obstacles.facing == MarioDirection.RIGHT)
                    {
                        avatar = marioFactory.ReturnMario(location);
                        avatar.FaceRightTransition();
                        objects.Add(avatar);
                    }
                    break;
            }
        }
    }
}
