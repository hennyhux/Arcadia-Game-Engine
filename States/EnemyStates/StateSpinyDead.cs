using GameSpace.Factories;
using GameSpace.GameObjects.EnemyObjects;
using GameSpace.Interfaces;
using GameSpace.Machines;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace GameSpace.States
{
    public class StateSpinyDead : IEnemyState
    {
        public ISprite StateSprite { get; set; }
        public bool CollidedWithMario { get; set; }
        private readonly Spiny Spiny;


        public StateSpinyDead(Spiny spiny)
        {
            Debug.Print("DEAD SPINY CREATED()");
            HUDHandler.GetInstance().UpdateExp(5);
            MarioHandler.GetInstance().IncrementMarioPoints(100);
            CollidedWithMario = false;
            spiny.Sprite = SpriteEnemyFactory.GetInstance().CreateDeadSpinySprite();
        }

        public void Draw(SpriteBatch spritebatch, Vector2 location)
        {

        }

        public void Update(GameTime gametime)
        {

        }

        public void Trigger()
        {
        }

        public void DrawBoundaries(SpriteBatch spritebatch, Rectangle destination)
        {

        }
    }
}
