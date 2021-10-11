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
            //EntityManager.MoveBlock(0, 1);
            //How to change mario's position
            //game.GetMario.Position = new Vector2(game.GetMario.Position.X, game.GetMario.Position.Y + 10);
            EntitiesManager.EntityManager.MoveItem((int)AvatarID.MARIO, (int)ControlDirection.DOWN);

            //game.GetMario.CrouchingTransition();//Use these seperatly, alongside the same one from the MoveDown class
            game.GetMario.StandingTransition();// 1 pair alloys jumping other crouching
            

            Debug.WriteLine("UpCommand, facing {0}\n AState {1}\n", game.GetMario.Facing, game.GetMario.marioActionState);
        }

        public void Unexecute()
        {
            //EntityManager.MoveBlock(0, -1);
        }
    }
}
