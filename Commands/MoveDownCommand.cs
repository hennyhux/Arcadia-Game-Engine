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
    public class MoveDownCommand : ICommand
    {
        //private IGameObjects reciever;
        private protected GameRoot game;
        public static int temp = 0;

        public MoveDownCommand(GameRoot game)
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
                !(currentPowerUpState is GameSpace.States.MarioStates.SmallMarioWalkingState)) && 
                (currentPowerUpState is GameSpace.States.MarioStates.BigMarioStandingState ||
                currentPowerUpState is GameSpace.States.MarioStates.FireMarioStandingState))
            {//IF previously standing, then crouch
                game.GetMario.CrouchingTransition();
            }//IF previously Jumping, then stand
            else if (currentPowerUpState is GameSpace.States.MarioStates.BigMarioJumpingState ||
                    currentPowerUpState is GameSpace.States.MarioStates.FireMarioJumpingState)
            {
                game.GetMario.JumpingDiscontinueTransition();
                game.GetMario.StandingTransition();
            }
            else
            {// Just move down
                //game.GetMario.JumpingDiscontinueTransition();
                game.GetMario.StandingTransition();
                game.GetMario.Position = new Vector2(game.GetMario.Position.X, game.GetMario.Position.Y + 10);
            }
        
                game.GetMario.CrouchingTransition();
            //}
           // game.GetMario.Position = new Vector2(game.GetMario.Position.X, game.GetMario.Position.Y + 10);

            //game.GetMario.CrouchingTransition();//Use these seperatly, alongside the same one from the MoveDown class
            //game.GetMario.StandingTransition();// 1 pair alloys jumping other crouching
        

            Debug.WriteLine("UpCommand, facing {0}\n AState {1}\n", game.GetMario.Facing, game.GetMario.marioActionState);
        }

        public void Unexecute()
        {
            //EntityManager.MoveBlock(0, -1);
        }
    }
}
