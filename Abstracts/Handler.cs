using GameSpace.Camera2D;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace GameSpace.Abstracts
{
    public abstract class Handler
    {
        private protected static List<IGameObjects> gameEntityList = new List<IGameObjects>();
        private protected static List<IGameObjects> prunedList = new List<IGameObjects>();
        private protected static List<IGameObjects> copyPrunedList = new List<IGameObjects>();
        private protected static List<IObjectAnimation> animationList = new List<IObjectAnimation>();
        private protected static List<IGameObjects> listOfWarpPipes = new List<IGameObjects>();
        private protected static List<IGameObjects> listOfWarpRoomPipes = new List<IGameObjects>();
        private protected static List<SoundEffect> musicList = new List<SoundEffect>();
        private protected static Dictionary<int, SoundEffect> soundEffectsList = new Dictionary<int, SoundEffect>();
        private protected static GameTime internalGametime = new GameTime();
        private protected static GameRoot gameRoot = new GameRoot();
        private protected static Mario mario;
        private protected static GameRoot game;
        private protected static Vector2 marioCurrentLocation;
        private protected static Camera cameraCopy;
        private protected static int currentWarpLocation = 0;
        private protected static int marioScores = 0;
        public static long timer;
        private protected static int damageTakenScale = 32;
        private protected static float experinceScale = 1f;
        private protected static int currentMarioLevel = 0; //mario max level is 3
    }
}
