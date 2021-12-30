using GameSpace.Abstracts;
using GameSpace.EntityManaging;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Machines;
using Microsoft.Xna.Framework;

namespace GameSpace.Objects.EnemyObjects
{
    public class UberGoomba : Enemy
    {
        public UberGoomba(Vector2 location)
        {
            state = new StateUberGoombaAlive(this);
            Position = location;
            drawBox = false;
            ObjectID = (int)EnemyID.UBERGOOMBA;
            Direction = (int)MarioDirection.LEFT;
        }
    }

    public class StateUberGoombaAlive : EnemyStates
    {
        public StateUberGoombaAlive(Enemy uberGoomba) : base(uberGoomba)
        {
            StateSprite = SpriteEnemyFactory.GetInstance().CreateUberGoombaSprite();
        }

        public override void Trigger()
        {
            enemy.state = new StateUberGoombaBersek(enemy);
            MusicHandler.GetInstance().PlaySoundEffect(13);
        }
    }

    public class StateUberGoombaBersek : EnemyStates
    {
        public StateUberGoombaBersek(Enemy uberGoomba) : base(uberGoomba)
        {
            StateSprite = SpriteEnemyFactory.GetInstance().CreateUberGoombaBerserkSprite();
        }

        public override void Trigger()
        {
            enemy.state = new StateUberGoombaDead(enemy);
            HUDHandler.GetInstance().UpdateExp(20);
        }

        internal override void UpdateSpeed()
        {
            if (EnemyCollisionHandler.GetInstance().IsGoingToFall(enemy))
            {
                enemy.Acceleration = new Vector2(0, 400);
            }

            else
            {
                enemy.Acceleration = new Vector2(0, 0);
                if (enemy.Direction == (int)MarioDirection.RIGHT)
                {
                    enemy.Velocity = new Vector2(175, 0);
                }

                else if (enemy.Direction == (int)MarioDirection.LEFT)
                {
                    enemy.Velocity = new Vector2(-175, 0);
                }
            }
        }
    }

    public class StateUberGoombaDead : EnemyStates
    {
        private Vector2 initialPosition;
        private Vector2 goalPosition;
        public StateUberGoombaDead(Enemy uberGoomba) : base(uberGoomba)
        {
            StateSprite = SpriteEnemyFactory.GetInstance().CreateUberGoombaDeadSprite();
            initialPosition = uberGoomba.Position;
            MarioHandler.GetInstance().IncrementMarioPoints(1000);
            DestoryCollisionBox();
            HaltAllMotion();
            CalcGoalPos();
            enemy.Acceleration = new Vector2(0, -445);
        }

        private void CalcGoalPos()
        {
            goalPosition = new Vector2(initialPosition.X, initialPosition.Y - 45f);
        }

        public override void Trigger()
        {

        }

        public override void Update(GameTime gametime)
        {
            StateSprite.Update(gametime);
            UpdatePosition(enemy.Position, gametime);
        }

        internal override void UpdatePosition(Vector2 location, GameTime gametime)
        {
            enemy.Velocity += enemy.Acceleration * (float)gametime.ElapsedGameTime.TotalSeconds;
            enemy.Position += enemy.Velocity * (float)gametime.ElapsedGameTime.TotalSeconds;

            if (enemy.Position.Y <= goalPosition.Y)
            {
                enemy.Acceleration = new Vector2(0, 445);
            }
        }
    }

}
