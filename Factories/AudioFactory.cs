using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
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
            this.song = content.Load<Song>("Audio/backgroundSong");
            this.smallJump = content.Load<SoundEffect>("Audio/smallJump");
            this.superJump = content.Load<SoundEffect>("Audio/superJump");
            this.stomp = content.Load<SoundEffect>("Audio/stomp");
            this.death = content.Load<SoundEffect>("Audio/death");
            this.powerUpAppear = content.Load<SoundEffect>("Audio/powerUpAppear");
            this.powerUpCollect = content.Load<SoundEffect>("Audio/powerUpCollect");
        }
      
        public Song CreateSong()
        {
            return this.song;
        }

        public List<SoundEffect> loadList()
        {
            List<SoundEffect> list = new List<SoundEffect>();
            list.Add(this.smallJump);
            list.Add(this.superJump);
            list.Add(this.stomp);
            list.Add(this.death);
            list.Add(this.powerUpAppear);
            list.Add(this.powerUpCollect);
            return list;
        }
    }
}
