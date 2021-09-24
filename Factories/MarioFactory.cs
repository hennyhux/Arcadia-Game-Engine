using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Factories
{
    public class MarioFactory
    {
        private Texture2D normalMarioSprite;

        private static BlockSpriteFactory instance = new BlockSpriteFactory();
        public static BlockSpriteFactory Instance => instance;

        public MarioFactory()
        {

        }

        public void LoadContent(ContentManager content)
        {
            normalMarioSprite = content.Load<Texture2D>("AvatarSprite/MarioStandingLeft");
        }

        public ISprite ReturnMarioStandingLeftSprite()
        {
            return new StaticSprite(normalMarioSprite, 1, 1, 1, new Microsoft.Xna.Framework.Vector2(350,200));
        }

    }
}
