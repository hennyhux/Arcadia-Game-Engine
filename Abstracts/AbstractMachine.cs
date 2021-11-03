using GameSpace.Camera2D;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        internal static List<SoundEffect> musicList = new List<SoundEffect>();
        internal static Mario mario;
        internal static Vector2 marioCurrentLocation;
        internal static Camera cameraCopy;
    }
}
