using GameSpace.EntitiesManager;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using GameSpace.States.ItemStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using GameSpace.Machines;


namespace GameSpace.GameObjects.ItemObjects
{
    public class OneUpShroom : IGameObjects
    {
        private IItemStates state;
        public ISprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public Rectangle CollisionBox { get; set; }
        public int ObjectID { get; set; }

        private bool hasCollided;
        private bool drawBox;


        public Rectangle ExpandedCollisionBox { get; set; }


        public OneUpShroom(Vector2 initialPosition)
        {
            ObjectID = (int)ItemID.ONEUPSHROOM;
            Sprite = SpriteItemFactory.GetInstance().CreateOneUpShroom();
            Position = initialPosition;
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);
            drawBox = false;
            hasCollided = false;
            ExpandedCollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 3);
            state = new StateOneUpShroomHidden(this);
        }

        public void Draw(SpriteBatch spritebatch)
        {
            Sprite.Draw(spritebatch, Position);
            if (drawBox)
            {
                Sprite.DrawBoundary(spritebatch, CollisionBox);
                Sprite.DrawBoundary(spritebatch, ExpandedCollisionBox);
            }
        }

        public void Update(GameTime gametime)
        {
            if (state is StateOneUpShroomHidden)
            {
                findMario();
            }

            UpdatePosition(Position, gametime);
            Sprite.Update(gametime);
        }

        public void Trigger()
        {
            if (!hasCollided)
            {
                Sprite.SetVisible();
                CollisionBox = new Rectangle();
                //play sound effect for 1-Up being collected
                MusicHandler.GetInstance().PlaySoundEffect(6);
            }
            hasCollided = true;

            //Increase Mario's lives
        }

        public void HandleCollision(IGameObjects entity)
        {
            switch (entity.ObjectID)
            {
                case (int)AvatarID.MARIO:
                    Trigger();
                    break;

                case (int)BlockID.USEDBLOCK:
                case (int)BlockID.QUESTIONBLOCK:
                case (int)BlockID.FLOORBLOCK:
                case (int)BlockID.STAIRBLOCK:
                case (int)BlockID.COINBRICKBLOCK:
                case (int)BlockID.BRICKBLOCK:
                    CollisionWithBlock(entity);
                    break;
            }
        }

        public void UpdatePosition(Vector2 location, GameTime gameTime)
        {
            if (EntityManager.IsGoingToFall(this))
            {
                Acceleration = new Vector2(0, 400);
            }

            else if (!EntityManager.IsGoingToFall(this))
            {
                Acceleration = new Vector2(0, 0);
                if (state is StateOneUpShroomRight)
                {
                    Velocity = new Vector2(85, 0);
                }

                if (state is StateOneUpShroomLeft)
                {
                    Velocity = new Vector2(-85, 0);
                }
            }

            Velocity += Acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            UpdateCollisionBox(Position);
        }

        public void ToggleCollisionBoxes()
        {
            drawBox = !drawBox;
        }

        public bool IsCurrentlyColliding()
        {
            throw new NotImplementedException();
        }

        public void CollisionWithBlock(IGameObjects entity)
        {
            if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.RIGHT)
            {
                Velocity = new Vector2(0, 0);
                state = new StateOneUpShroomRight(this);
            }

            else if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.LEFT)
            {
                Velocity = new Vector2(0, 0);
                state = new StateOneUpShroomLeft(this);
            }

            else if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.DOWN)
            {
                Acceleration = new Vector2(0, 0);
            }
        }

        private void UpdateCollisionBox(Vector2 location)
        {
            CollisionBox = new Rectangle((int)location.X + state.StateSprite.Texture.Width / 2 - 10, (int)Position.Y,
                state.StateSprite.Texture.Width * 2, state.StateSprite.Texture.Height * 2);

            ExpandedCollisionBox = new Rectangle((int)location.X + state.StateSprite.Texture.Width / 2 - 10, (int)Position.Y,
             state.StateSprite.Texture.Width * 2, (state.StateSprite.Texture.Height * 2) + 3);
        }

        private void findMario()
        {
            if (EntityManager.FindItem((int)AvatarID.MARIO).Position.X <= Position.X)
            {
                state = new StateOneUpShroomRight(this);

            }
            else
            {
                state = new StateOneUpShroomLeft(this);

            }
        }

        public bool RevealItem()
        {
            throw new NotImplementedException();
        }
    }
}
