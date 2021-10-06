using Microsoft.Xna.Framework;

namespace GameSpace
{
    public class MoveRightCommand : ICommand
    {
        private protected GameRoot game;

        public MoveRightCommand(GameRoot game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.GetMario.FaceRight();
            game.GetMario.Run();

        }

        public void Unexecute()
        {
            
        }
    }
}