using GameSpace.Abstracts;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace GameSpace.Machines
{
    public class MusicHandler : AbstractHandler
    {
        private static readonly MusicHandler instance = new MusicHandler();
        private bool mute = false;
        private Song backgroundSong;

        public static MusicHandler GetInstance()
        {
            return instance;
        }

        private MusicHandler()
        {

        }

        public void LoadMusicIntoList(List<SoundEffect> loadedList)
        {
            musicList = loadedList;
        }

        public void LoadSong(Song song)
        {
            backgroundSong = song;
        }

        public void PlaySong()
        {
            MediaPlayer.Play(backgroundSong);
            MediaPlayer.IsRepeating = true;
            //MediaPlayer.Volume = MediaPlayer.Volume / 2;
        }

        public void MuteSong()
        {
            MediaPlayer.IsMuted = !MediaPlayer.IsMuted;
        }

        public void PlaySoundEffect(int soundEffect)
        {
            if (!mute)
            {
                musicList[soundEffect].CreateInstance().Play();
            }
        }
        public void MuteSoundEffects()
        {
            mute = !mute;
        }

    }
}
