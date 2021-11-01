using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using static GameSpace.TileMapDefinition.LevelDefinition;

namespace GameSpace.TileMapDefinition
{
    public static class Loader
    {
        public static int boundaryX = 0;
        public static List<IGameObjects> Load(String xmlFile)
        {
            // List<IGameObjects> fullList = LoadEverything(xmlFile);
            List<IGameObjects> fullList = LoadBlocks(xmlFile);
            List<IGameObjects> itemsList = LoadItems(xmlFile);

            fullList.AddRange(itemsList);
            List<IGameObjects> enemiesList = LoadEnemies(xmlFile);
            fullList.AddRange(enemiesList);
            return fullList;
        }
        public static List<IGameObjects> LoadEverything(string xmlFile)
        {
            List<IGameObjects> objectsList = new List<IGameObjects>();
            List<Obstacles> fullList = new List<Obstacles>();
            MarioFactory marioFactory = MarioFactory.GetInstance();
            Mario avatar;
            ObjectFactory objectFactory = ObjectFactory.GetInstance();
            XmlSerializer serializer = new XmlSerializer(typeof(List<Obstacles>), new XmlRootAttribute("Level"));
            using (XmlReader reader = XmlReader.Create(xmlFile))
            {
                fullList = (List<Obstacles>)serializer.Deserialize(reader);
            }
            foreach (Obstacles obstacles in fullList)
            {
                Vector2 location = new Vector2(obstacles.x, obstacles.y);
                if (boundaryX < obstacles.x) boundaryX = obstacles.x;

                switch (obstacles.avatar)
                {
                    case AvatarID.MARIO:
                        if (obstacles.facing == eFacing.LEFT)
                        {
                            objectsList.Add(marioFactory.ReturnMario(location));
                        }
                        else if (obstacles.facing == eFacing.RIGHT)
                        {
                            avatar = marioFactory.ReturnMario(location);
                            avatar.FaceRightTransition();
                            objectsList.Add(avatar);
                        }
                        break;
                }
                switch (obstacles.block)
                {
                    case BlockID.BRICKBLOCK:
                        if (obstacles.blockRow == 1 && obstacles.item == ItemID.COIN)
                        {
                            objectsList.Add(objectFactory.CreateCoinBrickBlock(location));
                        }
                        else if (obstacles.blockRow == 1 && obstacles.item == ItemID.STAR)
                        {
                            objectsList.Add(objectFactory.CreateStarBrickBlock(location));
                        }
                        else
                        {
                            for (int i = 0; i < obstacles.blockRow; i++)
                            {
                                objectsList.Add(objectFactory.CreateBrickBlockObject(new Vector2(obstacles.x + (32 * i), obstacles.y)));
                            }
                        }
                        break;
                    case BlockID.QUESTIONBLOCK:
                        if (obstacles.item == ItemID.COIN) objectsList.Add(objectFactory.CreateQuestionBlockCoin(location));
                        else if (obstacles.item == ItemID.STAR) objectsList.Add(objectFactory.CreateQuestionBlockStar(location));
                        else if (obstacles.item == ItemID.ONEUPSHROOM) objectsList.Add(objectFactory.CreateQuestionBlockOneUpShroom(location));
                        else if (obstacles.item == ItemID.SUPERSHROOM) objectsList.Add(objectFactory.CreateQuestionBlockShroom(location));
                        break;
                    case BlockID.FLOORBLOCK:
                        for (int i = 0; i < obstacles.blockRow; i++)
                        {
                            objectsList.Add(objectFactory.CreateFloorBlockObject(new Vector2(obstacles.x + (32 * i), obstacles.y)));
                        }
                        break;
                    case BlockID.HIDDENBLOCK:
                        objectsList.Add(objectFactory.CreateHiddenBlockObject(location));
                        break;
                    case BlockID.STAIRBLOCK:
                        for (int i = 0; i < obstacles.blockRow; i++)
                        {
                            objectsList.Add(objectFactory.CreateStairBlockObject(new Vector2(obstacles.x + (32 * i), obstacles.y)));
                        }
                        break;
                    case BlockID.USEDBLOCK:
                        objectsList.Add(objectFactory.CreateUsedBlockObject(location));
                        break;
                }
                switch (obstacles.enemy)
                {
                    case EnemyID.GOOMBA:
                        objectsList.Add(objectFactory.CreateGoombaObject(location));
                        break;
                    case EnemyID.GREENKOOPA:
                        objectsList.Add(objectFactory.CreateGreenKoopaObject(location));
                        break;
                    case EnemyID.REDKOOPA:
                        objectsList.Add(objectFactory.CreateRedKoopaObject(location));
                        break;
                }
                switch (obstacles.item)
                {
                    case ItemID.SMALLPIPE:
                        objectsList.Add(objectFactory.CreateSmallPipeObject(location));
                        break;
                    case ItemID.MEDIUMPIPE:
                        objectsList.Add(objectFactory.CreateMediumPipeObject(location));
                        break;
                    case ItemID.BIGPIPE:
                        objectsList.Add(objectFactory.CreateBigPipeObject(location));
                        break;
                    case ItemID.CASTLE:
                        objectsList.Add(objectFactory.CreateCastleObject(location));
                        break;
                    case ItemID.FLAGPOLE:
                        objectsList.Add(objectFactory.CreateFlagPoleObject(location));
                        break;
                }
            }
            return objectsList;
        }

