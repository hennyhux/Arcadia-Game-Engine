using GameSpace.Abstracts;
using GameSpace.Camera2D;

namespace GameSpace.EntityManaging
{
    public class CameraMachine : AbstractMachine
    {
        private static readonly CameraMachine instance = new CameraMachine();

        public static CameraMachine GetInstance()
        {
            return instance;
        }

        private CameraMachine()
        {

        }

        public void LoadCamera(Camera camera)
        {
            cameraCopy = camera;
        }
    }
}
