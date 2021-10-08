using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using static GameSpace.TileMapDefinition.LevelDefinition;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.EntitiesManager;
using GameSpace.Interfaces;

namespace GameSpace.TileMapDefinition
{
    public static class Loader
    {

        public static void Load()
        {
            LoadBlocks();
        }

        public static void LoadBlocks()
        {
            List<Obstacles> obstacles = new List<Obstacles>();
            XmlSerializer serializer = new XmlSerializer(typeof(List<Obstacles>), new XmlRootAttribute("Level"));
            /*using (XmlReader reader = XmlReader.Create("../TileMapDefinition/Level.xml"))
            {
                obstacles = (List<Obstacles>)serializer.Deserialize(reader);
            } */
        }
    }
}
