using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;


namespace GameSpace.Factories
{
    public class AudioFactory
    {
        #region sounds
        private Song song;
        private SoundEffect smallJump;
        private SoundEffect superJump;
        private SoundEffect stomp;
        private SoundEffect death;
        private SoundEffect powerUpAppear;
        private SoundEffect powerUpCollect;
        private SoundEffect oneUpCollect;
        private SoundEffect coinCollect;
        private SoundEffect bump;
        private SoundEffect breakBlock;
        private SoundEffect pipeWarp;
        private SoundEffect warning;
        private SoundEffect gameover;
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
            oneUpCollect = content.Load<SoundEffect>("Audio/oneUpCollect"); //haven't tested
            coinCollect = content.Load<SoundEffect>("Audio/coinCollect");
            bump = content.Load<SoundEffect>("Audio/bump");
            breakBlock = content.Load<SoundEffect>("Audio/breakBlock");
            pipeWarp = content.Load<SoundEffect>("Audio/pipeWarp");
            warning = content.Load<SoundEffect>("Audio/timeWarning");
            gameover = content.Load<SoundEffect>("Audio/pipeWarp");
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
                coinCollect,
                bump,
                breakBlock,
                pipeWarp,      //10
                warning,
                gameover
            };
            return list;
        }
    }
}
