using GameSpace.Abstracts;
using GameSpace.EntityManaging;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.GameObjects.EnemyObjects
{
    public interface IKoopaState
    {
        public void Draw(SpriteBatch spritebatch, Vector2 location);
        public void Update(GameTime gametime);
        public void Trigger();
        public void UpdateCollisionBox(Vector2 location);
        public void UpdateSpeed();
        public void UpdatePosition(Vector2 location, GameTime gameTime);
        public void FlipSprite();
    }

    public abstract class StateGreenKoopa : IKoopaState
    {
        private protected GreenKoopa koopa;
        private protected ISprite StateSprite;
        private protected IKoopaState previousState;

        public virtual void Draw(SpriteBatch spritebatch, Vector2 location)
        {
            StateSprite.Draw(spritebatch, location);
            if (koopa.drawBox)
            {
                StateSprite.DrawBoundary(spritebatch, koopa.CollisionBox);
            }
        }

        public abstract void Trigger();

        public virtual void Update(GameTime gametime)
        {
            StateSprite.Update(gametime);
            UpdateSpeed();
            UpdateCollisionBox(koopa.Position);
            UpdatePosition(koopa.Position, gametime);
        }

        public virtual void UpdateCollisionBox(Vector2 location)
        {
            koopa.CollisionBox = new Rectangle((int)location.X + 15, (int)location.Y,
              StateSprite.Texture.Width / 2 , StateSprite.Texture.Height * 2 );

            koopa.ExpandedCollisionBox = new Rectangle((int)location.X + 15, (int)location.Y,
                StateSprite.Texture.Width / 2, (StateSprite.Texture.Height * 2) + 4);
        }

        public virtual void UpdatePosition(Vector2 location, GameTime gameTime)
        {
            koopa.UpdatePosition(location, gameTime);
        }

        public virtual void UpdateSpeed()
        {
            koopa.UpdateSpeed();
        }

        public virtual void FlipSprite()
        {
            StateSprite.Facing = SpriteEffects.FlipHorizontally;
        }

    }

    public class GreenKoopaAliveState : StateGreenKoopa
    {
        
        public GreenKoopaAliveState(GreenKoopa koopa)
        {
            this.koopa = koopa;
            StateSprite = SpriteEnemyFactory.GetInstance().CreateGreenKoopaLeftSprite();
        }

        public override void Trigger()
        {
            koopa.State = new GreenKoopaShellState(koopa);
        }
    }

    public class GreenKoopaShellState : StateGreenKoopa
    {
        private int countDown = 0;
        public GreenKoopaShellState(GreenKoopa koopa)
        {
            this.koopa = koopa;
            StateSprite = SpriteEnemyFactory.GetInstance().CreateGreenKoopaShellSprite();
        }

        public override void Trigger()
        {
            koopa.State = new GreenKoopaShellMovingState(koopa);
        }

        public override void Update(GameTime gametime)
        {
            StateSprite.Update(gametime);
            countDown++;

            if (countDown == 250)
            {
                koopa.State = new GreenKoopaShellAndLegsState(koopa);
            }
        }

        public override void Draw(SpriteBatch spritebatch, Vector2 location)
        {
            StateSprite.Draw(spritebatch, new Vector2(location.X, location.Y + 10));
            if (koopa.drawBox)
            {
                StateSprite.DrawBoundary(spritebatch, koopa.CollisionBox);
            }
        }

    }

    public class GreenKoopaShellAndLegsState : StateGreenKoopa
    {
        private int countDown = 0;
        public GreenKoopaShellAndLegsState(GreenKoopa koopa)
        {
            this.koopa = koopa;
            StateSprite = SpriteEnemyFactory.GetInstance().CreateGreenKoopaShellAndLegsSprite();
        }

        public override void Trigger()
        {
            koopa.State = new GreenKoopaShellMovingState(koopa);
        }

        public override void Update(GameTime gametime)
        {
            StateSprite.Update(gametime);
            countDown++;
            if (countDown == 250)
            {
                koopa.State = new GreenKoopaAliveState(koopa);
            }
        }
        public override void Draw(SpriteBatch spritebatch, Vector2 location)
        {
            StateSprite.Draw(spritebatch, new Vector2(location.X, location.Y + 10));
            if (koopa.drawBox)
            {
                StateSprite.DrawBoundary(spritebatch, koopa.CollisionBox);
            }
        }

    }

    public class GreenKoopaShellMovingState : StateGreenKoopa
    {

        private int countDown = 0;
        public GreenKoopaShellMovingState(GreenKoopa koopa)
        {
            this.koopa = koopa;
            StateSprite = SpriteEnemyFactory.GetInstance().CreateGreenKoopaShellSprite();
        }


        public override void Trigger()
        {
            
        }

        public override void Update(GameTime gametime)
        {
            StateSprite.Update(gametime);
            UpdateSpeed();
            UpdateCollisionBox(koopa.Position);
            UpdatePosition(koopa.Position, gametime);
            countDown++;

            if (countDown == 250)
            {
                koopa.State = new GreenKoopaDeadState(koopa);
            }
        }

        public override void Draw(SpriteBatch spritebatch, Vector2 location)
        {
            StateSprite.Draw(spritebatch, new Vector2(location.X, location.Y + 10));
            if (koopa.drawBox)
            {
                StateSprite.DrawBoundary(spritebatch, koopa.CollisionBox);
            }
        }

        public override void UpdateSpeed()
        {
            if (CollisionHandler.GetInstance().IsGoingToFall(koopa))
            {
                koopa.Acceleration = new Vector2(0, 400);
            }

            else
            {
                koopa.Acceleration = new Vector2(0, 0);
                if (koopa.Direction == (int)MarioDirection.RIGHT)
                {
                    koopa.Velocity = new Vector2(300, 0);
                }

                else if (koopa.Direction == (int)MarioDirection.LEFT)
                {
                    koopa.Velocity = new Vector2(-300, 0);
                }
            }
        }
    }

    public class GreenKoopaDeadState : StateGreenKoopa
    {
        public GreenKoopaDeadState(GreenKoopa koopa)
        {
            this.koopa = koopa;
            StateSprite = SpriteEnemyFactory.GetInstance().CreateGreenKoopaShellSprite();
            StateSprite.SetVisible();
        }

        public override void Update(GameTime gametime)
        {
            koopa.DeleteCollisionBox();
        }

        public override void Draw(SpriteBatch spritebatch, Vector2 location)
        {
            
        }

        public override void Trigger()
        {
            
        }
    }

    public class GreenKoopa : AbstractEnemy
    {
        public IKoopaState State { get; set; }
        private int countDown;
        public GreenKoopa(Vector2 initalPosition)
        {
            ObjectID = (int)EnemyID.GREENKOOPA;
            Direction = (int)MarioDirection.LEFT;
            drawBox = false;
            Position = initalPosition;
            State = new GreenKoopaAliveState(this);
            countDown = 0;
        }

        public override void Update(GameTime gametime)
        {
            State.Update(gametime);
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            State.Draw(spritebatch, Position);
        }

        public override void Trigger()
        {
            State.Trigger();
        }

        public void FlipSprite()
        {
            State.FlipSprite();
        }

        public override void HandleCollision(IGameObjects entity)
        {
            switch (entity.ObjectID)
            {
                case (int)AvatarID.MARIO:
                    CollisionHandler.GetInstance().EnemyToMarioCollision(this);
                    break;

                case (int)BlockID.USEDBLOCK:
                case (int)BlockID.QUESTIONBLOCK:
                case (int)BlockID.FLOORBLOCK:
                case (int)BlockID.STAIRBLOCK:
                case (int)BlockID.COINBRICKBLOCK:
                case (int)BlockID.BRICKBLOCK:
                case (int)ItemID.MEDIUMPIPE:
                case (int)ItemID.SMALLPIPE:
                case (int)ItemID.WARPPIPEBODY:
                case (int)ItemID.WARPPIPEHEAD:
                case (int)ItemID.WARPPIPEHEADWITHMOB:
                case (int)ItemID.WARPVINEWITHBLOCK:
                case (int)ItemID.WARPPIPEROOM:
                    CollisionHandler.GetInstance().EnemyToBlockCollision(this, entity);
                    break;

                case (int)ItemID.FIREBALL:
                    Trigger();
                    break;

                case (int)EnemyID.GOOMBA:
                    CollisionHandler.GetInstance().EnemyToEnemyCollision(this, (Goomba)entity);
                    break;
            }
        }

        public void ChangeState(IKoopaState state)
        {
            State = state;
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

        public class StateGreenKoopaRemoved : AbstractEnemyState
        {
            public StateGreenKoopaRemoved()
            {
                StateSprite = SpriteEnemyFactory.GetInstance().CreateGreenKoopaLeftSprite();
                StateSprite.SetVisible();
            }
        }
    }
}