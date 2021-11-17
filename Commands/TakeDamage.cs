
using GameSpace.Machines;
using System;
namespace GameSpace
{
    public class TakeDamage : ICommand
    {
        private protected GameRoot game;
        public TakeDamage(GameRoot game)
        {
            this.game = game;
        }

        public void Execute()
        {
            MarioHandler.GetInstance().DecrementMarioLives();
        }

        public void Unexecute()
        {
            throw new NotImplementedException();
        }
    }


}
