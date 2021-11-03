﻿using System.Diagnostics;

namespace GameSpace.Commands
{
    public class StateFireMarioCommand : ICommand
    {
        //private IGameObjects reciever;
        private protected GameRoot game;
        public static int temp = 0;

        public StateFireMarioCommand(GameRoot game)
        {
            this.game = game;
        }

        public void Execute()
        {
            //EntityManager.MoveBlock(0, 1);
            game.GetMario.FireMarioTransformation();
            Debug.WriteLine("BigMarioTransformation, powerUp {0}\n AState {1}\n", game.GetMario.marioPowerUpState, game.GetMario.marioActionState);
        }

        public void Unexecute()
        {
            //EntityManager.MoveBlock(0, -1);
        }
    }
}
