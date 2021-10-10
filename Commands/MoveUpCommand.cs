using GameSpace.EntitiesManager;
using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

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
            //EntityManager.MoveBlock(0, 1);
            //How to change mario's position
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
                game.GetMario.Position = new Vector2(game.GetMario.Position.X, game.GetMario.Position.Y - 10);

            }
            else
            { //Just move up
                game.GetMario.JumpingTransition();
                game.GetMario.Position = new Vector2(game.GetMario.Position.X, game.GetMario.Position.Y - 10);
            }
            Debug.WriteLine("UpCommand, facing {0}\n AState {1}\n", game.GetMario.Facing, game.GetMario.marioActionState);
        }

        public void Unexecute()
        {
            //EntityManager.MoveBlock(0, -1);
        }
    }
}
