using Microsoft.Xna.Framework;
using System.Diagnostics;
using GameSpace.Interfaces;
using GameSpace.Enums;
using GameSpace.EntitiesManager;

namespace GameSpace
{
    public class ResetCommand : ICommand
    {
        private protected GameRoot MyGame;

        public ResetCommand(GameRoot game)
        {
            this.MyGame = game;
        }

        public void Execute()
        {
            MyGame.Reset();
        }

        public void Unexecute()
        {

        }
    }
}