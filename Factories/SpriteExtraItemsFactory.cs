using GameSpace.Sprites;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.Factories
{
    public class SpriteExtraItemsFactory
    {
        #region sprites
        private Texture2D BigPipe;
        private Texture2D MediumPipe;
        private Texture2D SmallPipe;
        private Texture2D FlagPole;
        private Texture2D Castle;

        #endregion

        private static SpriteExtraItemsFactory instance;
        public static SpriteExtraItemsFactory GetInstance()
        {
            if (instance == null)
            {
                instance = new SpriteExtraItemsFactory();
            }

            return instance;
        }

        private SpriteExtraItemsFactory()
        {
        }
        public void LoadContent(ContentManager content)
        {
            BigPipe = content.Load<Texture2D>("ExtraItems/BigPipe");
            MediumPipe = content.Load<Texture2D>("ExtraItems/MediumPipe");
            SmallPipe = content.Load<Texture2D>("ExtraItems/SmallPipe");
            FlagPole = content.Load<Texture2D>("ExtraItems/FlagPole");
            Castle = content.Load<Texture2D>("ExtraItems/Castle");
        }

        public ISprite ReturnBigPipe()
        {
            return new BigPipeSprite(BigPipe, 1, 1, 1);
        }

        public ISprite ReturnMediumPipe()
        {
            return new MediumPipeSprite(MediumPipe, 1, 1, 1);
        }

        public ISprite ReturnSmallPipe()
        {
            return new SmallPipeSprite(SmallPipe, 1, 1, 1);
        }

        public ISprite ReturnFlagPole()
        {
            return new FlagPoleSprite(FlagPole, 1, 5, 5, 0, 0);
        }

        public ISprite ReturnCastle()
        {
            return new CastleSprite(Castle, 1, 1, 1);
        }
    }
}
