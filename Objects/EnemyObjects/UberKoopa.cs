using GameSpace.EntityManaging;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Objects.EnemyObjects
{
    public abstract class StateUberKoopa : IMobState
    {
        public ISprite StateSprite { get; set; }
        internal protected UberKoopa enemy;

        protected StateUberKoopa(UberKoopa enemy)
        {
            this.enemy = enemy;
        }

        public virtual void Draw(SpriteBatch spritebatch, Vector2 position)
        {
            StateSprite.Draw(spritebatch, position);
        }

        public virtual void DrawBoundingBox(SpriteBatch spritebatch, Rectangle collisionBox)
        {
            StateSprite.DrawBoundary(spritebatch, collisionBox);
        }

        public virtual void Trigger()
        {
            ++enemy.TimesCollided;
            if (enemy.TimesCollided == 3)
            {
                enemy.state = new StateUberKoopaDead(enemy);
            }
        }

        public virtual void Update(GameTime gametime)
        {
            StateSprite.Update(gametime);
            UpdatePosition(enemy.Position, gametime);
            UpdateCollisionBox(enemy.Position);
            UpdateSpeed();
        }

        internal virtual void UpdatePosition(Vector2 location, GameTime gametime)
        {
            enemy.Velocity += enemy.Acceleration * (float)gametime.ElapsedGameTime.TotalSeconds;
            enemy.Position += enemy.Velocity * (float)gametime.ElapsedGameTime.TotalSeconds;
        }

        internal virtual void UpdateCollisionBox(Vector2 location)
        {
            enemy.CollisionBox = new Rectangle((int)location.X, (int)location.Y,
                StateSprite.Texture.Width , StateSprite.Texture.Height * 2);

            enemy.ExpandedCollisionBox = new Rectangle((int)location.X, (int)location.Y,
                StateSprite.Texture.Width , (StateSprite.Texture.Height * 2) + 4);
        }

        internal virtual void UpdateSpeed()
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
                    enemy.Velocity = new Vector2(45, 0);
                }

                else if (enemy.Direction == (int)MarioDirection.LEFT)
                {
                    enemy.Velocity = new Vector2(-45, 0);
                }
            }
        }

        internal protected void DestoryCollisionBox()
        {
            enemy.CollisionBox = new Rectangle(0, 0, 0, 0);

            enemy.ExpandedCollisionBox = new Rectangle(0, 0, 0, 0);
        }

        internal protected void HaltAllMotion()
        {
            enemy.Velocity = new Vector2(0, 0);
            enemy.Acceleration = new Vector2(0, 0);
        }

    }
    public class UberKoopa : Enemy
    {
        public int TimesCollided { get; set; }
        public UberKoopa(Vector2 location)
        {
            state = new StateUberKoopaAliveLeft(this);
            Position = location;
            drawBox = false;
            ObjectID = (int)EnemyID.UBERKOOPA;
            Direction = (int)MarioDirection.LEFT;
            TimesCollided = 0;
        }
        public override void HandleCollision(IGameObjects entity)
        {
            switch (entity.ObjectID)
            {
                case (int)AvatarID.MARIO:
                    EnemyCollisionHandler.GetInstance().HandleMarioCollision(this);
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
                case (int)ItemID.WARPPIPEBODY:
                case (int)ItemID.WARPPIPEHEAD:
                case (int)ItemID.WARPPIPEHEADWITHMOB:
                case (int)ItemID.WARPVINEWITHBLOCK:
                case (int)ItemID.WARPPIPEROOM:
                    EnemyCollisionHandler.GetInstance().HandleBlockCollision(this, entity);
                    break;

                case (int)ItemID.FIREBALL:
                    Trigger();
                    break;

                case (int)EnemyID.GOOMBA:
                    break;
            }
        }
    }

    public class StateUberKoopaAliveLeft : StateUberKoopa
    {
        public StateUberKoopaAliveLeft(UberKoopa enemy) : base(enemy)
        {
            StateSprite = SpriteEnemyFactory.GetInstance().CreateUberKoopaSprite();
        }
    }

    public class StateUberKoopaAliveRight : StateUberKoopa
    {

        public StateUberKoopaAliveRight(UberKoopa enemy) : base(enemy)
        {
            StateSprite = SpriteEnemyFactory.GetInstance().CreateUberKoopaRightSprite();
        }
    }

    public class StateUberKoopaDead : StateUberKoopa
    {
        private Vector2 initialPosition;
        private Vector2 goalPosition;
        public StateUberKoopaDead(UberKoopa enemy) : base(enemy)
        {
            StateSprite = SpriteEnemyFactory.GetInstance().CreateUberKoopaDeadSprite();
            initialPosition = enemy.Position;
            HaltAllMotion();
            DestoryCollisionBox();
            CalcGoalPos();
            enemy.Acceleration = new Vector2(0, -445);
        }

        private void CalcGoalPos()
        {
            goalPosition = new Vector2(initialPosition.X, initialPosition.Y - 45f);
        }

        public override void Trigger()
        {
            //do nothing, chilling 
        }

        public override void Update(GameTime gametime)
        {
            StateSprite.Update(gametime);
            UpdatePosition(enemy.Position, gametime);
        }
    }

}
