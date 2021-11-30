using GameSpace.Abstracts;
using GameSpace.EntityManaging;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using GameSpace.States;
using GameSpace.States.EnemyStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
            state = new StateLakituRight(this);
            Sprite = SpriteEnemyFactory.GetInstance().CreateLakituRightSprite();
            Velocity = new Vector2(1, 0);
            //ExpandedCollisionBox = new Rectangle((int)(Position.X + Sprite.Texture.Width / 32), (int)Position.Y, Sprite.Texture.Width, Sprite.Texture.Height * 3);
            //UpdateCollisionBox(Position);
        }
        public override void Draw(SpriteBatch spritebatch)
        {
            Sprite.Draw(spritebatch, Position);
            //state.StateSprite.Draw(spritebatch, Position);
            if (drawBox)
            {
                //Debug.Print("BOUND DRAWS CREATED()");
                DrawBoundaries(spritebatch, CollisionBox);
            }
        }
        public override void Update(GameTime gametime)
        {
            //state.Update(gametime);
            //UpdateSpeed();
            //UpdateCollisionBox(Position);
            //UpdatePosition(Position, gametime);
            //Velocity = new Vector2(-1, 0);
            if (!(state is StateLakituDead))
            {
                if (Velocity.X > 0 && !(state is StateLakituRight))
                {
                    state = new StateLakituRight(this);

                }
                else if (Velocity.X < 0 && !(state is StateLakituLeft))
                {
                    state = new StateLakituLeft(this);
                }
                if (state is StateLakituDead)
                {
                    CollisionBox = new Rectangle(0, 0, 0, 0);
                }
                else
                {
                    CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, 47, 69);
                }
                //StateSprite.Facing
                //state.StateSprite.Update(gametime);
                Sprite.Update(gametime);

                ExpandedCollisionBox = CollisionBox;
            }
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

        public void DrawBoundaries(SpriteBatch spritebatch, Rectangle destination)
        {
            Sprite.DrawBoundary(spritebatch, destination);
        }

        public override void Trigger()
        {
            Debug.Print("LAKITU KILLED");
            state = new StateLakituDead(this);
            //SpawnCloudObject();
            CollisionBox = new Rectangle(0, 0, 0, 0);
            ExpandedCollisionBox = CollisionBox;

        }

        #region Collision Handling
        public IEnemyState GetCurrentState()
        {
            return state;
        }

        public override void HandleCollision(IGameObjects entity)
        {
            switch (entity.ObjectID)
            {
                case (int)AvatarID.MARIO:
                    CollisionHandler.GetInstance().EnemyToMarioCollision(this);
                    break;
            }
        }

        #endregion
    }
}

