using GameSpace.Abstracts;
using GameSpace.Interfaces;

namespace GameSpace.EntityManaging
{
    public class AnimationMachine : AbstractMachine
    {
        private static readonly AnimationMachine instance = new AnimationMachine();
        public static AnimationMachine GetInstance()
        {
            return instance;
        }

        private AnimationMachine()
        {

        }

        public void AddAnimation(IObjectAnimation animation)
        {
            animationList.Add(animation);
        }

    }
}
