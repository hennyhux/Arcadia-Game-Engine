namespace GameSpace.Commands
{
    public class StateDeadMarioCommand : ICommand
    {
        private protected GameRoot game;

        public StateDeadMarioCommand(GameRoot game)
        {
            this.game = game;
        }
        public void Execute()
        {
            //game.GetMario.Dead();
            game.GetMario.DeadTransition();
        }

        public void Unexecute()
        {
            throw new System.NotImplementedException();
        }
    }
}
