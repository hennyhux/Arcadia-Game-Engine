using GameSpace.Factories;
using GameSpace.GameObjects.EnemyObjects;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.States
{
    public class StateLakituDead : IEnemyState
    {
        public ISprite StateSprite { get; set; }
        public bool CollidedWithMario { get; set; }
        private readonly Lakitu Lakitu;


        public StateLakituDead(Lakitu lakitu)
        {
            lakitu.Sprite = SpriteEnemyFactory.GetInstance().CreateDeadLakituSprite();
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