        public static List<IGameObjects> LoadBlocks(string xmlFile)
        {
            List<IGameObjects> objectsList = new List<IGameObjects>();
            List<Obstacle> obstacleList = new List<Obstacle>();
            ObjectFactory objectFactory = ObjectFactory.GetInstance();
            XmlSerializer serializer = new XmlSerializer(typeof(List<Obstacle>), new XmlRootAttribute("Level"));
            using (XmlReader reader = XmlReader.Create(xmlFile))
            {
                obstacleList = (List<Obstacle>)serializer.Deserialize(reader);
            }
            foreach (Obstacle obstacleObject in obstacleList)
            {
                Vector2 location = new Vector2(obstacleObject.x, obstacleObject.y);
                if (boundaryX < obstacleObject.x) boundaryX = obstacleObject.x;
                switch (obstacleObject.block)
                {
                    case BlockID.BRICKBLOCK:
                        if (obstacleObject.item == ItemID.NOITEM) objectsList.Add(objectFactory.CreateBrickBlockObject(location));
                        if (obstacleObject.item == ItemID.COIN) objectsList.Add(objectFactory.CreateCoinBrickBlock(location));
                        if (obstacleObject.item == ItemID.STAR) objectsList.Add(objectFactory.CreateStarBrickBlock(location));
                        if (obstacleObject.item == ItemID.ONEUPSHROOM) objectsList.Add(objectFactory.CreateOneUpShroomBrickBlock(location));
                        if (obstacleObject.item == ItemID.SUPERSHROOM) objectsList.Add(objectFactory.CreateSuperShroomBrickBlock(location));
                        if (obstacleObject.item == ItemID.FIREFLOWER) objectsList.Add(objectFactory.CreateFireBrickBlock(location));
                        break;

                    case BlockID.QUESTIONBLOCK:
                        if (obstacleObject.item == ItemID.NOITEM) objectsList.Add(objectFactory.CreateQuestionBlockObject(location));
                        if (obstacleObject.item == ItemID.STAR) objectsList.Add(objectFactory.CreateQuestionBlockStar(location));
                        if (obstacleObject.item == ItemID.ONEUPSHROOM) objectsList.Add(objectFactory.CreateQuestionBlockOneUpShroom(location));
                        if (obstacleObject.item == ItemID.SUPERSHROOM) objectsList.Add(objectFactory.CreateQuestionBlockShroom(location));
                        if (obstacleObject.item == ItemID.FIREFLOWER) objectsList.Add(objectFactory.CreateQuestionBlockFire(location));
                        break;
                    case BlockID.FLOORBLOCK:
                        objectsList.Add(objectFactory.CreateFloorBlockObject(location));
                        break;
                    case BlockID.HIDDENBLOCK:
                        objectsList.Add(objectFactory.CreateHiddenBlockObject(location));
                        break;
                    case BlockID.STAIRBLOCK:
                        objectsList.Add(objectFactory.CreateStairBlockObject(location));
                        break;
                    case BlockID.USEDBLOCK:
                        objectsList.Add(objectFactory.CreateUsedBlockObject(location));
                        break;
                    case BlockID.COINBRICKBLOCK:
                        objectsList.Add(objectFactory.CreateCoinBrickBlock(location));
                        break;

                }

                //Not part of sprint 1: Add some kind of method that makes these items non-visible to start with.
                //Vector2 itemLocation = new Vector2(obstacleObject.x, obstacleObject.y-32);
                //switch (obstacleObject.item)
                //{
                //    case ItemID.SUPERSHROOM:
                //        objectsList.Add(objectFactory.CreateSuperShroomObject(new Vector2(obstacleObject.x - 4, obstacleObject.y - 36)));
                //        break;
                //    case ItemID.STAR:
                //        objectsList.Add(objectFactory.CreateStarObject(new Vector2(obstacleObject.x - 4, obstacleObject.y - 36)));
                //        break;
                //    case ItemID.ONEUPSHROOM:
                //        objectsList.Add(objectFactory.CreateOneUpShroomObject(new Vector2(obstacleObject.x - 4, obstacleObject.y - 36)));
                //        break;
                //    case ItemID.FIREFLOWER:
                //        objectsList.Add(objectFactory.CreateFireFlowerObject(new Vector2(obstacleObject.x - 4, obstacleObject.y - 36)));
                //        break;
                //    case ItemID.COIN:
                //        objectsList.Add(objectFactory.CreateCoinObject(new Vector2(obstacleObject.x + 6, obstacleObject.y - 29)));
                //        break;
                //}
            }
            return objectsList;
        }

