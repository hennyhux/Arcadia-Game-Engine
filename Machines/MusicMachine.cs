using GameSpace.Abstracts;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using GameSpace.EntityManaging;
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

        public void PlaySong(Song backgroundSong)
        {
            MediaPlayer.Play(backgroundSong);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = MediaPlayer.Volume / 2;
        }

        public void PlaySoundEffect(int soundEffect)
        {
            musicList[soundEffect].CreateInstance().Play();
        }

    }
}
