using GameSpace.GameObjects;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.Sprites;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Factories
{
    public class MarioFactory
    {
        private static protected Game1 instanceGame;
        private Texture2D normalMarioSprite;
        private static MarioFactory instance;

        public static MarioFactory GetInstance(Game1 game)
        {
            if (instance == null)
            {
                instance = new MarioFactory();
                instanceGame = game;
            }

            return instance;

        }
        private MarioFactory()
        {

        }

        public void LoadContent(ContentManager content)
        {
            normalMarioSprite = content.Load<Texture2D>("AvatarSprite/OGMarioSheet");
        }

        public Mario ReturnMario()
        {
            //return new MarioStates(normalMarioSprite, 350, 200, 0, 2, 4);
            return new Mario(instanceGame);//mario.
            //return new Mario(game, normalMarioSprite);
            //return new MarioSprite(normalMarioSprite, 350, 200, 0, 2, 3);
        }
        public MarioSprite ReturnMarioStandingLeftSprite()
        {
            // return new MarioSprite(normalMarioSprite, 350, 200, 0, 2, 3);
            return new MarioSprite(normalMarioSprite, 350, 200, 0, 0, 0);
            //return new Mario(Game1);
        }
    }
}
