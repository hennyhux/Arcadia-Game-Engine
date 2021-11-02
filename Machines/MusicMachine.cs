using GameSpace.Abstracts;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Machines
{
    public class MusicMachine : AbstractMachine
    {
        private static MusicMachine instance = new MusicMachine();
        public static MusicMachine GetInstance()
        {
            return instance;
        }

        private MusicMachine()
        {

        }

        public void LoadMusicIntoList(List<SoundEffect> loadedList)
        {
            musicList = loadedList;
        }

    }
}
