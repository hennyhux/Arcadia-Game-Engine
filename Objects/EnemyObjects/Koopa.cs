using GameSpace.Abstracts;
using GameSpace.Enums;
using GameSpace.Factories;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Objects.EnemyObjects
{
    public class Koopa : Enemy
    {
        public Koopa(Vector2 initalPosition)
        {
            ObjectID = (int)EnemyID.GREENKOOPA;
            Direction = (int)MarioDirection.LEFT;
            Position = initalPosition;
            state = new StateKoopaAliveLeft(this);
            drawBox = false;
        }
    }

    public class StateKoopaAliveLeft : EnemyStates
    {
        public StateKoopaAliveLeft(Enemy enemy) : base(enemy)
        {
            StateSprite = SpriteEnemyFactory.GetInstance().CreateGreenKoopaLeftSprite();
        }

        public override void Trigger()
        {
            enemy.state = new StateKoopaShell(enemy);
        }
    }

    public class StateKoopaAliveRight : EnemyStates
    {
        public StateKoopaAliveRight(Enemy enemy) : base(enemy)
        {
            StateSprite = SpriteEnemyFactory.GetInstance().CreateGreenKoopaRightSprite();
        }
        public override void Trigger()
        {
            enemy.state = new StateKoopaShell(enemy);
        }
    }

    public class StateKoopaShell : EnemyStates
    {
        private int countDown = 0;
        public StateKoopaShell(Enemy enemy) : base(enemy)
        {
            StateSprite = SpriteEnemyFactory.GetInstance().CreateGreenKoopaShellSprite();
        }

        public override void Update(GameTime gametime)
        {
            StateSprite.Update(gametime);
            countDown++;

            if (countDown == 250)
            {
                enemy.state = new StateKoopaShellAndLegs(enemy);
            }
        }

        public override void Trigger()
        {
            
        }
    }

    public class StateKoopaShellAndLegs : EnemyStates
    {
        private int countDown = 0;
        public StateKoopaShellAndLegs(Enemy enemy) : base(enemy)
        {
            StateSprite = SpriteEnemyFactory.GetInstance().CreateGreenKoopaShellAndLegsSprite();
        }

        public override void Update(GameTime gametime)
        {
            StateSprite.Update(gametime);
            countDown++;

            if (countDown == 250)
            {
                if (enemy.Direction == (int)MarioDirection.LEFT) enemy.state = new StateKoopaAliveLeft(enemy);

                else
                {
                    enemy.state = new StateKoopaAliveRight(enemy);
                }
            }
        }

        public override void Trigger()
        {
            
        }
    }
}
