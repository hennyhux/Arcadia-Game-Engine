using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using static GameSpace.TileMapDefinition.LevelDefinition;
using Microsoft.Xna.Framework.Graphics;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.Interfaces;
using GameSpace.EntitiesManager;
using Microsoft.Xna.Framework;
using GameSpace.Factories;
using GameSpace.Enums;

namespace GameSpace.TileMapDefinition
{
    public static class Loader
    {
        public static int boundaryX = 0;
        private static List<IGameObjects> objects = new List<IGameObjects>();
        private static ObjectFactory objectFactory = ObjectFactory.GetInstance();

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
                LoadExtraItems(objects, obstacles, location);
                LoadEnemies(objects,obstacles, location);
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
                case BlockID.BRICKBLOCK:
                    if (obstacles.blockRow == 1 && obstacles.item == ItemID.COIN)
                    {
                        objects.Add(objectFactory.CreateCoinBrickBlock(location));
                    }
                    else if (obstacles.blockRow == 1 && obstacles.item == ItemID.STAR)
                    {
                        objects.Add(objectFactory.CreateStarBrickBlock(location));
                    }
                    else
                    {
                        for (int i = 0; i < obstacles.blockRow; i++)
                        {
                            objects.Add(objectFactory.CreateBrickBlockObject(new Vector2(obstacles.x + (32 * i), obstacles.y)));
                        }
                    }
                    break;
                case BlockID.QUESTIONBLOCK:
                    if (obstacles.item == ItemID.COIN)
                    {
                        objects.Add(objectFactory.CreateQuestionBlockCoin(location));
                    }
                    else if (obstacles.item == ItemID.STAR)
                    {
                        objects.Add(objectFactory.CreateQuestionBlockStar(location));
                    }
                    else if (obstacles.item == ItemID.ONEUPSHROOM)
                    {
                        objects.Add(objectFactory.CreateQuestionBlockOneUpShroom(location));
                    }
                    else if (obstacles.item == ItemID.SUPERSHROOM)
                    {
                        objects.Add(objectFactory.CreateQuestionBlockShroom(location));
                    }
                    break;
                case BlockID.FLOORBLOCK:
                    for (int i = 0; i < obstacles.blockRow; i++)
                    {
                        objects.Add(objectFactory.CreateFloorBlockObject(new Vector2(obstacles.x + (32 * i), obstacles.y)));
                    }
                    break;
                case BlockID.HIDDENBLOCK:
                    objects.Add(objectFactory.CreateHiddenBlockObject(location));
                    break;
                case BlockID.STAIRBLOCK:
                    for (int i = 0; i < obstacles.blockRow; i++)
                    {
                        objects.Add(objectFactory.CreateStairBlockObject(new Vector2(obstacles.x + (32 * i), obstacles.y)));
                    }
                    break;
            }
        }
        public static void LoadExtraItems(List<IGameObjects> objects, Obstacles obstacles, Vector2 location)
        {
            switch (obstacles.item)
            {
                case ItemID.SMALLPIPE:
                    objects.Add(objectFactory.CreateSmallPipeObject(location));
                    break;
                case ItemID.MEDIUMPIPE:
                    objects.Add(objectFactory.CreateMediumPipeObject(location));
                    break;
                case ItemID.BIGPIPE:
                    objects.Add(objectFactory.CreateBigPipeObject(location));
                    break;
                /* case ItemID.WARPPIPE:
                     objects.Add(objectFactory.CreateBigPipeObject(location));
                     break;*/
                case ItemID.CASTLE:
                    objects.Add(objectFactory.CreateCastleObject(location));
                    break;
                case ItemID.FLAGPOLE:
                    objects.Add(objectFactory.CreateFlagPoleObject(location));
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
