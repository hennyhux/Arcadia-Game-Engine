using GameSpace.Sprites;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.Factories
{
    public class SpriteItemFactory
    {
        #region sprites
        private Texture2D Star;
        private Texture2D Coin;
        private Texture2D HiddenLevelCoin;
        private Texture2D OneUpShroom;
        private Texture2D SuperShroom;
        private Texture2D FireFlower;
        private Texture2D FireBall;
        private Texture2D HitFireball;
        private Texture2D WarpPipeHead;
        private Texture2D Vine;
        #endregion
        private static readonly SpriteItemFactory instance = new SpriteItemFactory();
        public static SpriteItemFactory GetInstance()
        {
            return instance;
        }

        private SpriteItemFactory()
        {

        }

        public void LoadContent(ContentManager content)
        {
            Star = content.Load<Texture2D>("Items/star");
            Coin = content.Load<Texture2D>("Items/coin");
            HiddenLevelCoin = content.Load<Texture2D>("Items/HiddenLevelCoin");
            OneUpShroom = content.Load<Texture2D>("Items/1upshroom");
            SuperShroom = content.Load<Texture2D>("Items/supermushroom");
            FireFlower = content.Load<Texture2D>("Items/flower");
            FireBall = content.Load<Texture2D>("Items/FireBalls");
            WarpPipeHead = content.Load<Texture2D>("Items/WarpPipeHead");
            HitFireball = content.Load<Texture2D>("Items/HitFireballs");
            Vine = content.Load<Texture2D>("Items/Vine");
        }
        public ISprite CreateStar()
        {
            return new AnimatedSprite(Star, 1, 4, 4, 0, 0);
        }

        public ISprite CreateSuperShroom()
        {
            return new AnimatedSprite(SuperShroom, 1, 1, 1, 0, 0);
        }

        public ISprite CreateOneUpShroom()
        {
            return new AnimatedSprite(OneUpShroom, 1, 1, 1, 0, 0);
        }

        public ISprite CreateFireFlower()
        {
            return new AnimatedSprite(FireFlower, 1, 4, 4, 0, 0);
        }

        public ISprite CreateCoin()
        {
            return new AnimatedSprite(Coin, 1, 4, 4, 0, 0);
        }

        public ISprite CreateHiddenLevelCoin()
        {
            return new AnimatedSprite(HiddenLevelCoin, 1, 3, 3, 0, 0);
        }

        public ISprite CreateFireBall()
        {
            return new AnimatedSprite(FireBall, 1, 4, 4, 0, 0);
        }

        public ISprite CreateHitFireBall()
        {
            return new AnimatedSprite(HitFireball, 1, 1, 1, 0, 0);
        }

        public ISprite CreateWarpPipeHead()
        {
            return new StaticSprite(WarpPipeHead, 1, 1, 1);
        }
        public ISprite CreateVine()
        {
            return new AnimatedSprite(Vine, 1, 1, 1, 0, 0);
        }
        public ISprite CreateWarpPipeHeadBack()
        {
            return new StaticSprite(WarpPipeHead, 1, 1, 1);
        }
    }
}
