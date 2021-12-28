using GameSpace.Abstracts;
using GameSpace.States.GameStates;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Handlers
{
    public class ScriptHandler : Handler
    {
        private GameState gameState;
        public ScriptHandler(GameState gameState)
        {
            this.gameState = gameState;
        }

        private InputHandler Input
        {
            get { return gameState.Input; }
        }

        public void Initialize()
        {
            
        }

        private void InititalizeGameControlCommands()
        {
            Input.ExitCommand = new ExitCommand(gameRoot);
            
        }

        private void InitializeMarioCommands()
        {

        }
    }
}
