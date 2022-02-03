
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
        private Texture2D Lakitu;
        private Texture2D Spiny;
        private Texture2D DeadSpiny;
        private Texture2D UberGoomba;
        private Texture2D UberKoopa;
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
            Lakitu = content.Load<Texture2D>("Enemies/Lakitu2");
            Spiny = content.Load<Texture2D>("Enemies/Spiny2");
            UberGoomba = content.Load<Texture2D>("Enemies/UberGoombas.cs");
            UberKoopa = content.Load<Texture2D>("Enemies/UberKoopa");
        }

        public ISprite CreateGoombaSprite()
        {
            return new AnimatedSprite(Goomba, 1, 2, 2, 0, 0);
        }

        public ISprite CreateDeadGoombaSprite()
        {
            return new EnemyDeadSprite(DeadGoomba, 1, 1, 1);
        }

        public ISprite CreateRedKoopaSprite()
        {
            return new AnimatedSprite(RedKoopa, 1, 2, 2, 0, 0);
        }

        public ISprite CreateGreenKoopaLeftSprite()
        {
            return new AnimatedSprite(GreenKoopa, 1, 2, 2, 0, 0);
        }

        public ISprite CreateRedKoopaRightSprite()
        {
            return new AnimatedSprite(RedKoopaRight, 1, 2, 2, 0, 0);
        }

        public ISprite CreateGreenKoopaRightSprite()
        {
            return new AnimatedSprite(GreenKoopaRight, 1, 2, 2, 0, 0);
        }

        public ISprite CreateGreenKoopaShellSprite()
        {
            return new AnimatedSprite(GreenKoopaShelled, 1, 2, 1, 1, 0);
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

        public ISprite CreateUberKoopaSprite()
        {
            return new UberKoopaSprite(UberKoopa, 1, 2, 2, 0, 0);
        }

        public ISprite CreateUberKoopaRightSprite()
        {
            return new UberKoopaSpriteRight(UberKoopa, 1, 2, 2, 0, 0);
        }

        public ISprite CreateUberKoopaDeadSprite()
        {
            return new UberKoopaSpriteDead(UberKoopa, 1, 2, 1);
        }

        public ISprite CreateUberGoombaSprite()
        {
            return new UberGoombaSprite(Goomba, 1, 2, 2, 0, 0);
        }

        public ISprite CreateUberGoombaBerserkSprite()
        {
            return new UberGoombaSprite(UberGoomba, 1, 2, 2, 0, 0);
        }

        public ISprite CreateUberGoombaDeadSprite()
        {
            return new UberGoombaDeadSprite(DeadGoomba, 1, 1, 1);
        }

        public ISprite CreateLakituSprite()
        {
            return new LakituSprite(Lakitu, 1, 1, 0, 1, 0, true);
        }

        public ISprite CreateLakituRightSprite()
        {
            return new LakituSprite(Lakitu, 1, 1, 1, 1, 0, true);
        }

        public ISprite CreateDeadLakituSprite()
        {
            return new LakituSprite(Lakitu, 1, 1, 1, 1, 0, false);
        }

        public ISprite CreateLakituCloudSprite()
        {
            return new LakituSprite(Lakitu, 1, 1, 1, 1, 1, true);
        }

        public ISprite CreateLakituThrowingSprite()
        {
            return new LakituSprite(Lakitu, 1, 1, 1, 1, 1, true);
        }

        public ISprite CreateSpinySprite()
        {
            return new SpinySprite(Spiny, 1, 1, 0, 1, 0, true);
        }

        public ISprite CreateSpinyRightSprite()
        {
            return new SpinySprite(Spiny, 1, 1, 1, 1, 0, true);
        }

        public ISprite CreateDeadSpinySprite()
        {
            return new EnemyDeadSprite(Spiny, 1, 1, 1);
        }
    }
}
