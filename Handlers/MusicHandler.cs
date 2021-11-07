using GameSpace.Abstracts;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace GameSpace.Machines
{
    public class MusicHandler : AbstractHandler
    {
        private static readonly MusicHandler instance = new MusicHandler();
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
