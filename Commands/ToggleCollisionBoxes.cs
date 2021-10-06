using GameSpace.EntitiesManager;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Commands
{
    public class ToggleCollisionBoxes : ICommand
    {
        public ToggleCollisionBoxes()
        {

        }

        public void Execute()
        {
            EntityManager.ToggleCollisionBox();
        }

        public void Unexecute()
        {
            
        }
    }
}
