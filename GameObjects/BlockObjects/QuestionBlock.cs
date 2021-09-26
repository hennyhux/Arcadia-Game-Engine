using GameSpace.Factories;
using GameSpace.Interfaces;
using GameSpace.States.BlockStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.GameObjects.BlockObjects
{
    public class QuestionBlock : IBlockObjects
    {
        private IBlockStates state;

        public QuestionBlock(Game1 game)
        {
           this.state = new QuestionBlockStates(game);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 Location)
        {
            state.Draw(spriteBatch, new Vector2(600, 150));
        }

        public void Trigger()
        {
            state.Initiate();   
        }

        public void Update(GameTime gametime)
        {
            state.Update(gametime);
        }
    }
}
