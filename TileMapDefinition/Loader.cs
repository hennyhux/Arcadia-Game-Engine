using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using static GameSpace.TileMapDefinition.LevelDefinition;

namespace GameSpace.TileMapDefinition
{
    public static class Loader
    {
        public static int boundaryX = 0;
        private static readonly ObjectFactory objectFactory = ObjectFactory.GetInstance();

        public static List<IGameObjects> Load(string xmlFile)
        {
            List<IGameObjects> objects = new List<IGameObjects>();
            List<Obstacles> fullList = new List<Obstacles>();
            XmlSerializer serializer = new XmlSerializer(typeof(List<Obstacles>), new XmlRootAttribute("Level"));
            using (XmlReader reader = XmlReader.Create(xmlFile))
            {
                fullList = (List<Obstacles>)serializer.Deserialize(reader);
            }
            foreach (Obstacles obstacles in fullList)
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
                LoadAvatar(objects, obstacles, location);
            }
            return objects;
        }

        public static void LoadBackground(List<IGameObjects> objects, Obstacles obstacles, Vector2 location)
        {
            /*Camera camera = new Camera(GraphicsDevice.Viewport) { Limits = new Rectangle(0, 0, Loader.boundaryX, 480) };//Should be set to level's max X and Y
            EntityManager.AddCamera(camera);
            Layer layer = new Layer(camera, BackgroundFactory.GetInstance().CreateRegularBackground(), new Vector2(1.0f, 1.0f));*/
        }
        public static void LoadBlocks(List<IGameObjects> objects, Obstacles obstacles, Vector2 location)
        {
            switch (obstacles.block)
            {
                case BlockID.FLOORBLOCK:
                    for (int i = 0; i < obstacles.blockRow; i++)
                    {
                        objects.Add(objectFactory.CreateFloorBlockObject(new Vector2(obstacles.x + (32 * i), obstacles.y)));
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
                case BlockID.BRICKBLOCK:
                    for (int i = 0; i < obstacles.blockRow; i++)
                    {
                        objects.Add(objectFactory.CreateBrickBlockObject(new Vector2(obstacles.x + (32 * i), obstacles.y)));
                    }
                    break;
                case BlockID.COINBRICKBLOCK:
                    objects.Add(objectFactory.CreateCoinBrickBlock(location));
                    break;
                case BlockID.STARBRICKBLOCK:
                    objects.Add(objectFactory.CreateStarBrickBlock(location));
                    break;
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
            }
        }
        public static void LoadItems(List<IGameObjects> objects, Obstacles obstacles, Vector2 location)
        {
            switch (obstacles.item)
            {
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
                case ItemID.SMALLPIPE:
                    objects.Add(objectFactory.CreateSmallPipeObject(location));
                    break;
                case ItemID.MEDIUMPIPE:
                    objects.Add(objectFactory.CreateMediumPipeObject(location));
                    break;
                case ItemID.BIGPIPE:
                    objects.Add(objectFactory.CreateBigPipeObject(location));
                    break;
                case ItemID.WARPPIPE:
                    //objects.Add(objectFactory.CreateWarpPipeObject(location));
                    break;
                case ItemID.WARPPIPEGOOMBA:
                    //objects.Add(objectFactory.CreateWarpPipeGoombaObject(location));
                    break;
                case ItemID.WARPPIPEGREENKOOPA:
                    //objects.Add(objectFactory.CreateWarpPipeGreenKoopaObject(location));
                    break;
                case ItemID.WARPPIPEREDKOOPA:
                    //objects.Add(objectFactory.CreateWarpPipeRedKoopaObject(location));
                    break;
                case ItemID.WARPPIPENEWENEMY:
                    //objects.Add(objectFactory.CreateWarpPipeNewEnemyObject(location));
                    break;
                case ItemID.FLAGPOLE:
                    objects.Add(objectFactory.CreateFlagPoleObject(location));
                    break;
                case ItemID.CASTLE:
                    objects.Add(objectFactory.CreateCastleObject(location));
                    break;
            }
        }

        public static void LoadEnemies(List<IGameObjects> objects, Obstacles obstacles, Vector2 location)
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
                case EnemyID.NEWENEMYFORSPRINT5:
                    //objects.Add(objectFactory.CreateNewEnemyObject(location));
                    break;
            }
        }

        public static void LoadAvatar(List<IGameObjects> objects, Obstacles obstacles, Vector2 location)
        {
            MarioFactory marioFactory = MarioFactory.GetInstance();
            Mario avatar;
            switch (obstacles.avatar)
            {
                case AvatarID.MARIO:
                    if (obstacles.facing == eFacing.LEFT)
                    {
                        objects.Add(marioFactory.ReturnMario(location));
                    }
                    else if (obstacles.facing == eFacing.RIGHT)
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
