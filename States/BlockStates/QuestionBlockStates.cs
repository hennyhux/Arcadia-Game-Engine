using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.States.BlockStates
{
    public class QuestionBlockStates : IBlockStates
    {
        private ISprite sprite;
        private BlockSpriteFactory blockFactory;
        private bool triggered;

        public QuestionBlockStates(Game1 game)
        {
            blockFactory = game.BlockFactory;
            sprite = blockFactory.ReturnQuestionBlock();
            triggered = false;
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 Location)
        {
            sprite.Draw(spriteBatch, new Vector2(400, 150));
        }

        public void Initiate()
        {
            sprite = blockFactory.ReturnUsedBlock(new Vector2(900, 150));
        }

        public void Update(GameTime gametime)
        {
            sprite.Update(gametime);
        }
    }
}
