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
            IMarioPowerUpStates currentPowerUpState = game.GetMario.marioPowerUpState;
            if ((!(currentPowerUpState is GameSpace.States.MarioStates.SmallMarioFallingState) &&
                !(currentPowerUpState is GameSpace.States.MarioStates.SmallMarioJumpingState) &&
                !(currentPowerUpState is GameSpace.States.MarioStates.SmallMarioRunningState) &&
                !(currentPowerUpState is GameSpace.States.MarioStates.SmallMarioStandingState) &&
                !(currentPowerUpState is GameSpace.States.MarioStates.SmallMarioWalkingState) &&
                (currentPowerUpState is GameSpace.States.MarioStates.BigMarioCrouchingState ||
                currentPowerUpState is GameSpace.States.MarioStates.FireMarioCrouchingState)))
            { //IF previously Crouching, then stand
                game.GetMario.CrouchingDiscontinueTransition();
                game.GetMario.StandingTransition();
            }
            else if (currentPowerUpState is GameSpace.States.MarioStates.BigMarioStandingState ||
                    currentPowerUpState is GameSpace.States.MarioStates.FireMarioStandingState)
            { //IF previously Standing, then jump
                game.GetMario.WalkingDiscontinueTransition();
                game.GetMario.JumpingTransition();
            }
            else
            { //Just move up
                game.GetMario.JumpingTransition();
                game.GetMario.Position = new Vector2(game.GetMario.Position.X, game.GetMario.Position.Y - 10);
            }
            
            //game.GetMario.StandingTransition();//Use these seperatly, alongside the same one from the MoveDown class
            //game.GetMario.JumpingTransition();// 1 pair alloys jumping other crouching

            Debug.WriteLine("UpCommand, facing {0}\n AState {1}\n", game.GetMario.Facing, game.GetMario.marioActionState);
        }

        public void Unexecute()
        {
            //EntityManager.MoveBlock(0, -1);
        }
    }
}
