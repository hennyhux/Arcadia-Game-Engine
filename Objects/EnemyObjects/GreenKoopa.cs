using GameSpace.Abstracts;
using GameSpace.EntitiesManager;
using GameSpace.EntityManaging;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

            if (hasCollidedOnTop) countdown++;

            if (countdown == 100)
            {
                state = new StateGreenKoopaShellAndLegs();
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