using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using static GameSpace.TileMapDefinition.LevelDefinition;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.EntitiesManager;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using GameSpace.Factories;
using GameSpace.Enums;

namespace GameSpace.TileMapDefinition
{
    public static class Loader
    {

        public static List<IGameObjects> Load(String xmlFile)
        {
            List<IGameObjects> fullList = LoadBlocks(xmlFile);
            List<IGameObjects> itemsList = LoadItems(xmlFile);
            fullList.AddRange(itemsList);
            List<IGameObjects> enemiesList = LoadEnemies(xmlFile);
            fullList.AddRange(enemiesList);
            return fullList;
        }

        public static List<IGameObjects> LoadBlocks(string xmlFile)
        {
            List<IGameObjects> objectsList = new List<IGameObjects>();
            List<Obstacle> obstacleList = new List<Obstacle>();
            ObjectFactory objectFactory = ObjectFactory.GetInstance();
            XmlSerializer serializer = new XmlSerializer(typeof(List<Obstacle>), new XmlRootAttribute("Level"));
            using (XmlReader reader = XmlReader.Create(xmlFile))// IF YOU GET AN ERROR HERE, GO TO GameRoot.cs and change the xmlFILE path to your PATH to the Level File(on YOUR PC)
            {
                obstacleList = (List<Obstacle>)serializer.Deserialize(reader);
            }
            foreach (Obstacle obstacleObject in obstacleList)
            {
                Vector2 location = new Vector2(obstacleObject.x, obstacleObject.y);
                switch (obstacleObject.block)
                {
                    case BlockID.BRICKBLOCK:
                        objectsList.Add(objectFactory.CreateBrickBlockObject(location));
                        break;

                    case BlockID.QUESTIONBLOCK:
                        objectsList.Add(objectFactory.CreateQuestionBlockObject(location));
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
                }

                //Not part of sprint 1: Add some kind of method that makes these items non-visible to start with.
                Vector2 itemLocation = new Vector2(obstacleObject.x, obstacleObject.y-32);
                switch (obstacleObject.item)
                {
                    case ItemID.SUPERSHROOM:
                        objectsList.Add(objectFactory.CreateSuperShroomObject(new Vector2(obstacleObject.x - 4, obstacleObject.y - 36)));
                        break;
                    case ItemID.STAR:
                        objectsList.Add(objectFactory.CreateStarObject(new Vector2(obstacleObject.x - 4, obstacleObject.y - 36)));
                        break;
                    case ItemID.ONEUPSHROOM:
                        objectsList.Add(objectFactory.CreateOneUpShroomObject(new Vector2(obstacleObject.x - 4, obstacleObject.y - 36)));
                        break;
                    case ItemID.FIREFLOWER:
                        objectsList.Add(objectFactory.CreateFireFlowerObject(new Vector2(obstacleObject.x - 4, obstacleObject.y - 36)));
                        break;
                    case ItemID.COIN:
                        objectsList.Add(objectFactory.CreateCoinObject(new Vector2(obstacleObject.x + 6, obstacleObject.y - 29)));
                        break;
                }
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

