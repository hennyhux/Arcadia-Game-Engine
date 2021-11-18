using GameSpace.EntityManaging;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.GameObjects.EnemyObjects;
using GameSpace.GameObjects.ExtraItemsObjects;
using GameSpace.GameObjects.ItemObjects;
using GameSpace.Interfaces;
using GameSpace.Level;
using GameSpace.Machines;
using GameSpace.Sprites;
using GameSpace.States.BlockStates;
using GameSpace.States.MarioStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

        public string Player { get; set; }

        public Mario(Vector2 initLocation)
        {
            ObjectID = (int)AvatarID.MARIO;
            drawBox = false;
            Position = new Vector2((int)initLocation.X, (int)initLocation.Y);
            numFireballs = 30;
            //marioLives = 3;
            Acceleration = new Vector2(0, 150);//NEW
            sprite = MarioFactory.GetInstance().CreateSprite(1);
            MarioPowerUpState = new SmallMarioState(this);
            MarioActionState = new SmallMarioStandingState(this);
            numCoinsCollected = 0;
            score = 0;
            Player = "Mario";
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

            sprite.Draw(spritebatch, Position);
            if (drawBox)
            {
                sprite.DrawBoundary(spritebatch, CollisionBox);
            }
        }
        public void Update(GameTime gametime)
        {
            if (Velocity.Y == 0 && CollisionHandler.GetInstance().IsGoingToFall())
            {
                FallingTransition();
            }

            Vector2 newLocation = Velocity * (float)gametime.ElapsedGameTime.TotalSeconds;
            if (!CollisionHandler.GetInstance().IsGoingToBeOutOfBounds(this, newLocation))
            {
                Position += newLocation;
            }

            UpdatePosition(Position, gametime);
            MarioPowerUpState.Update(gametime);
            MarioActionState.Update(gametime);
            sprite.Update(gametime);
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
            MarioActionState.FaceLeftTransition();
        }
        public void FaceRightTransition()
        {
            MarioActionState.FaceRightTransition();
        }

        public void UpTransition()
        {
            MarioActionState.UpTransition();
        }

        public void LeapTransition()
        {
            MarioActionState.LeapTransition();
        }

        public void DownTransition()
        {
            MarioActionState.DownTransition();
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
            DamageTransition();
        }

        #endregion

        public void UpdatePosition(Vector2 location, GameTime gameTime)
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
                CollisionBox = new Rectangle(0, 0, 0, 0);
                ExpandedCollisionBox = new Rectangle(0, 0, 0, 0);
            }
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
                case (int)ItemID.WARPPIPEROOM:
                case (int)ItemID.WARPPIPEBODY:
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
                    CollisionHandler.GetInstance().MarioToEnemyCollision(entity);
                    break;
                case (int)EnemyID.GREENKOOPA:
                    CollisionHandler.GetInstance().ChangeMarioStatesUponCollision(entity);
                    CollisionHandler.GetInstance().MarioToEnemyCollision((GreenKoopa)entity);
                    break;
                case (int)EnemyID.REDKOOPA:
                    CollisionHandler.GetInstance().ChangeMarioStatesUponCollision(entity);
                    CollisionHandler.GetInstance().MarioToEnemyCollision(entity);
                    break;
            }
        }

        public void StopAllMotion()
        {
            Velocity = new Vector2(0, 0);
            Acceleration = new Vector2(0, 0);
        }

        public void WarpMario()
        {
            IGameObjects[] NextPipe = FinderHandler.GetInstance().FindWarpPipes();
            Position = NextPipe[1].Position; // remove hardcode 
        }

        public void ToggleCollisionBoxes()
        {
            drawBox = !drawBox;
        }

        public bool IsCurrentlyColliding()
        {
            return false;
        }

        public bool RevealItem()
        {
            throw new System.NotImplementedException();
        }
    }
}