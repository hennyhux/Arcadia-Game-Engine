using GameSpace.Abstracts;
using GameSpace.EntitiesManager;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;

namespace GameSpace.GameObjects.EnemyObjects
{
    public class GreenKoopa : AbstractEnemy
    {
        public GreenKoopa(Vector2 initalPosition)
        {
            ObjectID = (int)EnemyID.GREENKOOPA;
            direction = (int)eFacing.LEFT;
            drawBox = false;
            Sprite = SpriteEnemyFactory.GetInstance().CreateGreenKoopaSprite();
            Position = initalPosition;
            state = new StateGreenKoopaAliveFaceLeft();
            UpdateCollisionBox(Position);
        }

        public override void UpdatePosition(Vector2 location, GameTime gameTime)
        {
            if (EntityManager.IsGoingToFall(this) && !(state is StateGreenKoopaShelled))
            {

                Velocity = new Vector2(0, Velocity.Y);
                Acceleration = new Vector2(0, 400);
            }

            else if (!EntityManager.IsGoingToFall(this))
            {
                Acceleration = new Vector2(0, 0);
                if (direction == (int)eFacing.RIGHT && !(state is StateGreenKoopaShelled)) Velocity = new Vector2(85, 0);
                if (direction == (int)eFacing.LEFT && !(state is StateGreenKoopaShelled)) Velocity = new Vector2(-85, 0);
            }

            if (state is StateGreenKoopaShelled) HaltAllMotion();
            Velocity += Acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            UpdateCollisionBox(Position);
        }

        internal override void UpdateCollisionBox(Vector2 location)
        {
            CollisionBox = new Rectangle((int)location.X + state.StateSprite.Texture.Width / 4, (int)Position.Y,
                state.StateSprite.Texture.Width / 2, state.StateSprite.Texture.Height * 2);

            ExpandedCollisionBox = new Rectangle((int)location.X + state.StateSprite.Texture.Width / 4, (int)Position.Y,
                state.StateSprite.Texture.Width / 2, (state.StateSprite.Texture.Height * 2) + 4); // MAGIC NUMBERS 
        }

        internal override void CollisionWithBlock(IGameObjects block)
        {
            if (EntityManager.DetectCollisionDirection(this, block) == (int)CollisionDirection.LEFT)
            {
                state = new StateGreenKoopaAliveFaceLeft();
                direction = (int)eFacing.LEFT;
            }

            if (EntityManager.DetectCollisionDirection(this, block) == (int)CollisionDirection.RIGHT)
            {
                state = new StateGreenKoopaAliveFaceRight();
                direction = (int)eFacing.RIGHT;
            }
        }

        internal override void CollisionWithFireball(IGameObjects fireball)
        {

        }

        internal override void CollisionWithMario(IGameObjects mario)
        {
            switch (EntityManager.DetectCollisionDirection(this, mario))
            {
                case (int)CollisionDirection.UP:
                    if (!(state is StateGreenKoopaShelled)) state = new StateGreenKoopaShelled();
                    if (state is StateGreenKoopaShelled) state = new StateGreenKoopaDeadMoving();
                    break;
            }
        }
    }

    public class StateGreenKoopaAliveFaceRight : AbstractEnemyState
    {
        public StateGreenKoopaAliveFaceRight()
        {
            StateSprite = SpriteEnemyFactory.GetInstance().CreateGreenKoopaRightSprite();
        }
    }

    public class StateGreenKoopaAliveFaceLeft : AbstractEnemyState
    {
        public StateGreenKoopaAliveFaceLeft()
        {
            StateSprite = SpriteEnemyFactory.GetInstance().CreateGreenKoopaSprite();
        }
    }

    public class StateGreenKoopaDeadMoving : AbstractEnemyState
    {
        public StateGreenKoopaDeadMoving()
        {
            StateSprite = SpriteEnemyFactory.GetInstance().CreateGreenKoopaShellSprite();
        }
    }

    public class StateGreenKoopaShelled : AbstractEnemyState
    {
        public StateGreenKoopaShelled()
        {
            StateSprite = SpriteEnemyFactory.GetInstance().CreateGreenKoopaShellSprite();
        }
    }
}