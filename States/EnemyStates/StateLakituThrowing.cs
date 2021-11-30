using GameSpace.Factories;
using GameSpace.GameObjects.EnemyObjects;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace GameSpace.States
{
    public class StateLakituThrowing : IEnemyState
    {
        public ISprite StateSprite { get; set; }
        public bool CollidedWithMario { get; set; }
        private  Lakitu Lakitu;

        private protected int maxOffset;
        private protected int currentOffset;
        private protected Vector2 initLocation;
        private protected int direction;// 1 for right, -1 for left
        public bool animationFinished;

        public StateLakituThrowing(Lakitu lakitu)
        {
            Debug.Print("LAKITU THROWING()");
            //StateSprite = SpriteEnemyFactory.GetInstance().CreateLakituSprite();
            CollidedWithMario = false;
            
            Lakitu = lakitu;
            Lakitu.state = this;
            Lakitu.Velocity = lakitu.Velocity;
            Lakitu.Position = new Vector2(Lakitu.Position.X, Lakitu.Position.Y + 21);
            lakitu.Sprite = SpriteEnemyFactory.GetInstance().CreateLakituThrowingSprite();
            initLocation = lakitu.Position;
            maxOffset = 100;
            currentOffset = 0;
            initLocation.X = lakitu.Position.X ;
            initLocation.Y = lakitu.Position.Y;
            animationFinished = false;
            direction = 1;
            if(lakitu.Velocity.X > 0)
            {
                direction = -1;
            }
            Lakitu.Velocity = Vector2.Zero;
        }

        public void Draw(SpriteBatch spritebatch, Vector2 location)
        {
            //StateSprite.Draw(spritebatch, location);
        }

        public void Update(GameTime gametime)
        {

            if (currentOffset < maxOffset / 2)
            {
                initLocation.X -= 1;
                Lakitu.Position = new Vector2(Lakitu.Position.X - 1 * direction, Lakitu.Position.Y);
                currentOffset += 1;
            }

            else if (currentOffset >= maxOffset / 2 && currentOffset < maxOffset)
            {
                initLocation.X += 1;
                Lakitu.Position = new Vector2(Lakitu.Position.X + 1 * direction, Lakitu.Position.Y);
                currentOffset += 1;
            }
            else
            {
                animationFinished = true;
                Lakitu.Position = new Vector2(Lakitu.Position.X, Lakitu.Position.Y - 21);
                Lakitu.state = new StateLakituLeft(Lakitu);

                Debug.WriteLine("animationFinished {0}", animationFinished);
            }
            
        }

        public void Trigger()
        {
            //Throw a Spinny
        }

        public void DrawBoundaries(SpriteBatch spritebatch, Rectangle destination)
        {
            //StateSprite.DrawBoundary(spritebatch, destination);
        }
    }
}
