using GameSpace.Abstracts;
using GameSpace.Enums;
using GameSpace.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameSpace.Machines;



namespace GameSpace.GameObjects.EnemyObjects
{
    public class Goomba : AbstractEnemy
    {
        public Goomba(Vector2 initalPosition)
        {
            ObjectID = (int)EnemyID.GOOMBA;
            direction = (int)eFacing.LEFT;
            Sprite = SpriteEnemyFactory.GetInstance().CreateGoombaSprite();
            Position = initalPosition;
            state = new StateGoombaAlive();
            UpdateCollisionBox(Position);
            drawBox = false;
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            state.Draw(spritebatch, Position);
            if (drawBox && !hasCollidedOnTop)
            {
                state.DrawBoundaries(spritebatch, CollisionBox);
                state.DrawBoundaries(spritebatch, ExpandedCollisionBox);
            }
            if (hasCollidedOnTop)
            {
                countDown++;
            }

        }

        public override void Update(GameTime gametime)
        {

            if (state is StateGoombaAlive)
            {
                state.Update(gametime);
                if (!hasCollidedOnTop && IsInview())
                {
                    UpdatePosition(Position, gametime);
                }

                if (countDown == 90)
                {
                    state.StateSprite.SetVisible();
                }
            }
        }

        public override void Trigger()
        {
            state = new StateGoombaDead();
            countDown = 0;
            MusicHandler.GetInstance().PlaySoundEffect(2);

        }
    }
    public class StateGoombaDead : AbstractEnemyState
    {
        public StateGoombaDead()
        {
            StateSprite = SpriteEnemyFactory.GetInstance().CreateDeadGoombaSprite();
        }
    }

    public class StateGoombaAlive : AbstractEnemyState
    {
        public StateGoombaAlive()
        {
            StateSprite = SpriteEnemyFactory.GetInstance().CreateGoombaSprite();
        }
    }
}


