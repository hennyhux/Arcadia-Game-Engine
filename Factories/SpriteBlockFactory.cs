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
        private Texture2D HiddenLevelBrickBlock;
        private Texture2D FloorBlock;
        private Texture2D VineBlock;
        private Texture2D HiddenLevelFloorBlock;
        private Texture2D StairBlock;
        private Texture2D HiddenBlock;
        private Texture2D WhiteRectangle;
        private Texture2D WarpPipeBody;
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
            HiddenLevelBrickBlock = content.Load<Texture2D>("Blocks/HiddenLevelBrickBlock");
            FloorBlock = content.Load<Texture2D>("Blocks/FloorBlock");
            VineBlock = content.Load<Texture2D>("Blocks/VineBlock");
            HiddenLevelFloorBlock = content.Load<Texture2D>("Blocks/HiddenLevelFloorBlock");
            StairBlock = content.Load<Texture2D>("Blocks/StairBlock");
            HiddenBlock = content.Load<Texture2D>("Blocks/UsedBlock");
            WhiteRectangle = content.Load<Texture2D>("WhiteTexture");
            WarpPipeBody = content.Load<Texture2D>("Items/WarpPipeBody");

        }
        public ISprite ReturnQuestionBlock()
        {
            return new QuestionBlockSprite(QuestionBlock, 1, 3, 3, 0, 0);
        }

        public ISprite ReturnBrickBlock()
        {
            return new BrickBlockSprite(BrickBlock, 1, 1, 1);
        }

        public ISprite ReturnHiddenLevelBrickBlock()
        {
            return new BrickBlockSprite(HiddenLevelBrickBlock, 1, 1, 1);
        }

        public ISprite ReturnFloorBlock()
        {
            return new BrickBlockSprite(FloorBlock, 1, 1, 1);
        }

        public ISprite ReturnVineBlock()
        {
            return new VineBlockSprite(VineBlock, 1, 1, 1);
        }

        public ISprite ReturnHiddenLevelFloorBlock()
        {
            return new HiddenLevelFloorBlockSprite(HiddenLevelFloorBlock, 1, 1, 1);
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

        public ISprite CreateWarpPipeBody()
        {
            return new HiddenLevelVerticalPipeSprite(WarpPipeBody, 1, 1, 1);
        }


    }
}
