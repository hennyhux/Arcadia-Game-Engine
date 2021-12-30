using GameSpace.Abstracts;
using GameSpace.Enums;
using GameSpace.Factories;
using Microsoft.Xna.Framework;



namespace GameSpace.GameObjects.EnemyObjects
{
    public class Goomba : Enemy
    {
        public Goomba(Vector2 initalPosition)
        {
            ObjectID = (int)EnemyID.GOOMBA;
            Direction = (int)MarioDirection.LEFT;
            Position = initalPosition;
            state = new StateGoombaAlive(this);
            drawBox = false;
        }
    }

    public class StateGoombaDead : EnemyStates
    {
        private Vector2 initialPosition;
        private Vector2 goalPosition;
        public StateGoombaDead(Enemy enemy) : base(enemy)
        {
            StateSprite = SpriteEnemyFactory.GetInstance().CreateDeadGoombaSprite();
            initialPosition = enemy.Position;
            DestoryCollisionBox();
            HaltAllMotion();
            CalcGoalPos();
            enemy.Acceleration = new Vector2(0, -445);
        }

        private void CalcGoalPos()
        {
            goalPosition = new Vector2(initialPosition.X, initialPosition.Y - 45f);
        }

        public override void Update(GameTime gametime)
        {
            StateSprite.Update(gametime);
            UpdatePosition(enemy.Position, gametime);
        }

        internal override void UpdatePosition(Vector2 location, GameTime gametime)
        {
            base.UpdatePosition(location, gametime);

            if (enemy.Position.Y <= goalPosition.Y)
            {
                enemy.Acceleration = new Vector2(0, 445);
            }
        }
    }

    public class StateGoombaAlive : EnemyStates
    {
        public StateGoombaAlive(Enemy enemy) : base(enemy)
        {
            StateSprite = SpriteEnemyFactory.GetInstance().CreateGoombaSprite();
        }

        public override void Trigger()
        {
            enemy.state = new StateGoombaDead(enemy);
        }
    }
}


