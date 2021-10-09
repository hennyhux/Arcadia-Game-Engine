using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Enums
{
    public enum BlockID
    {
        BRICKBLOCK = 0,
        QUESTIONBLOCK = 1,
        FlOORBLOCK = 2,
        HIDDENBLOCK = 3,
        STAIRBLOCK = 4,
        USEDBLOCK = 5,
    };

    public enum ItemID
    {
        SUPERSHROOM = 6,
        STAR = 7,
        ONEUPSHROOM = 8,
        FIREFLOWER = 9,
        COIN = 10,
        NULL = 11,
    };

    public enum EnemyID
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
        Left = 0,
        Right = 1,
    };

    public enum AvatarID
    {
        Mario = 0,
    };
}
