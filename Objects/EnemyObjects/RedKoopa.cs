using GameSpace.EntitiesManager;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using GameSpace.Sprites;
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
        private IEnemyState state;
        public ISprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public Rectangle CollisionBox { get; set; }
        public int ObjectID { get; set; }

        private Boolean drawBox;
        private Boolean inFrame; //is the current enemy inside of the viewport? 
        private int direction;


        public RedKoopa(Vector2 initalPosition)
        {
            ObjectID = (int)EnemyID.GREENKOOPA;
            direction = (int)eFacing.LEFT;
            drawBox = false;
            inFrame = true;
            this.Position = initalPosition;
            this.state = new StateRedKoopaAliveLeft();
            UpdateCollisionBox(Position);
        }

        public void Draw(SpriteBatch spritebatch)
        {
            state.Draw(spritebatch, Position);
            if (drawBox) state.DrawBoundaries(spritebatch, CollisionBox);
        }

        public void Update(GameTime gametime)
        {
            state.Update(gametime);
            SetPosition(Position);
        }

        public void Trigger()
        {
            state.Trigger();
        }

        public void SetPosition(Vector2 location)
        {
            if (!state.CollidedWithMario && direction == (int)eFacing.LEFT)
            {
                this.Position = new Vector2(location.X - .8f, Position.Y);
            }

            if (!state.CollidedWithMario && direction == (int)eFacing.RIGHT)
            {
                this.Position = new Vector2(location.X + .8f, Position.Y);
            }

            UpdateCollisionBox(location);
        }

        public void HandleCollision(IGameObjects entity)
        {
            switch (entity.ObjectID)
            {
                case (int)AvatarID.MARIO:
                    CollisionWithMario(entity);
                    break;

                case (int)BlockID.BRICKBLOCK:
                    CollisionWithBlock(entity);
                    break;

                    //dead when colliding with fireball, etc 
            }
        }

        #region Collision Handling
        private void CollisionWithBlock(IGameObjects block)
        {
            if (EntityManager.DetectCollisionDirection(this, block) == (int)CollisionDirection.LEFT)
            {
                state = new StateRedKoopaAliveLeft();
                direction = (int)eFacing.LEFT;
            }

            if (EntityManager.DetectCollisionDirection(this, block) == (int)CollisionDirection.RIGHT)
            {
                state = new StateRedKoopaAliveRight();
                direction = (int)eFacing.RIGHT;
            }
        }

        private void CollisionWithMario(IGameObjects mario)
        {
            if (EntityManager.DetectCollisionDirection(this, mario) == (int)CollisionDirection.UP)
            {
                this.Trigger();
            }

            //if the current state is shelled: (state.StateSprite is GreenKoopaShelled)
            //then stop the countdown and launch the object (green koopa)
            //also need to change the behavior 
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
        }
        #endregion
    }
}

