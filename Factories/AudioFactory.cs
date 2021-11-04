using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;


namespace GameSpace.Factories
{
    public class AudioFactory
    {
        #region mp3s
        private Song song;
        private SoundEffect smallJump;
        private SoundEffect superJump;
        private SoundEffect stomp;
        private SoundEffect death;
        private SoundEffect powerUpAppear;
        private SoundEffect powerUpCollect;
        private SoundEffect oneUpCollect;
        private SoundEffect coinCollect;

        #endregion
        private static readonly AudioFactory instance = new AudioFactory();
        public static AudioFactory GetInstance()
        {
            return instance;
        }

        private AudioFactory()
        {

        }

        public void LoadContent(ContentManager content)
        {
            song = content.Load<Song>("Audio/backgroundSong");
            smallJump = content.Load<SoundEffect>("Audio/smallJump");
            superJump = content.Load<SoundEffect>("Audio/superJump");
            stomp = content.Load<SoundEffect>("Audio/stomp");
            death = content.Load<SoundEffect>("Audio/death");
            powerUpAppear = content.Load<SoundEffect>("Audio/powerUpAppear");
            powerUpCollect = content.Load<SoundEffect>("Audio/powerUpCollect");
            oneUpCollect = content.Load<SoundEffect>("Audio/oneUpCollect");
            coinCollect = content.Load<SoundEffect>("Audio/coinCollect");
        }

        public Song CreateSong()
        {
            return song;
        }

        public List<SoundEffect> loadList()
        {
            List<SoundEffect> list = new List<SoundEffect>
            {
                smallJump,
                superJump,
                stomp,
                death,
                powerUpAppear,
                powerUpCollect, //5
                oneUpCollect,
                coinCollect
            };
            return list;
        }
    }
}
