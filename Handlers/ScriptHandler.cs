using GameSpace.Abstracts;

namespace GameSpace.Handlers
{
    public class ScriptHandler : Handler
    {
        private readonly GameState gameState;
        public ScriptHandler(GameState gameState)
        {
            this.gameState = gameState;
        }

        private InputHandler Input => gameState.Input;

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
