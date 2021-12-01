using GameSpace.Abstracts;
using GameSpace.Camera2D;
using GameSpace.EntityManaging;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using GameSpace.Machines;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

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

    public abstract class StateUberGoomba : IMobState
    {
        public ISprite StateSprite { get; set; }
        internal protected UberGoomba enemy;

        protected StateUberGoomba(UberGoomba enemy)
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

        public abstract void Trigger();

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
                StateSprite.Texture.Width + 10, StateSprite.Texture.Height * 3);

            enemy.ExpandedCollisionBox = new Rectangle((int)location.X, (int)location.Y,
                StateSprite.Texture.Width + 10, (StateSprite.Texture.Height * 3) + 4);
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
    public class StateUberGoombaAlive : StateUberGoomba
    {
        public StateUberGoombaAlive(UberGoomba uberGoomba) : base(uberGoomba)
        {
            StateSprite = SpriteEnemyFactory.GetInstance().CreateUberGoombaSprite();
        }

        public override void Trigger()
        {
            enemy.state = new StateUberGoombaBersek(enemy);
            MusicHandler.GetInstance().PlaySoundEffect(13);
        }

    }

    public class StateUberGoombaBersek : StateUberGoomba
    {
        public StateUberGoombaBersek(UberGoomba uberGoomba) : base(uberGoomba)
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

    public class StateUberGoombaDead : StateUberGoomba
    {

        private Vector2 initialPosition;
        private Vector2 goalPosition;
        public StateUberGoombaDead(UberGoomba uberGoomba) : base(uberGoomba)
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

    public interface IMobState
    {
        public ISprite StateSprite { get; set; }
        public void Draw(SpriteBatch spritebatch, Vector2 position);
        public void DrawBoundingBox(SpriteBatch spritebatch, Rectangle collisionBox);
        public void Trigger();
        public void Update(GameTime gametime);
    }

    public abstract class Enemy : IGameObjects
    {
        public ISprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public Rectangle CollisionBox { get; set; }
        public Rectangle ExpandedCollisionBox { get; set; }
        public int ObjectID { get; set; }
        public int Direction { get; set; }
        internal IMobState state;

        internal bool drawBox;

        public virtual void Draw(SpriteBatch spritebatch)
        {
            state.Draw(spritebatch, Position);

            if (drawBox) state.DrawBoundingBox(spritebatch, CollisionBox);
        }

        public virtual void HandleCollision(IGameObjects entity)
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
                    if (state is StateUberGoombaBersek)
                    {
                        entity.Trigger();
                        MarioHandler.GetInstance().IncrementMarioPoints(100);
                    }
                    break;
            }
        }

        public virtual bool RevealItem()
        {
            return false; // an enemy does not have any items
        }

        public virtual void ToggleCollisionBoxes()
        {
            drawBox = !drawBox;
        }

        public virtual void Trigger()
        {
            state.Trigger();
            MusicHandler.GetInstance().PlaySoundEffect(2);
        }

        public virtual void Update(GameTime gametime)
        {
            state.Update(gametime);
        }

        private protected bool IsInview()
        {
            Camera copyCam = FinderHandler.GetInstance().FindCameraCopy();
            return (Position.X > copyCam.Position.X && Position.X < copyCam.Position.X + 800);
        }
    }
}
