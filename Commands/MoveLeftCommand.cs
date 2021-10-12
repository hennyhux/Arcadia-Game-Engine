using Microsoft.Xna.Framework;
using System.Diagnostics;
using GameSpace.Interfaces;
using GameSpace.Enums;
using GameSpace.EntitiesManager;

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
            //EntityManager.FindItem((int)AvatarID.MARIO).Velocity = new Vector2(EntityManager.FindItem((int)AvatarID.MARIO).Velocity.X - 40, (float)0);
            //game.GetMario.FaceLeftTransition();
            //How to change mario's position

            //game.GetMario.SprintingTransition();
            /*IMarioActionStates currentState = game.GetMario.marioActionState;

           // 
            if ((currentState is GameSpace.States.MarioStates.SmallMarioStandingState ||
                currentState is GameSpace.States.MarioStates.BigMarioStandingState ||
                currentState is GameSpace.States.MarioStates.FireMarioStandingState)
                && eFacing.RIGHT == game.GetMario.Facing)
            {
                    game.GetMario.FaceLeftTransition();
            }
            else
            {
                game.GetMario.FaceLeftTransition();
                if (eFacing.RIGHT != game.GetMario.Facing)
                {
                    EntityManager.MoveItem((int)AvatarID.MARIO, (int)ControlDirection.LEFT);
                    //game.GetMario.Position = new Vector2(game.GetMario.Position.X - 10, game.GetMario.Position.Y);
                    //game.GetMario.SetPosition(new Vector2(game.GetMario.Position.X, game.GetMario.Position.Y));
                }
            }
            //game.GetMario.Position = new Vector2(game.GetMario.Position.X - 10, game.GetMario.Position.Y);
            //game.GetMario.WalkingTransition();*/
            Debug.WriteLine("LeftCommand, facing {0}\n AState {1}\n", game.GetMario.Facing, game.GetMario.marioActionState);
        }

        public void Unexecute()
        {
            
        }
    }
}