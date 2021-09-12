
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSpace
{
    public class DrawSpritesCommand : ICommand
    {
        private protected Game1 game;
        private protected int index;

        public DrawSpritesCommand(Game1 game, int id)
        {
            this.game = game;
            index = id;

        }

        public void Execute()
        {
            this.game.SpriteList.ElementAt<ISprite>(index).SetVisible();
        }

        public void Unexecute()
        {
            throw new NotImplementedException();
        }
    }
}

