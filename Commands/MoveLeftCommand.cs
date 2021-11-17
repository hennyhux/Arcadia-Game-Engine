using System.Diagnostics;

namespace GameSpace
{
    public class MoveLeftCommand : ICommand
    {
        private protected GameRoot game;
        public static int temp = 0;

        public MoveLeftCommand(GameRoot game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.GetMario.FaceLeftTransition();
         
        }

        public void Unexecute()
        {

        }
    }
}