using GameSpace.EntitiesManager;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.Interfaces;
using GameSpace.States.ItemStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;



namespace GameSpace.GameObjects.ItemObjects
{
    public class Fireball : IGameObjects
    {

        private readonly IItemStates state;
        public ISprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public Rectangle CollisionBox { get; set; }
        public int ObjectID { get; set; }

        private Boolean hasCollided;
        private Boolean drawBox;
        public Mario Mario;

        public Fireball(Mario mario)
        {
            ObjectID = (int)ItemID.FIREBALL;
            Sprite = SpriteItemFactory.GetInstance().CreateFireBall();
            Mario = mario;
            Position = Mario.Position;
            CollisionBox = new Rectangle((int)Position.X + 5, (int)Position.Y, (Sprite.Texture.Width * 2 / 4) - 10, Sprite.Texture.Height * 2 + 5);
            hasCollided = false;
            drawBox = false;
            ++Mario.numFireballs;

            if (Mario.Facing == 0)
            {
                state = new StateFireballLeft(this);
            }
            else
            {
                state = new StateFireballRight(this);
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            Sprite.Draw(spritebatch, Position);
            state.Draw(spritebatch, Position);
            if (drawBox)
            {
                Sprite.DrawBoundary(spritebatch, CollisionBox);
            }
        }

        public void Update(GameTime gametime)
        {
            state.Update(gametime);
            Sprite.Update(gametime);
            UpdatePosition(Position, gametime);

        }

        public void Trigger()
        {
            if (!hasCollided)
            {
                Sprite.SetVisible();
                CollisionBox = new Rectangle(1, 1, 0, 0);
            }
            hasCollided = true;
            --Mario.numFireballs;
        }

        public void HandleCollision(IGameObjects entity)
        {

            switch (entity.ObjectID)
            {
                case (int)EnemyID.GOOMBA:
                case (int)EnemyID.GREENKOOPA:
                case (int)EnemyID.REDKOOPA:
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
            if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.DOWN)
            {
                Position = new Vector2(Position.X, Position.Y - 10);
                if (state is StateFireballRight) Velocity = new Vector2(45, 0);
                if (state is StateFireballLeft) Velocity = new Vector2(-45, 0);
                Acceleration = new Vector2(0, -200);
                state.Trigger();
            }
            else if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.RIGHT ||
                     EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.LEFT)
            {
                Trigger();

            }

        }

        private void UpdateCollisionBox(Vector2 location)
        {
            if (!hasCollided) CollisionBox = new Rectangle((int)Position.X + 5, (int)Position.Y, (Sprite.Texture.Width * 2 / 4) - 10, Sprite.Texture.Height * 2 + 5);
        }
    }
}

