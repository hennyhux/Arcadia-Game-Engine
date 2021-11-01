using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameSpace.Abstracts
{
    public abstract class AbstractMachine
    {
        internal static List<IGameObjects> gameEntityList = new List<IGameObjects>();
        internal static List<IGameObjects> prunedList = new List<IGameObjects>();
        internal static List<IGameObjects> copyPrunedList = new List<IGameObjects>();
        internal static List<IObjectAnimation> animationList = new List<IObjectAnimation>();
        internal static List<IGameObjects> listOfWarpPipes = new List<IGameObjects>();
        internal static IGameObjects mario;
        internal static Vector2 marioCurrentLocation;
    }
}
