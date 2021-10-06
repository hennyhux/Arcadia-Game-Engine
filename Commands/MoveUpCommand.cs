using GameSpace.EntitiesManager;
using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Commands
{
    public class MoveUpCommand : ICommand
    {
        private IGameObjects reciever;

        public MoveUpCommand(IGameObjects block)
        {
            this.reciever = block;
        }

        public void Execute()
        {
            EntityManager.MoveBlock(0, 1);
        }

        public void Unexecute()
        {
            EntityManager.MoveBlock(0, -1);
        }
    }
}
