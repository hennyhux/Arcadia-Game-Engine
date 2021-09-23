using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;
using GameSpace.Sprites;

namespace GameSpace.Factories
{
    public class BlockFactory
    {

        #region sprites
        private Texture2D QuestionBlock;
        private Texture2D UsedBlock;
        private Texture2D BrickBlock;
        private Texture2D FloorBlock;
        private Texture2D StairBlock;
        private Texture2D HiddenBlock;
        #endregion

        private static BlockFactory instance = new BlockFactory();
        public static BlockFactory Instance => instance;
        public BlockFactory()
        {

        }

        public void LoadContent(ContentManager content)
        {
            QuestionBlock = content.Load<Texture2D>("Blocks/QuestionBlock");
            UsedBlock = content.Load<Texture2D>("Blocks/UsedBlock");
            BrickBlock = content.Load<Texture2D>("Blocks/BrickBlock");
            FloorBlock = content.Load<Texture2D>("Blocks/FloorBlock");
            StairBlock = content.Load<Texture2D>("Blocks/StairBlock");
            HiddenBlock = content.Load<Texture2D>("Blocks/HiddenBlock");
        } 
        public ISprite ReturnQuestionBlock()
        {
            return new QuestionBlockSprite(QuestionBlock, 1, 3, 3, 0, 0);
        }

        public ISprite ReturnUsedBlock()
        {
            return new UsedBlockSprite(UsedBlock, 1, 1, 1);
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
    }
}
