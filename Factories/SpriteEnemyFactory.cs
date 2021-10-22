
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;
using GameSpace.Sprites;
using GameSpace.Sprites.Enemy;

namespace GameSpace.Factories
{
    public class SpriteEnemyFactory
    {

        #region sprites
        private Texture2D Goomba;
        private Texture2D DeadGoomba;
        private Texture2D GreenKoopa;
        private Texture2D RedKoopa;
        private Texture2D GreenKoopaShelled;
        #endregion

        private static SpriteEnemyFactory instance;
        public static SpriteEnemyFactory GetInstance()
        {
            if (instance == null)
            {
                instance = new SpriteEnemyFactory();
            }

            return instance;
        }

        private SpriteEnemyFactory()
        {

        }

        public void LoadContent(ContentManager content)
        {
            Goomba = content.Load<Texture2D>("Enemies/Goombas");
            DeadGoomba = content.Load<Texture2D>("Enemies/GoombasDead");
            RedKoopa = content.Load<Texture2D>("Enemies/RedKoopas");
            GreenKoopa = content.Load<Texture2D>("Enemies/GreenKoopas");
            GreenKoopaShelled = content.Load<Texture2D>("Enemies/GreenKoopaShelled");

        }

        public ISprite CreateGoombaSprite()
        {
            return new GoombaSprite(Goomba, 1, 2, 2, 0, 0);
        }

        public ISprite CreateDeadGoombaSprite()
        {
            return new GoombasDeadSprite(DeadGoomba, 1, 1, 1);
        }

        public ISprite CreateRedKoopaSprite()
        {
            return new RedKoopaSprite(RedKoopa, 1, 2, 2, 0, 0); // RED KOOPA IS THE SAME AS GREEN KOOPA CAN USE THE SAME CLASS 
        }

        public ISprite CreateGreenKoopaSprite()
        {
            return new GreenKoopaSprite(GreenKoopa, 1, 2, 2, 0, 0);
        }

        public ISprite CreateGreenKoopaShellSprite()
        {
            return new GoombasDeadSprite(GreenKoopaShelled, 1, 1, 1);
        }

        public ISprite CreateShelledGreenKoopaSprite()
        {
            return new GreenKoopaSprite(GreenKoopaShelled, 1, 2, 1, 0, 0);
        }

        public ISprite CreateShelledWithLegsGreenKoopaSprite()
        {
            return new GreenKoopaSprite(GreenKoopaShelled, 1, 2, 2, 0, 0);
        }
    }
}
