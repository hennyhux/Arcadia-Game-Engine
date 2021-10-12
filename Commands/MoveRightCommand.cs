using Microsoft.Xna.Framework;
using System.Diagnostics;
using GameSpace.Enums;
using GameSpace.Interfaces;
using GameSpace.EntitiesManager;

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
            game.GetMario.FaceRightTransition();
            //EntityManager.FindItem((int)AvatarID.MARIO).Velocity = new Vector2(EntityManager.FindItem((int)AvatarID.MARIO).Velocity.X + 40, (float)0);
            //game.GetMario.Facing = Enums.eFacing.Right;
            //How to change mario's position
            /*IMarioActionStates currentState = game.GetMario.marioActionState;

            
            if ((currentState is GameSpace.States.MarioStates.SmallMarioStandingState ||
                currentState is GameSpace.States.MarioStates.BigMarioStandingState ||
                currentState is GameSpace.States.MarioStates.FireMarioStandingState) 
                && eFacing.LEFT == game.GetMario.Facing)
            {
                    game.GetMario.FaceRightTransition();
            }
            else
            {
                game.GetMario.FaceRightTransition();
                if (eFacing.LEFT != game.GetMario.Facing)
                {
                    EntityManager.MoveItem((int)AvatarID.MARIO, (int)ControlDirection.RIGHT);
                   // game.GetMario.Position = new Vector2(game.GetMario.Position.X + 10, game.GetMario.Position.Y);
                }
            }

            
            //game.GetMario.Position = new Vector2(game.GetMario.Position.X + 10, game.GetMario.Position.Y);
            //game.GetMario.FaceRightTransition();
            
            //game.GetMario.WalkingTransition();*/
            Debug.WriteLine("RightCommand, facing {0}\n AState {1}\n", game.GetMario.Facing, game.GetMario.marioActionState);
        }

        public void Unexecute()
        {

        }
    }
}