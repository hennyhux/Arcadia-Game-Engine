using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using GameSpace.GameObjects.EnemyObjects;

namespace GameSpace.States.EnemyStates
{
    public class StateGreenKoopaDeadLeft : IEnemyState
    {
        public ISprite StateSprite { get; set; }
        public Boolean CollidedWithMario { get; set; }
        private GreenKoopa GreenKoopa;

        public StateGreenKoopaDeadLeft(GreenKoopa greenKoopa)
        {
            StateSprite = SpriteEnemyFactory.GetInstance().CreateGreenKoopaShellSprite();
            CollidedWithMario = false;
            this.GreenKoopa = greenKoopa;
            this.GreenKoopa.state = this;
            this.GreenKoopa.Velocity = new Vector2((float)-1, (float)0);

        }

        public void Draw(SpriteBatch spritebatch, Vector2 location)
        {
            StateSprite.Draw(spritebatch, location);
        }

        public void Update(GameTime gametime)
        {
            StateSprite.Update(gametime);
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
