using GameSpace.Enums;

namespace GameSpace.TileMapDefinition
{
    public class LevelDefinition
    {

        public class Objects
        {
            public int x;
            public int y;
            public BlockID block;
            public int blockRow;
            public ItemID item;
            public int itemRow;
            public EnemyID enemy;
            public AvatarID avatar;
            public MarioDirection facing;
        }
    }
}
