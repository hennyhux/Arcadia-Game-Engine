using GameSpace.Factories;
using GameSpace.GameObjects.EnemyObjects;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameSpace.Machines;


namespace GameSpace.States.EnemyStates
{
    public class StateRedKoopaDead : IEnemyState
    {
        public ISprite StateSprite { get; set; }
        public bool CollidedWithMario { get; set; }
        private int countDown;
        private readonly RedKoopa RedKoopa;



        public StateRedKoopaDead(RedKoopa redKoopa)
        {
            StateSprite = SpriteEnemyFactory.GetInstance().CreateRedKoopaShellSprite();
            CollidedWithMario = true;
            RedKoopa = redKoopa;
            RedKoopa.state = this;
            RedKoopa.Velocity = new Vector2(0, 0);
            MusicHandler.GetInstance().PlaySoundEffect(2);
        }

        public void Draw(SpriteBatch spritebatch, Vector2 location)
        {
            Vector2 copy = new Vector2(location.X, location.Y + 20);
            StateSprite.Draw(spritebatch, copy);
            if (CollidedWithMario)
            {
                countDown++;
            }
        }

        public void Update(GameTime gametime)
        {
            StateSprite.Update(gametime);
            if (countDown == 225)
            {
                StateSprite = SpriteEnemyFactory.GetInstance().CreateRedKoopaShellAndLegsSprite();
            }

            if (countDown == 550)
            {
                if (RedKoopa.direction == 0)
                {
                    RedKoopa.state = new StateRedKoopaAliveLeft(RedKoopa);
                }
                else
                {
                    RedKoopa.state = new StateRedKoopaAliveRight(RedKoopa);
                }
                countDown = 0;
            }

        }

        public void Trigger()
        {

        }

        public void DrawBoundaries(SpriteBatch spritebatch, Rectangle destination)
        {
            StateSprite.DrawBoundary(spritebatch, destination);
        }

    }
}
