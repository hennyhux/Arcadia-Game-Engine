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

namespace GameSpace.TileMapDefinition
{
    public static class Loader
    {

        public static void Load(String xmlFile)
        {
            LoadBlocks(xmlFile);
        }

        public static void LoadBlocks(string xmlFile)
        {
            List<Obstacle> obstacle = new List<Obstacle>();
            XmlSerializer serializer = new XmlSerializer(typeof(List<Obstacle>));
            ObjectFactory objectFactory = ObjectFactory.GetInstance();

            ///*
            if (File.Exists("Level.xml"))
            {
                TextReader textReader = new StreamReader("Level.xml");
                obstacle = (List<Obstacle>)serializer.Deserialize(textReader);
                textReader.Close();

                foreach (Obstacle block in obstacle)
                {
                    objectFactory.CreateFloorBlockObject(new Vector2(block.x, block.y));
                }
            }

            objectFactory.CreateFloorBlockObject(new Vector2(268, 68));
            //*/
                
            /*
            using (XmlReader reader = XmlReader.Create("/TileMapDefinition/Level.xml"))
            {
                Obstacle = (List<Obstacle>)serializer.Deserialize(reader);
            }
            */
        }
    }
}
