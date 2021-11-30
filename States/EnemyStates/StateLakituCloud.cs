using GameSpace.Factories;
using GameSpace.GameObjects.EnemyObjects;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameSpace.Machines;
using System.Diagnostics;
using GameSpace.Sprites;
namespace GameSpace.States
{
    public class StateLakituCloud : IEnemyState
    {
        public ISprite StateSprite { get; set; }
        public bool CollidedWithMario { get; set; }
        private readonly Lakitu Lakitu;
        public bool marioInCloud { get; set; }


        public StateLakituCloud(Lakitu lakitu)
        {
            //Debug.WriteLine("CREATE CLOUD SPRITE: ,\n");
            //lakitu.Sprite = SpriteEnemyFactory.GetInstance().CreateDeadLakituSprite();
            Lakitu = lakitu;
            Lakitu.state = this;
            //Lakitu.Velocity = lakitu.Velocity;
            Lakitu.Velocity = Vector2.Zero;
            marioInCloud = false;
            //Lakitu.Velocity = new Vector2(150, Lakitu.Velocity.Y);
            Lakitu.Sprite = SpriteEnemyFactory.GetInstance().CreateLakituCloudSprite();
        }

        public void Draw(SpriteBatch spritebatch, Vector2 location)
        {

        }

        public void Update(GameTime gametime)
        {
            if (marioInCloud == true)
            {
                Lakitu.Position = MarioHandler.GetInstance().GetPosition();
                //Lakitu.Position = new Vector2(Lakitu.Position )
            }
        }

        public void Trigger()
        {
            if(marioInCloud == false)
            {
                Debug.Print("ENTER CLOUD");
                marioInCloud = true;
                MarioHandler.GetInstance().EnterCloudMario();
                ((LakituSprite)Lakitu.Sprite).TurnInVisible();
                Lakitu.CollisionBox = new Rectangle(0, 0, 0, 0);
                Lakitu.ExpandedCollisionBox = Lakitu.CollisionBox;
                Lakitu.state = new StateLakituDead(Lakitu);
            }
            else if (marioInCloud == true && MarioHandler.GetInstance().getCloudMario() == false)
            {
                Lakitu.state = new StateLakituDead(Lakitu);
            }
        }

        public void DrawBoundaries(SpriteBatch spritebatch, Rectangle destination)
        {

        }
    }
}
