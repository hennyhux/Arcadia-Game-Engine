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
            Lakitu = lakitu;
            Lakitu.state = this;
            Lakitu.Velocity = lakitu.Velocity;
            //Lakitu.Velocity = new Vector2(150, Lakitu.Velocity.Y);
            //lakitu.Sprite = SpriteEnemyFactory.GetInstance().CreateLakituRightSprite();
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
