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
        #endregion
        private static readonly AudioFactory instance = new AudioFactory();
        private List<SoundEffect> list;
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
        }
      
        public Song CreateSong()
        {
            return this.song;
        }

        public List<SoundEffect> loadList()
        {
            this.list = new List<SoundEffect>();
            this.list.Add(this.smallJump);
            this.list.Add(this.superJump);
            return this.list;
        }
    }
}
