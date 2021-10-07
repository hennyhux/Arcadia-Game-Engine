using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Enums
{
    public enum BlockID
    {
        BRICKBLOCK,
        QUESTIONBLOCK,
        FlOORBLOCK,
        HIDDENBLOCK,
        STAIRBLOCK,
        USEDBLOCK,
    };

    public enum ItemID
    {
        SUPERSHROOM = 6,
        STAR = 7,
        ONEUPSHROOM = 8,
        FIREFLOWER = 9,
        COIN = 10,
    };

    public enum EnemyID
    {
        GOOMBA = 11,
        GREENKOOPA = 12,
        REDKOOPA = 13
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

    public enum eFacing
    {
        Left = 0,
        Right = 1,
    };
}
