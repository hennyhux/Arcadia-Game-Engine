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
            public ItemID item;
            public EnemyID enemy;
            public AvatarID avatar;
            public eFacing facing;
        }
    }
}
