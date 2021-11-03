using GameSpace.Abstracts;
using GameSpace.Interfaces;

namespace GameSpace.EntityManaging
{
    public class AnimationAgency : AbstractHandler
    {
        private static readonly AnimationAgency instance = new AnimationAgency();
        public static AnimationAgency GetInstance()
        {
            return instance;
        }

        private AnimationAgency()
        {

        }

        public void AddAnimation(IObjectAnimation animation)
        {
            animationList.Add(animation);
        }

    }
}
