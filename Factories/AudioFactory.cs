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
            song = content.Load<Song>("Audio/backgroundSong");
            smallJump = content.Load<SoundEffect>("Audio/smallJump");
            superJump = content.Load<SoundEffect>("Audio/superJump");
        }

        public Song CreateSong()
        {
            return song;
        }

        public List<SoundEffect> loadList()
        {
            list = new List<SoundEffect>
            {
                smallJump,
                superJump
            };
            return list;
        }
    }
}
