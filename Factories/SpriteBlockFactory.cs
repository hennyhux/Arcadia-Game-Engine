using GameSpace.Sprites;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.Factories
{
    public class SpriteBlockFactory
    {

        #region sprites
        private Texture2D QuestionBlock;
        private Texture2D ShatterBlock;
        private Texture2D UsedBlock;
        private Texture2D BrickBlock;
        private Texture2D FloorBlock;
        private Texture2D StairBlock;
        private Texture2D HiddenBlock;
        private Texture2D WhiteRectangle;
        #endregion


        private static SpriteBlockFactory instance;
        public static SpriteBlockFactory GetInstance()
        {
            if (instance == null)
            {
                instance = new SpriteBlockFactory();
            }

            return instance;
        }

        private SpriteBlockFactory()
        {
        }

        public void LoadContent(ContentManager content)
        {
            QuestionBlock = content.Load<Texture2D>("Blocks/QuestionBlock");
            ShatterBlock = content.Load<Texture2D>("Blocks/ShatterBrickBlock");
            UsedBlock = content.Load<Texture2D>("Blocks/UsedBlock");
            BrickBlock = content.Load<Texture2D>("Blocks/BrickBlock");
            FloorBlock = content.Load<Texture2D>("Blocks/FloorBlock");
            StairBlock = content.Load<Texture2D>("Blocks/StairBlock");
            HiddenBlock = content.Load<Texture2D>("Blocks/UsedBlock");
            WhiteRectangle = content.Load<Texture2D>("WhiteTexture");
        }
        public ISprite ReturnQuestionBlock()
        {
            return new QuestionBlockSprite(QuestionBlock, 1, 3, 3, 0, 0);
        }

        public ISprite ReturnBrickBlock()
        {
            return new BrickBlockSprite(BrickBlock, 1, 1, 1);
        }

        public ISprite ReturnFloorBlock()
        {
            return new FloorBlockSprite(FloorBlock, 1, 1, 1);
        }

        public ISprite ReturnStairBlock()
        {
            return new StairBlockSprite(StairBlock, 1, 1, 1);
        }

        public ISprite ReturnHiddenBlock()
        {
            return new HiddenBlockSprite(HiddenBlock, 1, 1, 1);
        }

        public ISprite ReturnShatterBlock()
        {
            return new ShatterBlockSprite(ShatterBlock, 1, 1, 1);
        }

        public ISprite ReturnUsedBlock()
        {
            return new UsedBlockSprite(UsedBlock, 1, 1, 1);
        }

        public Texture2D CreateBoundingBoxTexture()
        {
            return WhiteRectangle;
        }
    }
}
