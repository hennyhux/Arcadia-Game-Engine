using GameSpace.Abstracts;
using GameSpace.EntityManaging;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;

namespace GameSpace.GameObjects.EnemyObjects
{
    public class GreenKoopa : AbstractEnemy
    {
        private int countdown;
        public GreenKoopa(Vector2 initalPosition)
        {
            ObjectID = (int)EnemyID.GREENKOOPA;
            Direction = (int)eFacing.LEFT;
            drawBox = false;
            Position = initalPosition;
            state = new StateGreenKoopaAliveFaceLeft();
            UpdateCollisionBox(Position);
            countdown = 0;
        }

        public override void Update(GameTime gametime)
        {
            base.Update(gametime);

            if (hasCollidedOnTop)
            {
                countdown++;
            }

            if (countdown == 250)
            {
                state = new StateGreenKoopaShellAndLegs();
            }

            if (countdown == 500)
            {
                state = new StateGreenKoopaAliveFaceLeft();
                countdown = 0;
                hasCollidedOnTop = false;
            }

        }

        public override void HandleCollision(IGameObjects entity)
        {
            switch (entity.ObjectID)
            {
                case (int)AvatarID.MARIO:
                    CollisionHandler.GetInstance().EnemyToMarioCollision((GreenKoopa)this, entity);
                    break;

                case (int)BlockID.USEDBLOCK:
                case (int)BlockID.QUESTIONBLOCK:
                case (int)BlockID.FLOORBLOCK:
                case (int)BlockID.STAIRBLOCK:
                case (int)BlockID.COINBRICKBLOCK:
                case (int)BlockID.BRICKBLOCK:
                case (int)ItemID.BIGPIPE:
                case (int)ItemID.MEDIUMPIPE:
                case (int)ItemID.SMALLPIPE:
                    CollisionHandler.GetInstance().EnemyToBlockCollision(this, entity);
                    break;

                case (int)ItemID.FIREBALL:
                    Trigger();
                    break;
            }
        }

        public override void UpdatePosition(Vector2 location, GameTime gameTime)
        {
            if (state is StateGreenKoopaDeadMoving)
            {
                Position += new Vector2(300, 0) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            else if (!(state is StateGreenKoopaShelled) &&
                !(state is StateGreenKoopaShellAndLegs) && 
                !(state is StateGreenKoopaDeadMoving))
            {
                Velocity += Acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

        }

        public override void Trigger()
        {
            state = new StateGreenKoopaShelled();
            hasCollidedOnTop = true;
        }

        protected override void UpdateCollisionBox(Vector2 location)
        {

            base.UpdateCollisionBox(location);

            CollisionBox = new Rectangle((int)location.X + 20, (int)location.Y,
              state.StateSprite.Texture.Width / 2, state.StateSprite.Texture.Height * 2);
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
                StateSprite = SpriteEnemyFactory.GetInstance().CreateGreenKoopaLeftSprite();
            }
        }

        public class StateGreenKoopaDeadMoving : AbstractEnemyState
        {
            public StateGreenKoopaDeadMoving()
            {
                StateSprite = SpriteEnemyFactory.GetInstance().CreateGreenKoopaShellSprite();
            }
        }

        public class StateGreenKoopaShellAndLegs : AbstractEnemyState
        {
            public StateGreenKoopaShellAndLegs()
            {
                StateSprite = SpriteEnemyFactory.GetInstance().CreateGreenKoopaShellAndLegsSprite();
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
}