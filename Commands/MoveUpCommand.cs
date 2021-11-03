namespace GameSpace.Commands
{
    public class MoveUpCommand : ICommand
    {
        //private IGameObjects reciever;
        private protected GameRoot game;
        public static int temp = 0;

        public MoveUpCommand(GameRoot game)
        {
            this.game = game;
        }

        public void Execute()
        {
            //EntityManager.FindItem((int)AvatarID.MARIO).Velocity = new Vector2((float)0, EntityManager.FindItem((int)AvatarID.MARIO).Velocity.Y - 40);
            //game.GetMario.JumpingTransition();
            game.GetMario.UpTransition();
            /*if(game.GetMario.marioActionState is BigMarioJumpingState || game.GetMario.marioActionState is FireMarioJumpingState)
            {
                //play super jumping 
                game.soundEffects[1].CreateInstance().Play();

            }
            else if (game.GetMario.marioActionState is SmallMarioJumpingState)
            {
                //play standard jumping 
                game.soundEffects[0].CreateInstance().Play();

            }*/

            /*EntityManager.FindItem((int)AvatarID.MARIO).Velocity = new Vector2((float)0, EntityManager.FindItem((int)AvatarID.MARIO).Velocity.Y - 40);
            IMarioActionStates currentState = game.GetMario.marioActionState;
            if ((!(currentState is GameSpace.States.MarioStates.SmallMarioFallingState) &&
                !(currentState is GameSpace.States.MarioStates.SmallMarioJumpingState) &&
                !(currentState is GameSpace.States.MarioStates.SmallMarioRunningState) &&
                !(currentState is GameSpace.States.MarioStates.SmallMarioStandingState) &&
                !(currentState is GameSpace.States.MarioStates.SmallMarioWalkingState) &&
                (currentState is GameSpace.States.MarioStates.BigMarioCrouchingState ||
                currentState is GameSpace.States.MarioStates.FireMarioCrouchingState)))
            { //IF previously Crouching, then stand
                game.GetMario.StandingTransition();
            }
            else if (currentState is GameSpace.States.MarioStates.BigMarioStandingState ||
                    currentState is GameSpace.States.MarioStates.FireMarioStandingState)
            { //IF previously Standing, then jump and move
                game.GetMario.JumpingTransition();
                EntityManager.FindItem((int)AvatarID.MARIO).Velocity = new Vector2((float)0, EntityManager.FindItem((int)AvatarID.MARIO).Velocity.Y - 40);
            }
            else
            { //Just move up
                game.GetMario.JumpingTransition();
                EntityManager.FindItem((int)AvatarID.MARIO).Velocity = new Vector2((float)0, EntityManager.FindItem((int)AvatarID.MARIO).Velocity.Y - 40);
            }
            Debug.WriteLine("UpCommand, facing {0}\n AState {1}\n", game.GetMario.Facing, game.GetMario.marioActionState);*/
        }

        public void Unexecute()
        {

        }
    }
}
