using GameSpace.Abstracts;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Machines;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace GameSpace.GameObjects.EnemyObjects
{
    public class Goomba : AbstractEnemy
    {
        public Goomba(Vector2 initalPosition)
        {
            ObjectID = (int)EnemyID.GOOMBA;
            Direction = (int)MarioDirection.LEFT;
            Position = initalPosition;
            state = new StateGoombaAlive();
            UpdateCollisionBox(Position);
            drawBox = false;
            hasCollidedOnTop = false;
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
            if (state is StateGoombaAlive && IsInview())
            {
                state.Update(gametime);
                UpdateSpeed();
                UpdateCollisionBox(Position, gametime);
                UpdateCollisionBox(Position);
            }

            else
            {
                DeleteCollisionBox();
                if (countDown == 90)
                {
                    state.StateSprite.SetVisible();
                }
            }
        }
        public override void Trigger()
        {
            if (!hasCollidedOnTop)
            {
                state = new StateGoombaDead();
                hasCollidedOnTop = true;
                countDown = 0;
                MusicHandler.GetInstance().PlaySoundEffect(2);
            }

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


