using GameSpace.EntityManaging;

namespace GameSpace.Commands
{
    public class ToggleCollisionBoxes : ICommand
    {
        public ToggleCollisionBoxes()
        {

        }

        public void Execute()
        {
            TheaterMachine.GetInstance().ToggleCollisionBox();
        }

        public void Unexecute()
        {

        }
    }
}
