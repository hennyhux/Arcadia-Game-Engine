using GameSpace.EntityManaging;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.GameObjects.EnemyObjects;
using GameSpace.GameObjects.ExtraItemsObjects;
using GameSpace.GameObjects.ItemObjects;
using GameSpace.Interfaces;
using GameSpace.Level;
using GameSpace.Sprites;
using GameSpace.States.BlockStates;
using GameSpace.States.MarioStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace GameSpace.GameObjects.BlockObjects
{
    public class Mario : IGameObjects
    {
        private bool drawBox;
        public LevelRestart levelRestart;
        public GameRoot hey;
        public MarioSprite sprite { get; set; }
        public MarioDirection Facing { get; set; }
        public IMarioPowerUpStates MarioPowerUpState { get; set; }
        public IMarioActionStates MarioActionState { get; set; }
        public ISprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public Rectangle CollisionBox { get; set; }
        public int ObjectID { get; set; }
        public int numFireballs;
        public Rectangle ExpandedCollisionBox { get; set; }
        public int numCoinsCollected { get; set; }
        public int score { get; set; }

        public bool IsDead { get; set; }

        public bool IsInvincible { get; set; }

        public double invincibleTimer { get; set; }

        public string Player { get; set; }

        public bool onCloud { get; set; }

        public bool isClimbing { get; set; }

        public Mario(Vector2 initLocation)
        {
            ObjectID = (int)AvatarID.MARIO;
            drawBox = false;
            Position = new Vector2((int)initLocation.X, (int)initLocation.Y);
            numFireballs = 30;
            //marioLives = 3;
            Acceleration = new Vector2(0, 0);//NEW
            sprite = MarioFactory.GetInstance().CreateSprite(1);
            MarioPowerUpState = new SmallMarioState(this);
            MarioActionState = new SmallMarioStandingState(this);
            numCoinsCollected = 0;
            score = 0;
            invincibleTimer = 0;
            IsInvincible = false;
            Player = "Mario";
            IsDead = false;
            onCloud = false;

        }

        public void Draw(SpriteBatch spritebatch)
        {

            switch (Facing)
            {
                case MarioDirection.LEFT:
                    sprite.Facing = SpriteEffects.None;
                    break;
                case MarioDirection.RIGHT:
                    sprite.Facing = SpriteEffects.FlipHorizontally;
                    break;
            }
            if(onCloud == false)
            {
                sprite.Draw(spritebatch, Position, IsInvincible);
            }
            else
            {
                sprite.Draw(spritebatch, Position, IsInvincible, onCloud);
            }
            if (drawBox)
            {
                sprite.DrawBoundary(spritebatch, CollisionBox);
            }
        }

        public void CloudMovement(GameTime gametime)
        {
            Debug.WriteLine("IN CLOUD MOVEMENT, {0}, VELO: {1}, ACCEL {2}", onCloud, Velocity.Y, Acceleration.Y);
            //Velocity = new Vector2(Velocity.X, -50);
            //Acceleration = new Vector2(Acceleration.X, -125);
            Vector2 newLocation = new Vector2(0, -50) * (float)gametime.ElapsedGameTime.TotalSeconds;
            if (!CollisionHandler.GetInstance().IsGoingToBeOutOfBounds(this, newLocation))
            {
                Position += newLocation;
            }
            else
            {
                ExitCloud();
            }
        }

        public void StartClimbing()
        {
            sprite.EnterClimb();
            Velocity = new Vector2(0, -100);
        }

        public void EndClimbing()
        {
            sprite.ExitClimb();
            Velocity = new Vector2(0, 0);
            StandingTransition();
        }

        public void CheckVineTeleport()
        {
            if(Position.Y <= 50)
            {

            }
        }
        
        public void Update(GameTime gametime)
        {
            if (Velocity.Y == 0 && CollisionHandler.GetInstance().IsGoingToFall() && onCloud == false)
            {
                FallingTransition();
            }
            //Velocity += Acceleration * (float)gametime.ElapsedGameTime.TotalSeconds;
            Vector2 newLocation = Velocity * (float)gametime.ElapsedGameTime.TotalSeconds;
            if (!CollisionHandler.GetInstance().IsGoingToBeOutOfBounds(this, newLocation))
            {
                Position += newLocation;
            }


            if (onCloud) CloudMovement(gametime);
            if (!onCloud)
            {
                MarioPowerUpState.Update(gametime);// these
                MarioActionState.Update(gametime);// may be wrong for cloud mario
            }

            UpdateCollisionBox(Position, gametime);


            sprite.Update(gametime);

            if (invincibleTimer > 0)
            {
                IsInvincible = true;
                invincibleTimer = invincibleTimer - gametime.ElapsedGameTime.TotalSeconds;
            }
            else
            {

                IsInvincible = false;
                Debug.WriteLine("NOT invincibleTimer, {0}", invincibleTimer);
                Debug.WriteLine("NOT IsInvincible, {0}", IsInvincible);
            }
        }

        #region Mario States 
        public void Exit() { }

        public void StandingTransition()
        {
            MarioActionState.StandingTransition();
        }
        public void CrouchingTransition() { MarioActionState.CrouchingTransition(); }
        public void WalkingTransition() { MarioActionState.WalkingTransition(); }
        public void RunningTransition() { MarioActionState.RunningTransition(); } //Longer you hold running you increase velocity and speed of animation
        public void JumpingTransition() { MarioActionState.JumpingTransition(); }
        public void FallingTransition() { MarioActionState.FallingTransition(); }

        public void FaceLeftTransition()
        {
            if (onCloud)
            {
                if (Facing == MarioDirection.RIGHT)
                {
                    MarioActionState.StandingTransition();
                    MarioActionState.FaceLeftTransition();
                }
                Velocity = new Vector2(-100, Velocity.Y);
            }
            else
            {
                MarioActionState.FaceLeftTransition();
            }

                
        }
        public void FaceRightTransition()
        {
            if (onCloud)
            {
                if(Facing == MarioDirection.LEFT)
                {
                    MarioActionState.StandingTransition();
                    MarioActionState.FaceRightTransition();
                }
                Velocity = new Vector2(100, Velocity.Y);
            }
            else
            {
                MarioActionState.FaceRightTransition();
            }
               
        }

        public void UpTransition()
        {
            if (onCloud)
            { 
                if(Velocity.Y < 0)
                {
                    ExitCloud();
                }
                else
                {
                    Velocity = new Vector2(Velocity.X, Velocity.Y - 150);
                }
                
                
            }
            else
            {
                MarioActionState.UpTransition();
            }
            
        }

        public void ExitCloud()
        {
            onCloud = false;
            Acceleration = new Vector2(Acceleration.X, 600);
            MarioActionState.UpTransition();
        }

        public void LeapTransition()
        {
            MarioActionState.LeapTransition();
        }

        public void DownTransition()
        {
            if (onCloud)
            {
                Velocity = new Vector2(Velocity.X, Velocity.Y + 150);

            }
            else
            {
                MarioActionState.DownTransition();
            }
            
        }

        public void CrouchingDiscontinueTransition() { MarioActionState.CrouchingDiscontinueTransition(); }//when you exit crouch, release down key
        public void FaceLeftDiscontinueTransition() { MarioActionState.FaceLeftDiscontinueTransition(); }//generic entering walk and run, face left then start walking, then start running
        public void FaceRightDiscontinueTransition() { MarioActionState.FaceRightDiscontinueTransition(); }
        public void WalkingDiscontinueTransition() { MarioActionState.WalkingDiscontinueTransition(); }//decelerata and go to standing
        public void RunningDiscontinueTransition() { MarioActionState.RunningDiscontinueTransition(); }//decelerate and go to walking dis
        public void JumpingDiscontinueTransition() { MarioActionState.JumpingDiscontinueTransition(); }//abort jump or force jump to disc bc you reached apex of jump

        public void Enter(IMarioPowerUpStates previousPowerUpState) { }

        public void SmallMarioTransformation()
        {
            MarioPowerUpState.SmallMarioTransformation();
        }

        public void BigMarioTransformation()
        {
            MarioPowerUpState.BigMarioTransformation();
        }

        public void FireMarioTransformation()
        {
            MarioPowerUpState.FireMarioTransformation();
        }

        public void DeadTransition()
        {
            MarioPowerUpState.DeadTransition();
        }

        public void DamageTransition()
        {
            MarioPowerUpState.DamageTransition();
        }

        public void Trigger()
        {
            //Debug.WriteLine("invincibleTimer, {0}", invincibleTimer);
            //Debug.WriteLine("IsInvincible, {0}", IsInvincible);
            if (!IsDead && !(IsInvincible))
            {
                if (onCloud)
                {
                    //onCloud = false;
                    ExitCloud();
                }
                else
                {
                    MarioPowerUpState.DamageTransition();
                }
                
                invincibleTimer = 3;

            }
            else if(IsDead)
            {
                MarioPowerUpState.DeadTransition();
            }
        }

        #endregion

        public void UpdateCollisionBox(Vector2 location, GameTime gameTime)
        {

            if (!(MarioPowerUpState is States.BlockStates.DeadMarioState))
            {
                if (MarioPowerUpState is SmallMarioState)
                {
                    CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, 32, 32);
                }
                else if (MarioActionState is FireMarioCrouchingState || MarioActionState is BigMarioCrouchingState)
                {
                    CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, 32, 44);
                }

                else
                {
                    CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, 32, 64);
                }

                ExpandedCollisionBox = new Rectangle(CollisionBox.X, CollisionBox.Y, CollisionBox.Width, CollisionBox.Height + 5);
            }

            else
            {
                KillCollision();
            }
        }

        private void KillCollision()
        {
            CollisionBox = new Rectangle(0, 0, 0, 0);
        }

        public void HandleCollision(IGameObjects entity)
        {

            switch (entity.ObjectID)
            {
                case (int)ItemID.FIREFLOWER:
                    CollisionHandler.GetInstance().MarioToItemCollision((FireFlower)entity);
                    break;

                case (int)ItemID.SUPERSHROOM:
                    CollisionHandler.GetInstance().MarioToItemCollision((SuperShroom)entity);
                    break;
                case (int)ItemID.ONEUPSHROOM:
                    CollisionHandler.GetInstance().MarioToItemCollision((OneUpShroom)entity);
                    break;
                case (int)ItemID.COIN:
                    CollisionHandler.GetInstance().MarioToItemCollision((Coin)entity);
                    break;

                case (int)ItemID.HIDDENLEVELCOIN:
                    CollisionHandler.GetInstance().MarioToItemCollision((HiddenLevelCoin)entity);
                    break;

                case (int)ItemID.STAR:
                    CollisionHandler.GetInstance().MarioToItemCollision((Star)entity);
                    break;

                case (int)ItemID.CASTLE:
                    CollisionHandler.GetInstance().MarioToItemCollision((Castle)entity);
                    break;

                case (int)ItemID.FLAGPOLE:
                    CollisionHandler.GetInstance().MarioToItemCollision((FlagPole)entity);
                    break;
                case (int)ItemID.VINE:
                    CollisionHandler.GetInstance().ChangeMarioStatesUponCollision(entity);
                    CollisionHandler.GetInstance().MarioToItemCollision((Vine)entity);
                    break;
                case (int)BlockID.QUESTIONBLOCK:
                case (int)BlockID.BRICKBLOCK:
                case (int)BlockID.FLOORBLOCK:
                case (int)BlockID.STAIRBLOCK:
                case (int)BlockID.USEDBLOCK:
                case (int)BlockID.COINBRICKBLOCK:
                case (int)ItemID.BIGPIPE:
                case (int)ItemID.MEDIUMPIPE:
                case (int)ItemID.SMALLPIPE:
                case (int)ItemID.WARPPIPEHEAD:
                case (int)ItemID.WARPPIPEHEADWITHMOB:
                case (int)ItemID.WARPVINEWITHBLOCK:
                case (int)ItemID.WARPPIPEROOM:
                case (int)ItemID.WARPPIPEBODY:
                case (int)BlockID.VINEHIDDENBLOCK:
                case (int)BlockID.HIDDENLEVELFLOORBLOCK:
                case (int)BlockID.HIDDENLEVELBRICKBLOCK:
                    CollisionHandler.GetInstance().ChangeMarioStatesUponCollision(entity);
                    CollisionHandler.GetInstance().MarioToBlockCollision(entity);
                    break;

                case (int)BlockID.HIDDENBLOCK:
                    CollisionHandler.GetInstance().ChangeMarioStatesUponCollision(entity);
                    CollisionHandler.GetInstance().MarioToHiddenBlockCollision(entity);
                    break;

                case (int)EnemyID.GOOMBA:
                    CollisionHandler.GetInstance().ChangeMarioStatesUponCollision(entity);
                    if (!IsInvincible)CollisionHandler.GetInstance().MarioToEnemyCollision(entity);
                    break;
                case (int)EnemyID.GREENKOOPA:
                    CollisionHandler.GetInstance().ChangeMarioStatesUponCollision(entity);
                    if (!IsInvincible) CollisionHandler.GetInstance().MarioToEnemyCollision((GreenKoopa)entity);
                    break;
                case (int)EnemyID.SPINY:
                    CollisionHandler.GetInstance().ChangeMarioStatesUponCollision(entity);
                    CollisionHandler.GetInstance().MarioToEnemyCollision((SpinyRefactored)entity);
                    break;
                case (int)EnemyID.REDKOOPA:
                    CollisionHandler.GetInstance().ChangeMarioStatesUponCollision(entity);
                    if (!IsInvincible) CollisionHandler.GetInstance().MarioToEnemyCollision(entity);
                    break;

                case (int)EnemyID.UBERGOOMBA:
                    CollisionHandler.GetInstance().ChangeMarioStatesUponCollision(entity);
                    if (!IsInvincible) CollisionHandler.GetInstance().MarioToEnemyCollision(entity);
                    break;
            }
        }

        public void ToggleCollisionBoxes()
        {
            drawBox = !drawBox;
        }

        public bool ReturnDrawCollisionBoxes()
        {
            return drawBox;
        }

        public bool RevealItem()
        {
            return false;
        }
    }
}