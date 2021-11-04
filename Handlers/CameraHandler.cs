using GameSpace.Abstracts;
using GameSpace.Camera2D;

namespace GameSpace.EntityManaging
{
    public class CameraHandler : AbstractHandler
    {
        private static readonly CameraHandler instance = new CameraHandler();

        public static CameraHandler GetInstance()
        {
            return instance;
        }

        private CameraHandler()
        {

        }

        public void LoadCamera(Camera camera)
        {
            cameraCopy = camera;
        }
    }
}
