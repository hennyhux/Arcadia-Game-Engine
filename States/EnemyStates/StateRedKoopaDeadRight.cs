using GameSpace.Factories;
using GameSpace.GameObjects.EnemyObjects;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.States.EnemyStates
{
    public class StateRedKoopaDeadRight : IEnemyState
    {
        public ISprite StateSprite { get; set; }
        public bool CollidedWithMario { get; set; }
        private readonly RedKoopa RedKoopa;

        public StateRedKoopaDeadRight(RedKoopa redKoopa)
        {
            StateSprite = SpriteEnemyFactory.GetInstance().CreateRedKoopaShellSprite();
            CollidedWithMario = false;
            RedKoopa = redKoopa;
            RedKoopa.state = this;
            RedKoopa.Velocity = new Vector2(+1, 0);
            RedKoopa.Direction = 1;

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
