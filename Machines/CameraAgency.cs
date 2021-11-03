using GameSpace.Abstracts;
using GameSpace.Camera2D;

namespace GameSpace.EntityManaging
{
    public class CameraAgency : AbstractHandler
    {
        private static readonly CameraAgency instance = new CameraAgency();

        public static CameraAgency GetInstance()
        {
            return instance;
        }

        private CameraAgency()
        {

        }

        public void LoadCamera(Camera camera)
        {
            cameraCopy = camera;
        }
    }
}
