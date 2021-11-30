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
using GameSpace.Machines;
namespace GameSpace.GameObjects.EnemyObjects
{
    public class Lakitu : AbstractEnemy
    {
        private int searchState;//0 passed mario, now throw spiny, 1 going left towards mario, 2 going right towards mario
        public Lakitu(Vector2 initalPosition)
        {
            Debug.Print("LAKITU CREATED()");
            ObjectID = (int)EnemyID.LAKITU;
            searchState = 1;
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

        public void trackMovement(GameTime gametime)
        {
            if (searchState == 0 && !(state is StateLakituThrowing))// Lakitu has alreay thrown a spiny, begin searching again
            {
                if (FinderHandler.GetInstance().FindItem((int)AvatarID.MARIO).Position.X < Position.X)
                {
                    searchState = 1;
                }
                else if (FinderHandler.GetInstance().FindItem((int)AvatarID.MARIO).Position.X > Position.X)
                {
                    searchState = 2;
                }
            }
            if (searchState == 1)
            {
                if(FinderHandler.GetInstance().FindItem((int)AvatarID.MARIO).Position.X < Position.X)
                {
                    Velocity = new Vector2(-100, 0);
                }
                else if (FinderHandler.GetInstance().FindItem((int)AvatarID.MARIO).Position.X > Position.X + 200)//passed mario
                {
                    searchState = 0;
                    //Enter Throw Animation
                    state = new StateLakituThrowing(this);
                }
            }
            if (searchState == 2)
            {
                if (FinderHandler.GetInstance().FindItem((int)AvatarID.MARIO).Position.X > Position.X)
                {
                    Velocity = new Vector2(100, 0);
                }
                else if (FinderHandler.GetInstance().FindItem((int)AvatarID.MARIO).Position.X < Position.X - 200)//passed mario
                {
                    searchState = 0;
                    //Enter Throw Animation
                    state = new StateLakituThrowing(this);
                }
            }
            
        }
        public override void Update(GameTime gametime)
        {

            //UpdateSpeed();
            //UpdateCollisionBox(Position);

            //Velocity = new Vector2(-1, 0);
            
            if (!(state is StateLakituDead))
            {
                trackMovement(gametime);
                if (Velocity.X > 0 && !(state is StateLakituRight) && !(state is StateLakituThrowing) && !(state is StateLakituDead))
                //if (Velocity.X > 0 && (state is StateLakituLeft) && !(state is StateLakituDead))
                {

                    state = new StateLakituRight(this);

                }
                else if (Velocity.X < 0 && !(state is StateLakituLeft) && !(state is StateLakituThrowing) && !(state is StateLakituDead))
                //else if (Velocity.X > 0 && (state is StateLakituRight) && !(state is StateLakituDead))
                {
                    state = new StateLakituLeft(this);
                }
                if (state is StateLakituDead)
                {
                    CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, 48, 48);
                }
                if (state is StateLakituDead)
                {
                    CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, 48, 48);
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
            UpdatePosition(Position, gametime);
            state.Update(gametime);
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
            if(!(state is StateLakituDead))
            {
                Debug.Print("LAKITU KILLED");
                state = new StateLakituDead(this);
                //SpawnCloudObject();
                //CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, 48, 48);
                CollisionBox = new Rectangle(0, 0, 0, 0);
                //ExpandedCollisionBox = CollisionBox;
            }


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

