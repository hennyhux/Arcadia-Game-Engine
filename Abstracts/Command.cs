using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Abstracts
{
    public abstract class RootCommand : ICommand
    {
        protected GameRoot reciever;

        protected RootCommand(GameRoot reciever)
        {
            this.reciever = reciever;
        }

        public abstract void Execute();
        public abstract void Unexecute();
    }

    public abstract class TheaterCommand : ICommand
    {
        protected GameRoot reciever;

        protected TheaterCommand(GameRoot reciever)
        {
            this.reciever = reciever;
        }

        public abstract void Execute();
        public abstract void Unexecute();
    }

}
