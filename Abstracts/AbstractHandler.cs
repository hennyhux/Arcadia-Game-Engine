using GameSpace.Camera2D;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace GameSpace.Abstracts
{
    public abstract class AbstractHandler
    {
        internal protected static List<IGameObjects> gameEntityList = new List<IGameObjects>();
        internal protected static List<IGameObjects> prunedList = new List<IGameObjects>();
        internal protected static List<IGameObjects> copyPrunedList = new List<IGameObjects>();
        internal protected static List<IObjectAnimation> animationList = new List<IObjectAnimation>();
        internal protected static List<IGameObjects> listOfWarpPipes = new List<IGameObjects>();
        internal protected static List<SoundEffect> musicList = new List<SoundEffect>();
        internal protected static Mario mario;
        internal protected static Vector2 marioCurrentLocation;
        internal protected static Camera cameraCopy;
    }
}
