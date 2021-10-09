using System;
using System.Collections.Generic;
using System.Text;
using GameSpace.Enums;

namespace GameSpace.TileMapDefinition
{
    public class LevelDefinition
    {
        public class Obstacle
        {
            public int block;
            public ItemID item;
            public int x;
            public int y;
        }
        public class Enemies
        {
            public EnemyID enemy;
            public int x;
            public int y;
        }
        public class Items
        {
            public ItemID item;
            public int x;
            public int y;
        }
        public class Avatar
        {
            public AvatarID avatar;
            public eFacing facing;
            public int x;
            public int y;
        }
    }
}
