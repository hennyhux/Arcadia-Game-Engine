
using GameSpace.Sprites;
using GameSpace.Sprites.Enemy;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.Factories
{
    public class SpriteEnemyFactory
    {

        #region sprites
        private Texture2D Goomba;
        private Texture2D DeadGoomba;
        private Texture2D GreenKoopa;
        private Texture2D GreenKoopaRight;
        private Texture2D GreenKoopaShelled;
        private Texture2D RedKoopa;
        private Texture2D RedKoopaRight;
        private Texture2D RedKoopaShelled;
        private Texture2D Plant;
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
            GreenKoopaRight = content.Load<Texture2D>("Enemies/GreenKoopaRight");
            GreenKoopaShelled = content.Load<Texture2D>("Enemies/GreenKoopaShelled");
            RedKoopaShelled = content.Load<Texture2D>("Enemies/RedKoopaShelled");
            RedKoopaRight = content.Load<Texture2D>("Enemies/RedKoopaRight");
            Plant = content.Load<Texture2D>("Enemies/Plant");

        }

        public ISprite CreateGoombaSprite()
        {
            return new GoombaSprite(Goomba, 1, 2, 2, 0, 0);
        }

        public ISprite CreateDeadGoombaSprite()
        {
            return new EnemyDeadSprite(DeadGoomba, 1, 1, 1);
        }

        public ISprite CreateRedKoopaSprite()
        {
            return new KoopaSprite(RedKoopa, 1, 2, 2, 0, 0);
        }

        public ISprite CreateGreenKoopaLeftSprite()
        {
            return new KoopaSprite(GreenKoopa, 1, 2, 2, 0, 0);
        }

        public ISprite CreateRedKoopaRightSprite()
        {
            return new KoopaSprite(RedKoopaRight, 1, 2, 2, 0, 0);
        }

        public ISprite CreateGreenKoopaRightSprite()
        {
            return new KoopaSprite(GreenKoopaRight, 1, 2, 2, 0, 0);
        }

        public ISprite CreateGreenKoopaShellSprite()
        {
            return new KoopaSprite(GreenKoopaShelled, 1, 2, 1, 1, 0);
        }
        public ISprite CreateRedKoopaShellSprite()
        {
            return new KoopaSprite(RedKoopaShelled, 1, 2, 1, 1, 0);
        }

        public ISprite CreateGreenKoopaShellAndLegsSprite()
        {
            return new KoopaSprite(GreenKoopaShelled, 1, 2, 2, 0, 0);
        }

        public ISprite CreateRedKoopaShellAndLegsSprite()
        {
            return new KoopaSprite(RedKoopaShelled, 1, 2, 2, 0, 0);
        }

        public ISprite CreatePlantSprite()
        {
            return new KoopaSprite(Plant, 1, 2, 2, 0, 0);
        }
    }
}
