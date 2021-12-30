using GameSpace.Abstracts;
using GameSpace.EntityManaging;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using GameSpace.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
namespace GameSpace.GameObjects.EnemyObjects
{
    public class Spiny : AbstractEnemy
    {
        private readonly int searchState;//0 passed mario, now throw spiny, 1 going left towards mario, 2 going right towards mario
        public Spiny(Vector2 initalPosition)
        {
            Debug.Print("Spiny CREATED()");
            ObjectID = (int)EnemyID.SPINY;
            searchState = 1;
            Direction = (int)MarioDirection.LEFT;
            drawBox = false;
            Position = initalPosition;
            state = new StateSpinyLeft(this);
            Sprite = SpriteEnemyFactory.GetInstance().CreateSpinySprite();
            //UpdateCollisionBox(Position);
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

            state.Update(gametime);
            if (!(state is StateSpinyDead))
            {
                if (Velocity.X > 0 && !(state is StateSpinyRight))
                {
                    state = new StateSpinyRight(this);
                    Direction = (int)MarioDirection.RIGHT;

                }
                else if (Velocity.X < 0 && !(state is StateSpinyLeft))
                {
                    state = new StateSpinyLeft(this);
                    Direction = (int)MarioDirection.LEFT;
                }
                if (state is StateSpinyDead)
                {
                    CollisionBox = new Rectangle(0, 0, 0, 0);
                }
                else
                {
                    CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, 30, 30);
                }
                //StateSprite.Facing
                //state.StateSprite.Update(gametime);
                Sprite.Update(gametime);

                ExpandedCollisionBox = CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, 32, 32 + 8);
            }

            UpdateSpeed();
            //UpdateCollisionBox(Position);
            UpdatePosition(Position, gametime);
            // Velocity = new Vector2(-1, 0);
            state.Update(gametime);
        }
        public override void UpdateSpeed()
        {
            if (CollisionHandler.GetInstance().IsGoingToFall(this))
            {
                //Acceleration = new Vector2(0, 4000);
                Velocity = new Vector2(Velocity.X, 200);
            }

            else
            {
                Acceleration = new Vector2(0, 0);
                if (Direction == (int)MarioDirection.RIGHT)
                {
                    Velocity = new Vector2(75, 0);
                }

                else if (Direction == (int)MarioDirection.LEFT)
                {
                    Velocity = new Vector2(-75, 0);
                }
            }
        }
        public override void UpdatePosition(Vector2 location, GameTime gameTime) //use velocity
        {


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

