namespace GameSpace.Enums
{
    public enum BlockID
    {
        FLOORBLOCK = 0,
        HIDDENLEVELFLOORBLOCK = 6,
        HIDDENBLOCK = 1,
        VINEHIDDENBLOCK = 222,
        USEDBLOCK = 2,
        STAIRBLOCK = 3,
        BRICKBLOCK = 4,
        HIDDENLEVELBRICKBLOCK = 5,
        COINBRICKBLOCK = 40,
        STARBRICKBLOCK = 400,
        FIREBRICKBLOCK = 4000,
        SUPERSHROOMBRICKBLOCK = 40000,
        ONEUPSHROOMBRICKBLOCK = 400000,
        QUESTIONBLOCK = 7,
        COINQUESTIONBLOCK = 8,
        STARQUESTIONBLOCK = 9,
        ONEUPSHROOMQUESTIONBLOCK = 10,
        SUPERSHROOMQUESTIONBLOCK = 11,
        FIREFLOWERQUESTIONBLOCK = 12,
    };

    public enum ItemID : int
    {
        COIN = 13,
        HIDDENLEVELCOIN = 130,
        HUDCOIN = 131,
        STAR = 14,
        ONEUPSHROOM = 15,
        SUPERSHROOM = 16,
        FIREFLOWER = 17,
        FIREBALL = 18,
        SMALLPIPE = 19,
        MEDIUMPIPE = 20,
        BIGPIPE = 21,
        WARPPIPEHEAD = 22,
        WARPPIPEHEADWITHMOB = 23,
        HIDDENLEVELHORIZONTALPIPE = 200,
        HIDDENLEVELVERTICALPIPE = 2000,
        WARPPIPEROOM = 24,
        WARPPIPEBODY = 25,
        WARPPIPEBACK = 26, //For Sprint 5, we can implement another enemy
        FLAGPOLE = 27,
        CASTLE = 28,
        VINE = 29,
        WARPVINEWITHBLOCK = 50,
        BLACKWINDOW = 999,
        VINEHIDDENBLOCK = 222
    };

    public enum PowerUpID : int
    {
        COIN = 13,
        HIDDENLEVELCOIN = 130,
        STAR = 14,
        ONEUPSHROOM = 15,
        SUPERSHROOM = 16,
        FIREFLOWER = 17,
        FIREBALL = 18,
    };

    public enum EnemyID : int
    {
        GOOMBA = 29,
        UBERGOOMBA = 290,
        GREENKOOPA = 30,
        UBERKOOPA = 300,
        REDKOOPA = 31,
        PLANT = 32,
        LAKITU = 45
    };

    public enum Velocity
    {
        VELOCITYMARIO = 33,
        VELOCITYGOOMBA = 34,
        VELOCITYGREENKOOPA = 35,
        VELOCITYREDKOOPA = 36,
        VELOCITYNEWENEMY = 37 //For Sprint 5, we can implement velocity for new enemy
    };

    public enum MarioDirection
    {
        LEFT = 38,
        RIGHT = 39
    };

    public enum AvatarID
    {
        MARIO = 1984,
    };

    public enum CollisionDirection
    {
        UP, // NEEDS FIXED! If this enum is changed, Mario will do nothing when it is supposed to kill an enemy. This might be HARDCODED somewhere. NEEDS FIXED!
        DOWN = 42,
        LEFT = 43,
        RIGHT = 44
    };
}
