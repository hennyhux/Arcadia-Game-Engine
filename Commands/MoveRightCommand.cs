using Microsoft.Xna.Framework;
using System.Diagnostics;
using GameSpace.Enums;

namespace GameSpace
{
    public class MoveRightCommand : ICommand
    {
        private protected GameRoot game;
        public static int temp = 0;

        public MoveRightCommand(GameRoot game)
        {
            this.game = game;
        }

        public void Execute()
        {
            
            //game.GetMario.Facing = Enums.eFacing.Right;
            game.GetMario.FaceRightTransition();
            Debug.WriteLine("RightCommand, facing {0}\n AState {1}\n", game.GetMario.Facing, game.GetMario.marioActionState);
            //game.GetMario.WalkingTransition();
        }

        public void Unexecute()
        {

        }
    }
}