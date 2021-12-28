using GameSpace.Abstracts;
using GameSpace.Interfaces;

namespace GameSpace.EntityManaging
{
    public class AnimationHandler : Handler
    {
        private static readonly AnimationHandler instance = new AnimationHandler();
        public static AnimationHandler GetInstance()
        {
            return instance;
        }

        private AnimationHandler()
        {

        }



        public void AddAnimation(IObjectAnimation animation)
        {
            animationList.Add(animation);
        }

        public void AddMarioWarpAnimation()
        {

        }

    }

    public class AnimationFactory
    {
        public AnimationFactory()
        {

        }



    }
}
