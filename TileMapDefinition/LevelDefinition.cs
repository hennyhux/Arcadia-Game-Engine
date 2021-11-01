using GameSpace.Enums;

namespace GameSpace.TileMapDefinition
{
    public class LevelDefinition
    {

        public class Obstacles
        {
            public int x;
            public int y;
            public BlockID block;
            public int blockRow;
            public bool hiddenItem;
            public ItemID item;
            public EnemyID enemy;
            public AvatarID avatar;
            public eFacing facing;
        }

        public class Obstacle
        {
            public BlockID block;
            public ItemID item;
            public int x;
            public int y;
        }
        public class Enemy
        {
            public EnemyID enemy;
            public int x;
            public int y;
        }
        public class Item
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
