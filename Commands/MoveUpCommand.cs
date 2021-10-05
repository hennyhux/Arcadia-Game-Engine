using GameSpace.EntitiesManager;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Commands
{
    public class MoveUpCommand : ICommand
    {
        private protected Game1 game;

        public MoveUpCommand(Game1 game)
        {
            this.game = game;
        }

        public void Execute()
        {
            EntityManager.AccessItem(0).SetPosition(new Vector2(0, 1));
        }

        public void Unexecute()
        {

        }
    }
}
