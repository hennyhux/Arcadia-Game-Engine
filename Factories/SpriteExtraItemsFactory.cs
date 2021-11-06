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
        private Texture2D HiddenLevelHorizontalPipe;
        private Texture2D HiddenLevelVerticalPipe;
        private Texture2D FlagPole;
        private Texture2D Castle;
        private Texture2D BlackWindow;

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
            HiddenLevelHorizontalPipe = content.Load<Texture2D>("ExtraItems/HiddenHorizontalPipe");
            HiddenLevelVerticalPipe = content.Load<Texture2D>("ExtraItems/HiddenVerticalPipe");
            FlagPole = content.Load<Texture2D>("ExtraItems/FlagPole");
            Castle = content.Load<Texture2D>("ExtraItems/Castle");
            BlackWindow = content.Load<Texture2D>("BlackWindow");
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

        public ISprite ReturnHiddenLevelHorizontalPipe()
        {
            return new HiddenLevelHorizontalPipeSprite(HiddenLevelHorizontalPipe, 1, 1, 1);
        }

        public ISprite ReturnHiddenLevelVerticalPipe()
        {
            return new HiddenLevelVerticalPipeSprite(HiddenLevelVerticalPipe, 1, 1, 1);
        }

        public ISprite ReturnFlagPole()
        {
            return new FlagPoleSprite(FlagPole, 1, 5, 5, 0, 0);
        }

        public ISprite ReturnCastle()
        {
            return new CastleSprite(Castle, 1, 1, 1);
        }

        public ISprite ReturnBlackWindow()
        {
            return new BlackWindowSprite(BlackWindow, 1, 1, 1);
        }
    }
}
