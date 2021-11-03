using GameSpace.Factories;
using GameSpace.GameObjects.EnemyObjects;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameSpace.Machines;

namespace GameSpace.States.EnemyStates
{
    public class StateGreenKoopaDead : IEnemyState
    {
        public ISprite StateSprite { get; set; }
        public bool CollidedWithMario { get; set; }
        private int countDown;
        private readonly GreenKoopa GreenKoopa;



        public StateGreenKoopaDead(GreenKoopa greenKoopa)
        {
            StateSprite = SpriteEnemyFactory.GetInstance().CreateGreenKoopaShellSprite();
            CollidedWithMario = true;
            GreenKoopa = greenKoopa;
            GreenKoopa.state = this;
            GreenKoopa.Velocity = new Vector2(0, 0);
            MusicHandler.GetInstance().PlaySoundEffect(2);
        }

        public void Draw(SpriteBatch spritebatch, Vector2 location)
        {
            StateSprite.Draw(spritebatch, location);
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
                StateSprite = SpriteEnemyFactory.GetInstance().CreateGreenKoopaShellAndLegsSprite();
            }

            if (countDown == 550)
            {
                if (GreenKoopa.Direction == 0)
                {
                    GreenKoopa.state = new StateGreenKoopaAliveLeft(GreenKoopa);
                }
                else
                {
                    GreenKoopa.state = new StateGreenKoopaAliveRight(GreenKoopa);
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
