using System;
using System.Collections.Generic;
using System.Text;
using GameSpace.Enums;

namespace GameSpace.TileMapDefinition
{
    class LevelDefinition
    {
        public class Obstacles
        {
            public BlockID block;
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
            public int x;
            public int y;
        }
    }
}
