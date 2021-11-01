using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;

namespace GameSpace.Factories
{
    public class AudioFactory
    {
        #region mp3s
        private Song song;
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
        }
      
        public Song CreateSong()
        {
            return this.song;
        }


    }
}
