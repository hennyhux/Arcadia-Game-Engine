using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using GameSpace.GameObjects.EnemyObjects;


namespace GameSpace.States
{
    public class StateGreenKoopaDead : IEnemyState
    {
        public ISprite StateSprite { get; set; }
        public Boolean CollidedWithMario { get; set; }
        private int countDown;
        private GreenKoopa GreenKoopa;



        public StateGreenKoopaDead(GreenKoopa greenKoopa)
        {
            StateSprite = SpriteEnemyFactory.GetInstance().CreateGreenKoopaShellSprite();
            CollidedWithMario = true;
            this.GreenKoopa = greenKoopa;
            this.GreenKoopa.state = this;
            this.GreenKoopa.Velocity = new Vector2(0, 0);
        }

        public void Draw(SpriteBatch spritebatch, Vector2 location)
        {
            StateSprite.Draw(spritebatch, location);
            if (CollidedWithMario) this.countDown++;
        }

        public void Update(GameTime gametime)
        {
            StateSprite.Update(gametime);
            if (countDown == 225) StateSprite = SpriteEnemyFactory.GetInstance().CreateGreenKoopaShellAndLegsSprite();

            if (countDown == 550)
            {
                if (this.GreenKoopa.direction == 0)
                {
                    this.GreenKoopa.state = new StateGreenKoopaAliveLeft(this.GreenKoopa);
                }
                else
                {
                    this.GreenKoopa.state = new StateGreenKoopaAliveRight(this.GreenKoopa);
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
