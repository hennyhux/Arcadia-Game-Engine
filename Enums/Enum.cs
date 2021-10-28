using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Enums
{
    public enum BlockID
    {
        BRICKBLOCK = 0,
        QUESTIONBLOCK = 1,
        FLOORBLOCK = 2,
        HIDDENBLOCK = 3,
        STAIRBLOCK = 4,
        USEDBLOCK = 5,
        COINBRICKBLOCK = 17,
        BRICKBLOCKSTAR = 18
    };

    public enum ItemID: int
    {
        SUPERSHROOM = 6,
        STAR = 7,
        ONEUPSHROOM = 8,
        FIREFLOWER = 9,
        COIN = 100,
        NOITEM = 110,
        BIGPIPE = 120,
        MEDIUMPIPE = 130,
        SMALLPIPE = 140,
        FLAGPOLE = 150,
        CASTLE = 160,
        FIREBALL = 19
    };

    public enum EnemyID: int
    {
        GOOMBA = 12,
        GREENKOOPA = 13,
        REDKOOPA = 14
    };

    public enum ControlDirection
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    };

    public enum CollisionDirection
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    };

    public enum Velocity
    {
        VELOCITYGOOMBA
    };

    public enum eFacing
    {
        LEFT = 0,
        RIGHT = 1,
    };

    public enum AvatarID
    {
        MARIO = 16,
    };

}
