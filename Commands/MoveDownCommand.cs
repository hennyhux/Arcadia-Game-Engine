using GameSpace.EntitiesManager;
using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using GameSpace.Enums;

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
            //EntityManager.FindItem((int)AvatarID.MARIO).Velocity = new Vector2((float)0, EntityManager.FindItem((int)AvatarID.MARIO).Velocity.Y + 40);

            IMarioActionStates currentState = game.GetMario.marioActionState;
            IMarioPowerUpStates currentstate2 = game.GetMario.marioPowerUpState;
            // if (currentstate2 is GameSpace.States.MarioStates.)
            if ((!(currentState is GameSpace.States.MarioStates.SmallMarioFallingState) &&
                !(currentState is GameSpace.States.MarioStates.SmallMarioJumpingState) &&
                !(currentState is GameSpace.States.MarioStates.SmallMarioRunningState) &&
                !(currentState is GameSpace.States.MarioStates.SmallMarioStandingState) &&
                !(currentState is GameSpace.States.MarioStates.SmallMarioWalkingState)) &&
                (currentState is GameSpace.States.MarioStates.BigMarioStandingState ||
                currentState is GameSpace.States.MarioStates.FireMarioStandingState))
            {//IF previously standing, then crouch
                game.GetMario.CrouchingTransition();
            }//IF previously Jumping, then stand and move
            else if (currentState is GameSpace.States.MarioStates.BigMarioJumpingState ||
                    currentState is GameSpace.States.MarioStates.FireMarioJumpingState)
            {
                game.GetMario.StandingTransition();
                EntityManager.FindItem((int)AvatarID.MARIO).Velocity = new Vector2((float)0, EntityManager.FindItem((int)AvatarID.MARIO).Velocity.Y + 40);
            }
            else if ((currentState is GameSpace.States.MarioStates.BigMarioCrouchingState ||
                    currentState is GameSpace.States.MarioStates.FireMarioCrouchingState))
            {
                EntityManager.FindItem((int)AvatarID.MARIO).Velocity = new Vector2((float)0, EntityManager.FindItem((int)AvatarID.MARIO).Velocity.Y + 40);
            }
            else
            {
                game.GetMario.StandingTransition();
                EntityManager.FindItem((int)AvatarID.MARIO).Velocity = new Vector2((float)0, EntityManager.FindItem((int)AvatarID.MARIO).Velocity.Y + 40);
            }

            Debug.WriteLine("UpCommand, facing {0}\n AState {1}\n", game.GetMario.Facing, game.GetMario.marioActionState);
        }

        public void Unexecute()
        {
            //EntityManager.MoveBlock(0, -1);
        }
    }
}