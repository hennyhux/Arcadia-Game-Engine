using GameSpace.EntitiesManager;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using GameSpace.Sprites;
using GameSpace.States;
using GameSpace.States.EnemyStates;
using GameSpace.States.StateMachines;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.GameObjects.EnemyObjects
{
    public class RedKoopa : IGameObjects
    {
        public IEnemyState state;
        public ISprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public Rectangle CollisionBox { get; set; }
        public int ObjectID { get; set; }

        private Boolean drawBox;
        private Boolean inFrame; //is the current enemy inside of the viewport? 
        public int direction;
        public Rectangle ExpandedCollisionBox { get; set; }
        private Boolean shifted;
        public RedKoopa(Vector2 initalPosition)
        {
            ObjectID = (int)EnemyID.REDKOOPA;
            direction = (int)eFacing.LEFT;
            drawBox = false;
            shifted = false;
            inFrame = true;
            this.Position = initalPosition;
            this.state = new StateRedKoopaAliveLeft(this);
            Sprite = SpriteEnemyFactory.GetInstance().CreateRedKoopaSprite();
            ExpandedCollisionBox = new Rectangle((int)(Position.X + Sprite.Texture.Width / 32), (int)Position.Y, Sprite.Texture.Width, Sprite.Texture.Height * 3);
            UpdateCollisionBox(Position);
        }

        public void Draw(SpriteBatch spritebatch)
        {
            if (shifted)
            {
                Position = new Vector2(Position.X, Position.Y - 20);
                shifted = false;
            }
            state.Draw(spritebatch, Position);
            if (drawBox) state.DrawBoundaries(spritebatch, CollisionBox);
        }

        public void Update(GameTime gametime)
        {
            state.Update(gametime);
            UpdatePosition(Position, gametime);
        }

        public void Trigger()
        {

        }

        public void UpdatePosition(Vector2 location, GameTime gameTime) //use velocity
        {
            if (EntityManager.IsGoingToFall((RedKoopa)this) && !(state is StateRedKoopaDead))
            {

                Velocity = new Vector2(0, Velocity.Y);
                Acceleration = new Vector2(0, 400);
            }

            else if (!EntityManager.IsGoingToFall((RedKoopa)this))
            {
                Acceleration = new Vector2(0, 0);
                if (direction == (int)eFacing.RIGHT && !(state is StateRedKoopaDead)) Velocity = new Vector2(85, 0);
                if (direction == (int)eFacing.LEFT && !(state is StateRedKoopaDead)) Velocity = new Vector2(-85, 0);
            }

            Velocity += Acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            UpdateCollisionBox(Position);
        }

        public void HandleCollision(IGameObjects entity)
        {
            switch (entity.ObjectID)
            {
                case (int)AvatarID.MARIO:
                    CollisionWithMario(entity);
                    break;

                case (int)BlockID.USEDBLOCK:
                case (int)BlockID.QUESTIONBLOCK:
                case (int)BlockID.FLOORBLOCK:
                case (int)BlockID.STAIRBLOCK:
                case (int)BlockID.COINBRICKBLOCK:
                case (int)BlockID.BRICKBLOCK:
                    CollisionWithBlock(entity);
                    break;
                case (int)ItemID.FIREBALL:
                    CollisionWithFireball(entity);
                    break;

                    //dead when colliding with fireball, etc 
            }
        }

        #region Collision Handling
        private void CollisionWithBlock(IGameObjects block)
        {
            //If alive and hits block stays alive
            if (this.state is StateRedKoopaAliveLeft || this.state is StateRedKoopaAliveRight)
            {
                if (EntityManager.DetectCollisionDirection(this, block) == (int)CollisionDirection.LEFT)
                {
                    state = new StateRedKoopaAliveLeft(this);
                    direction = (int)eFacing.LEFT;
                }

                if (EntityManager.DetectCollisionDirection(this, block) == (int)CollisionDirection.RIGHT)
                {
                    state = new StateRedKoopaAliveRight(this);
                    direction = (int)eFacing.RIGHT;
                }
                else if (EntityManager.DetectCollisionDirection(this, block) == (int)CollisionDirection.UP)
                {
                    PreformBounce();
                    HaltAllMotion();
                }

            }
            //If Dead and hits block stays dead
            else if (this.state is StateRedKoopaDeadLeft || this.state is StateRedKoopaDeadRight)
            {
                if (EntityManager.DetectCollisionDirection(this, block) == (int)CollisionDirection.LEFT)
                {
                    state = new StateRedKoopaDeadLeft(this);
                    direction = (int)eFacing.LEFT;
                }

                if (EntityManager.DetectCollisionDirection(this, block) == (int)CollisionDirection.RIGHT)
                {
                    state = new StateRedKoopaDeadRight(this);
                    direction = (int)eFacing.RIGHT;
                }

                else if (EntityManager.DetectCollisionDirection(this, block) == (int)CollisionDirection.DOWN)
                {
                    
                    HaltAllMotion();
                }

            }    
        }

        private void CollisionWithFireball(IGameObjects fireball)
        {
            this.state = new StateRedKoopaDead(this);
            PreformShellOffset();

        }

        public IEnemyState GetCurrentState()
        {
            return state;
        }

        private void PreformShellOffset()
        {
            Position = new Vector2(Position.X, Position.Y + 20);
            shifted = true;
        }

        private void CollisionWithMario(IGameObjects mario)
        {
            if (EntityManager.DetectCollisionDirection(this, mario) == (int)CollisionDirection.UP)
            {
                    this.state = new StateRedKoopaDead(this);
               // PreformShellOffset();
            }


            if (this.state is StateRedKoopaDead)
            {
                if (EntityManager.DetectCollisionDirection(this, mario) == (int)CollisionDirection.UP)
                {
                    //PreformShellOffsetDown();
                    this.state = new StateRedKoopaDead(this);
                }
                else if (EntityManager.DetectCollisionDirection(this, mario) == (int)CollisionDirection.RIGHT)
                {
                    //PreformShellOffsetDown();
                    this.state = new StateRedKoopaDeadRight(this);
                }
                else if (EntityManager.DetectCollisionDirection(this, mario) == (int)CollisionDirection.LEFT)
                {
                    //PreformShellOffsetDown();
                    this.state = new StateRedKoopaDeadLeft(this);
                }
            }
        }

        public void ToggleCollisionBoxes()
        {
            drawBox = !drawBox;
        }

        public bool IsCurrentlyColliding()
        {
            throw new NotImplementedException();
        }

        private void UpdateCollisionBox(Vector2 location)
        {
            this.CollisionBox = new Rectangle((int)location.X + state.StateSprite.Texture.Width / 4 + 2, (int)Position.Y,
                state.StateSprite.Texture.Width / 2, state.StateSprite.Texture.Height * 2);

            this.ExpandedCollisionBox = new Rectangle((int)location.X + state.StateSprite.Texture.Width / 4 - 6, (int)Position.Y,
             state.StateSprite.Texture.Width, (state.StateSprite.Texture.Height * 2) + 3);

        }

        private void PreformBounce()
        {
            Position = new Vector2(Position.X, Position.Y - 3);
        }

        private void HaltAllMotion()
        {
            Velocity = new Vector2(0, 0);
            Acceleration = new Vector2(0, 0);
        }


        #endregion
    }
}

