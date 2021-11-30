using GameSpace.Abstracts;
using GameSpace.EntityManaging;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using GameSpace.States;
using GameSpace.States.EnemyStates;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace GameSpace.GameObjects.EnemyObjects
{
    public class Lakitu : AbstractEnemy
    {

        public Lakitu(Vector2 initalPosition)
        {
            Debug.Print("LAKITU CREATED()");
            ObjectID = (int)EnemyID.LAKITU;
            Direction = (int)MarioDirection.LEFT;
            drawBox = false;
            Position = initalPosition;
            state = new StateLakituAlive(this);
            Sprite = SpriteEnemyFactory.GetInstance().CreateLakituSprite();
            //ExpandedCollisionBox = new Rectangle((int)(Position.X + Sprite.Texture.Width / 32), (int)Position.Y, Sprite.Texture.Width, Sprite.Texture.Height * 3);
            //UpdateCollisionBox(Position);
        }
        public override void Update(GameTime gametime)
        {
            //state.Update(gametime);
            //UpdateSpeed();
            //UpdateCollisionBox(Position);
            //UpdatePosition(Position, gametime);
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, 47, 69);
            //CollisionBox = new Rectangle((int)(Position.X), (int)Position.Y, Sprite.Texture.Width, Sprite.Texture.Height);
            ExpandedCollisionBox = CollisionBox;
        }
        public override void UpdatePosition(Vector2 location, GameTime gameTime) //use velocity
        {
            //ExpandedCollisionBox = new Rectangle(CollisionBox.X, CollisionBox.Y, CollisionBox.Width, CollisionBox.Height);
            Acceleration = new Vector2(0, 0);
            if (Direction == (int)MarioDirection.RIGHT)// && !(state is StateRedKoopaDead))
            {
                //Velocity = new Vector2(85, 0);
            }

            if (Direction == (int)MarioDirection.LEFT)// && !(state is StateRedKoopaDead))
            {
                //Velocity = new Vector2(-85, 0);
            }
            

            Velocity += Acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            //UpdateCollisionBox(Position);
        }

        public override void Trigger()
        {
            //state = new StateRedKoopaDead(this);
            //PreformShellOffset();
        }

        #region Collision Handling
        public IEnemyState GetCurrentState()
        {
            return state;
        }

        #endregion
    }
}