        public static List<IGameObjects> LoadItems(string xmlFile)
        {
            List<IGameObjects> objectsList = new List<IGameObjects>();
            List<Item> itemList = new List<Item>();
            ObjectFactory objectFactory = ObjectFactory.GetInstance();
            XmlSerializer serializer = new XmlSerializer(typeof(List<Item>), new XmlRootAttribute("Level"));
            using (XmlReader reader = XmlReader.Create(xmlFile))
            {
                itemList = (List<Item>)serializer.Deserialize(reader);
            }
            foreach (Item itemObject in itemList)
            {
                Vector2 location = new Vector2(itemObject.x, itemObject.y);
                if (boundaryX < itemObject.x) boundaryX = itemObject.x;
                switch (itemObject.item)
                {
                    case ItemID.SUPERSHROOM:
                        objectsList.Add(objectFactory.CreateSuperShroomObject(location));
                        break;
                    case ItemID.STAR:
                        objectsList.Add(objectFactory.CreateStarObject(location));
                        break;
                    case ItemID.ONEUPSHROOM:
                        objectsList.Add(objectFactory.CreateOneUpShroomObject(location));
                        break;
                    case ItemID.FIREFLOWER:
                        objectsList.Add(objectFactory.CreateFireFlowerObject(location));
                        break;
                    case ItemID.COIN:
                        objectsList.Add(objectFactory.CreateCoinObject(location));
                        break;
                }
            }
            return objectsList;
        }

        public static List<IGameObjects> LoadEnemies(string xmlFile)
        {
            List<IGameObjects> objectsList = new List<IGameObjects>();
            List<Enemy> enemyList = new List<Enemy>();
            ObjectFactory objectFactory = ObjectFactory.GetInstance();
            XmlSerializer serializer = new XmlSerializer(typeof(List<Enemy>), new XmlRootAttribute("Level"));
            using (XmlReader reader = XmlReader.Create(xmlFile))
            {
                enemyList = (List<Enemy>)serializer.Deserialize(reader);
            }
            foreach (Enemy enemyObject in enemyList)
            {
                Vector2 location = new Vector2(enemyObject.x, enemyObject.y);
                if (boundaryX < enemyObject.x) boundaryX = enemyObject.x;

                switch (enemyObject.enemy)
                {
                    case EnemyID.GOOMBA:
                        objectsList.Add(objectFactory.CreateGoombaObject(location));
                        break;
                    case EnemyID.GREENKOOPA:
                        objectsList.Add(objectFactory.CreateGreenKoopaObject(location));
                        break;
                    case EnemyID.REDKOOPA:
                        objectsList.Add(objectFactory.CreateRedKoopaObject(location));
                        break;
                }
            }
            return objectsList;
        }

        public static List<IGameObjects> LoadAvatars(string xmlFile)
        {
            List<IGameObjects> avatarsList = new List<IGameObjects>();
            MarioFactory marioFactory = MarioFactory.GetInstance();
            Mario avatar;
            List<Avatar> avatarList = new List<Avatar>();
            XmlSerializer serializer = new XmlSerializer(typeof(List<Avatar>), new XmlRootAttribute("Level"));
            using (XmlReader reader = XmlReader.Create(xmlFile))
            {
                avatarList = (List<Avatar>)serializer.Deserialize(reader);
            }
            foreach (Avatar avatarObject in avatarList)
            {
                Vector2 location = new Vector2(avatarObject.x, avatarObject.y);
                if (boundaryX < avatarObject.x) boundaryX = avatarObject.x;
                switch (avatarObject.facing)
                {
                    case eFacing.LEFT:
                        avatarsList.Add(marioFactory.ReturnMario(location));
                        break;
                    case eFacing.RIGHT:
                        avatar = marioFactory.ReturnMario(location);
                        avatar.FaceRightTransition();
                        avatarsList.Add(avatar);
                        break;
                }
            }
            return avatarsList;
        }
    }
}

