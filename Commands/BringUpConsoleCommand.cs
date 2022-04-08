using QuakeConsole;

namespace GameSpace.States.GameStates
{
    public class BringUpConsoleCommand : ICommand
    {
        private ConsoleComponent console;
        public BringUpConsoleCommand(GameRoot game)
        {
            this.console = game.console;
        }

        public void Execute()
        {
            console.ToggleOpenClose();
        }

        public void Unexecute()
        {
            throw new System.NotImplementedException();
        }
    }
}