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
        }

        public ISprite ReturnQuestionBlock()
        {
            return new QuestionBlockSprite(QuestionBlock);
        }

        public ISprite ReturnUsedBlock()
        {
            return new UsedBlockSprite(UsedBlock);
        }

        

    }
}
