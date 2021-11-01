using GameSpace.EntitiesManager;

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
