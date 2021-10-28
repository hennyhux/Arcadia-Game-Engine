using GameSpace.Abstracts;
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
    public class RedKoopa : AbstractEnemy
    {
  
        public RedKoopa(Vector2 initalPosition)
        {
            ObjectID = (int)EnemyID.REDKOOPA;
            direction = (int)eFacing.LEFT;
            drawBox = false;
            Position = initalPosition;
            state = new StateRedKoopaAliveLeft(this);
            Sprite = SpriteEnemyFactory.GetInstance().CreateRedKoopaSprite();
            ExpandedCollisionBox = new Rectangle((int)(Position.X + Sprite.Texture.Width / 32), (int)Position.Y, Sprite.Texture.Width, Sprite.Texture.Height * 3);
            UpdateCollisionBox(Position);
        }

        public override void UpdatePosition(Vector2 location, GameTime gameTime) //use velocity
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

        public override void Trigger()
        {
            this.state = new StateRedKoopaDead(this);
            PreformShellOffset();
        }

        #region Collision Handling
        internal override void CollisionWithBlock(IGameObjects block)
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

        public IEnemyState GetCurrentState()
        {
            return state;
        }

        private void PreformShellOffset()
        {
            Position = new Vector2(Position.X, Position.Y + 20);
        }

        internal override void CollisionWithMario(IGameObjects mario)
        {

            if (this.state is StateRedKoopaAliveRight || this.state is StateRedKoopaAliveLeft)
            {
                if (EntityManager.DetectCollisionDirection(this, mario) == (int)CollisionDirection.UP)
                {
                    this.state = new StateRedKoopaDead(this);
                    PreformShellOffset();
                }
            } 
            else if(this.state is StateRedKoopaDeadRight || this.state is StateRedKoopaDeadLeft)
            {
                if (EntityManager.DetectCollisionDirection(this, mario) == (int)CollisionDirection.UP)
                {
                    this.state = new StateRedKoopaDead(this);
                }
            }
            else if(this.state is StateRedKoopaDead)
            {
                if (EntityManager.DetectCollisionDirection(this, mario) == (int)CollisionDirection.LEFT)
                {
                    this.state = new StateRedKoopaDeadLeft(this);
                }
                else if (EntityManager.DetectCollisionDirection(this, mario) == (int)CollisionDirection.RIGHT)
                {
                    this.state = new StateRedKoopaDeadRight(this);
                }
            }
        }

        #endregion
    }
}

