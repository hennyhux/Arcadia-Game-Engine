using GameSpace.Interfaces;

namespace GameSpace.EntityManaging
{
    public class FinderMachine
    {
        private static readonly FinderMachine instance = new FinderMachine();
        public static FinderMachine GetInstance()
        {
            return instance;
        }

        private FinderMachine()
        {

        }

        public IGameObjects FindItem(int ItemID)
        {
            foreach (IGameObjects entity in TheaterMachine.GetInstance().)
            {
                if (entity.ObjectID == ItemID)
                {
                    return entity;
                }
            }
            return null; //lets try not to return null
        }
    }
}
