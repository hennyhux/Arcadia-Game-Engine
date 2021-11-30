using GameSpace.Factories;
using GameSpace.GameObjects.EnemyObjects;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
namespace GameSpace.States
{
    public class StateSpinyRight : IEnemyState
    {
        public ISprite StateSprite { get; set; }
        public bool CollidedWithMario { get; set; }
        private Spiny Spiny;


        public StateSpinyRight(Spiny spiny)
        {
            //Debug.WriteLine("CREATE RIGHT SPRITE: ," );
            //StateSprite = SpriteEnemyFactory.GetInstance().CreateSpinyRightSprite();
            CollidedWithMario = false;
            Spiny = spiny;
            Spiny.state = this;
            Spiny.Velocity = Spiny.Velocity;
            Spiny.Velocity = new Vector2(150, Spiny.Velocity.Y);
            Spiny.Sprite = SpriteEnemyFactory.GetInstance().CreateSpinyRightSprite();

        }

        public void Draw(SpriteBatch spritebatch, Vector2 location)
        {
            //StateSprite.Draw(spritebatch, location);
        }

        public void Update(GameTime gametime)
        {
            //StateSprite.Update(gametime);
        }

        public void Trigger()
        {
        }

        public void DrawBoundaries(SpriteBatch spritebatch, Rectangle destination)
        {
            //StateSprite.DrawBoundary(spritebatch, destination);
        }
    }
}
