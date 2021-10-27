using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using GameSpace.GameObjects.EnemyObjects;


namespace GameSpace.States
{
    public class StateRedKoopaDead: IEnemyState
    {
        public ISprite StateSprite { get; set; }
        public Boolean CollidedWithMario { get; set; }
        private int countDown;
        private RedKoopa RedKoopa;



        public StateRedKoopaDead(RedKoopa redKoopa)
        {
            StateSprite = SpriteEnemyFactory.GetInstance().CreateRedKoopaShellSprite();
            CollidedWithMario = true;
            this.RedKoopa = redKoopa;
            this.RedKoopa.state = this;
            this.RedKoopa.Velocity = new Vector2(0, 0);
        }

        public void Draw(SpriteBatch spritebatch, Vector2 location)
        {
            Vector2 copy = new Vector2(location.X, location.Y + 20);
            StateSprite.Draw(spritebatch, copy);
            if (CollidedWithMario) this.countDown++;
        }

        public void Update(GameTime gametime)
        {
            StateSprite.Update(gametime);
            if (countDown == 225) StateSprite = SpriteEnemyFactory.GetInstance().CreateRedKoopaShellAndLegsSprite();

            if (countDown == 550)
            {
                if (this.RedKoopa.direction == 0)
                {
                    this.RedKoopa.state = new StateRedKoopaAliveLeft(this.RedKoopa);
                }
                else
                {
                    this.RedKoopa.state = new StateRedKoopaAliveRight(this.RedKoopa);
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
