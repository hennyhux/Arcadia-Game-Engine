using GameSpace.Abstracts;
using GameSpace.EntityManaging;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.Interfaces;
using GameSpace.Machines;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.GameObjects.EnemyObjects
{
    public class SpinyRefactored : Enemy
    {
        private readonly int searchState;
        public SpinyRefactored(Vector2 initalPosition)
        {
            state = new StateSpinyAliveLeft(this);
            searchState = 1;
            Position = initalPosition;
            drawBox = ((Mario)FinderHandler.GetInstance().FindItem((int)AvatarID.MARIO)).ReturnDrawCollisionBoxes();
            ObjectID = (int)EnemyID.SPINY;
            Direction = (int)MarioDirection.LEFT;
        }

        public override void HandleCollision(IGameObjects entity)
        {
            switch (entity.ObjectID)
            {

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
                case (int)ItemID.WARPPIPEROOM:
                    EnemyCollisionHandler.GetInstance().HandleBlockCollision(this, entity);
                    break;

                case (int)ItemID.FIREBALL:
                    Trigger();
                    break;
            }
        }
    }
    public abstract class StateSpiny : IMobState
    {
        public ISprite StateSprite { get; set; }
        protected internal SpinyRefactored enemy;

        protected StateSpiny(SpinyRefactored enemy)
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
                StateSprite.Texture.Width / 8, StateSprite.Texture.Height / 2);

            enemy.ExpandedCollisionBox = new Rectangle((int)location.X, (int)location.Y,
                StateSprite.Texture.Width / 8, (StateSprite.Texture.Height / 2) + 6);
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
                    enemy.Velocity = new Vector2(75, 0);
                }

                else if (enemy.Direction == (int)MarioDirection.LEFT)
                {
                    enemy.Velocity = new Vector2(-75, 0);
                }
            }
        }

        protected internal void DestoryCollisionBox()
        {
            enemy.CollisionBox = new Rectangle(0, 0, 0, 0);
            enemy.ExpandedCollisionBox = new Rectangle(0, 0, 0, 0);
        }

        protected internal void HaltAllMotion()
        {
            enemy.Velocity = new Vector2(0, 0);
            enemy.Acceleration = new Vector2(0, 0);
        }
    }

    public class StateSpinyAliveLeft : StateSpiny
    {
        public StateSpinyAliveLeft(SpinyRefactored spiny) : base(spiny)
        {
            StateSprite = SpriteEnemyFactory.GetInstance().CreateSpinySprite();
        }

        public override void Trigger()
        {
            //death when triggered, or whatever the behavior is 
            DestoryCollisionBox();
            HaltAllMotion();
            enemy.state = new StateSpinyDeath(enemy);
        }
    }

    public class StateSpinyAliveRight : StateSpiny
    {
        public StateSpinyAliveRight(SpinyRefactored spiny) : base(spiny)
        {
            StateSprite = SpriteEnemyFactory.GetInstance().CreateSpinyRightSprite();
        }

        public override void Trigger()
        {
            //death when triggered, or whatever the behavior is 
            DestoryCollisionBox();
            HaltAllMotion();
            enemy.state = new StateSpinyDeath(enemy);
        }
    }

    public class StateSpinyDeath : StateSpiny
    {
        public StateSpinyDeath(SpinyRefactored enemy) : base(enemy)
        {
            StateSprite = SpriteEnemyFactory.GetInstance().CreateSpinyRightSprite();
            StateSprite.SetVisible();
            MarioHandler.GetInstance().IncrementMarioPoints(100);
        }

        public override void Trigger()
        {
            // does nothing
        }
    }
}

