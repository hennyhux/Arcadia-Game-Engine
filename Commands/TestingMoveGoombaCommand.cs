using GameSpace.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Commands
{
    public class TestingMoveGoombaCommand : ICommand
    {
        private int direction;
        public TestingMoveGoombaCommand(int direction)
        {
            this.direction = direction;
        }

        public void Execute()
        {
            EntitiesManager.EntityManager.MoveItem((int)EnemyID.GOOMBA, (int)direction);
        }

        public void Unexecute()
        {
            throw new NotImplementedException();
        }
    }
}
