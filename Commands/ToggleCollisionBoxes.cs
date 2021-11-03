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
            TheaterHandler.GetInstance().ToggleCollisionBox();
        }

        public void Unexecute()
        {

        }
    }
}
