using GameSpace.Factories;
using GameSpace.Interfaces;
using GameSpace.States.BlockStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using GameSpace.Sprites;
using System.Diagnostics;
using GameSpace.States.MarioStates;
using GameSpace.Enums;
using GameSpace.EntitiesManager;

namespace GameSpace.GameObjects.BlockObjects
{
    public class Mario : IGameObjects
    {

        //actionStateMachine
        //powerupStateMachine
        //
        //public Texture2D Texture { get; set; }
        private int x;
        private int y;
        public static int X { get; set; }
        public static int Y { get; set; }
        private Boolean hasCollided;
        private Boolean drawBox;


        //private int actionState; //[Idling, Crouching, Walking, Running, Jumping, Falling, Dying]
        //private int marioPower;// [Small, Big, Fire, Star, Dead]
        // private int facingRight;// left = 0, right = 1

        //private static MarioPowerUpStates actionState;


        //private MarioSprite sprite;
        public MarioSprite sprite { get; set; }
        public eFacing Facing { get; set; }
        //private IMarioActionState marioPowerUpState;
        // private MarioPowerUpStates marioPowerUpState;

        public IMarioPowerUpStates marioPowerUpState { get; set; }

        public IMarioActionStates marioActionState { get; set; }
        public ISprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public Rectangle CollisionBox { get; set; }
        public int ObjectID { get; set; }

        //public IMarioPowerUpStates marioPowerUpState { get; set; }

        //private MarioPowerUpStates previousMarioActionState { get; set; }
        // private MarioPowerUpStates previousMarioPowerUpState;



        //public Mario(Game1 game, Texture2D texture)
        public Mario(Vector2 initLocation)
        {
            Debug.WriteLine("Mario.cs(50) CREATED MARIO \n");
            this.ObjectID = (int)AvatarID.MARIO;
            drawBox = false;
            this.Position = new Vector2((int)initLocation.X, (int)initLocation.Y);
            this.CollisionBox = new Rectangle((int)initLocation.X - 3, (int)initLocation.Y, 32, 32);

            // this.state = new MarioStates(game);
            // this.sprite = MarioFactory.GetInstance().ReturnMarioS1tandingLeftSprite();
            //this.marioPowerUpState = new SmallMarioState(this);
            int big = 0;
            if (big == 0)
            {
                this.sprite = MarioFactory.GetInstance().CreateSprite(1);
                //this.Sprite = MarioFactory.GetInstance().CreateSprite(1); 
                this.marioPowerUpState = new SmallMarioState(this);
                this.marioActionState = new SmallMarioStandingState(this);
            }
            /*else if(big == 1)
             {
                 this.sprite = MarioFactory.GetInstance().CreateSprite(16);
                 this.marioPowerUpState = new BigMarioState(this);
                 this.marioActionState = new BigMarioStandingState(this);

             }
             else
             {

                 this.sprite = MarioFactory.GetInstance().CreateSprite(32);
                 this.marioPowerUpState = new FireMarioState(this);
                 this.marioActionState = new FireMarioStandingState(this);
                 ///*////this.marioPowerUpState = new FireMarioState(this);
            // this.marioActionState = new FireMarioStandingState(this);
            // this.sprite = MarioFactory.GetInstance().CreateSprite(MarioFactory.MarioSpriteType(marioActionState, this.marioPowerUpState));
            //this.sprite = MarioFactory.GetInstance().CreateSprite(10);*/*/
            //}

        }

        public void Draw(SpriteBatch spritebatch)
        {
            if (drawBox) sprite.DrawBoundary(spritebatch, CollisionBox);
            if (Facing == eFacing.LEFT)
                //sprite.facingRight = 0;
                sprite.facing = SpriteEffects.None;// swap if
            else
                //sprite.facingRight = 0;
                sprite.facing = SpriteEffects.FlipHorizontally;// swap if base facing direction of sprite is right
            this.sprite.Draw(spritebatch, Position);
        }
        public void Update(GameTime gametime)
        {
            Vector2 newLocation = Velocity * (float)gametime.ElapsedGameTime.TotalSeconds;
            if (!IsGoingToBeOutOfBounds(newLocation)) Position += newLocation;
            this.CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, CollisionBox.Width, CollisionBox.Height);
            this.marioPowerUpState.Update(gametime);
            this.marioActionState.Update(gametime);
            this.sprite.Update(gametime);

        }

        public void Exit() { }

        public void StandingTransition()
        {
            //Debug.WriteLine("MARIO STAND");
            // Debug.WriteLine("Super Stand Trans");
            marioActionState.StandingTransition();
        }
        public void CrouchingTransition() { marioActionState.CrouchingTransition(); }
        public void WalkingTransition() { marioActionState.WalkingTransition(); }
        public void RunningTransition() { marioActionState.RunningTransition(); } //Longer you hold running you increase velocity and speed of animation
        public void JumpingTransition() { marioActionState.JumpingTransition(); }
        public void FallingTransition() { marioActionState.FallingTransition(); }

        public void FaceLeftTransition() {
            if (this.Facing == eFacing.RIGHT)
            {
                //this.Velocity = new Vector2((float)0, (float)0);//
            }

                marioActionState.FaceLeftTransition(); }
        public void FaceRightTransition() 
        {
            if (this.Facing == eFacing.LEFT)
            {
                //this.Velocity = new Vector2((float)0, (float)0);//
            }
            marioActionState.FaceRightTransition(); }

        public void UpTransition()
        {
            marioActionState.UpTransition();
        }
    
        public void DownTransition()
        {
            marioActionState.DownTransition();
        }

        public void CrouchingDiscontinueTransition() { marioActionState.CrouchingDiscontinueTransition(); }//when you exit crouch, release down key
        public void FaceLeftDiscontinueTransition() { marioActionState.FaceLeftDiscontinueTransition(); }//generic entering walk and run, face left then start walking, then start running
        public void FaceRightDiscontinueTransition() { marioActionState.FaceRightDiscontinueTransition(); }
        public void WalkingDiscontinueTransition() { marioActionState.WalkingDiscontinueTransition(); }//decelerata and go to standing
        public void RunningDiscontinueTransition() { marioActionState.RunningDiscontinueTransition(); }//decelerate and go to walking dis
        public void JumpingDiscontinueTransition() { marioActionState.JumpingDiscontinueTransition(); }//abort jump or force jump to disc bc you reached apex of jump

        public void Enter(IMarioPowerUpStates previousPowerUpState) { }
        //ublic  void Exit() { }

        public void smallMarioTransformation() 
        {
            marioPowerUpState.smallMarioTransformation();
            //this.Position = new Vector2((int)Position.X, (int)Position.Y - sprite.Height);
            this.CollisionBox = new Rectangle((int)(Position.X + sprite.Texture.Width / 16), (int)Position.Y, sprite.Texture.Width / 12, sprite.Texture.Height / 6);
        }

        public void bigMarioTransformation()
        {
            this.CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, 32, 64);
            marioPowerUpState.bigMarioTransformation();
            //this.Position = new Vector2((int)Position.X, (int)Position.Y + sprite.Height);
        }

        public void fireMarioTransformation() 
        { 
            marioPowerUpState.fireMarioTransformation();
            //this.Position = new Vector2((int)Position.X, (int)Position.Y + sprite.Height);
            this.CollisionBox = new Rectangle((int)Position.X, (int)Position.Y + 32, 32, 64);
        }

        public void DeadTransition() { marioPowerUpState.DeadTransition(); }

        public void Trigger()
        {
            
        }

        //unused testing method 
        public void SetPosition(Vector2 location)
        {

            if (EntityManager.IsCurrentlyBigMario())
            {
                this.CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, 32, 64);
            }

            else
            {
                this.CollisionBox = new Rectangle((int)(Position.X + sprite.Texture.Width / 16), (int)Position.Y, sprite.Texture.Width / 12, sprite.Texture.Height / 6);
            }
                       
        }

        private void VisualCue()
        {
            
        }

        //BRICKBLOCK = 0,
        //QUESTIONBLOCK = 1,
        //FLOORBLOCK = 2,
        //HIDDENBLOCK = 3,
        //STAIRBLOCK = 4,
        //USEDBLOCK = 5,
        public void HandleCollision(IGameObjects entity)
        {
            switch (entity.ObjectID)
            {
                case (int)ItemID.FIREFLOWER:
                    CollisionWithFireFlower(entity);
                    break;

                case (int)ItemID.SUPERSHROOM:
                    CollisionWithSuperShroom(entity);
                    break;

                case (int)BlockID.QUESTIONBLOCK:
                    CollisionWithBumpBlock(entity);
                    break;

                case (int)BlockID.BRICKBLOCK:
                    CollisionWithBrickBlock(entity);
                    break;

                case (int)BlockID.FLOORBLOCK:
                    CollisionWithFloorBlock(entity);
                    break;

                case (int)BlockID.HIDDENBLOCK:
                    CollisionWithHiddenBlock(entity);
                    break;

                case (int)BlockID.STAIRBLOCK:
                    CollisionWithBumpBlock(entity);
                    break;

                case (int)BlockID.USEDBLOCK:
                    CollisionWithUsedBlock(entity);
                    break;

                //All enemy encounters use same method. 
                case (int)EnemyID.GOOMBA:
                case (int)EnemyID.GREENKOOPA:
                case (int)EnemyID.REDKOOPA:
                    CollisionWithEnemy(entity);
                    break;
            } 
        }

        private bool IsGoingToBeOutOfBounds(Vector2 newLocation)
        {
            if (Position.X + newLocation.X <= 0) return true;
            if (Position.X + (CollisionBox.Width) + newLocation.X > 800) return true;
            if (Position.Y + newLocation.Y <= 0) return true;
            if (Position.Y + newLocation.Y >= 450) return true;
            return false;
        }

        private void changeStateUponCollision(IGameObjects entity)
        {
            if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.LEFT) { StandingTransition(); }

            else if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.RIGHT) { StandingTransition(); }

            else if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.UP) { FallingTransition(); }

            else if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.DOWN) { StandingTransition(); }
        }


        private void CollisionWithFloorBlock(IGameObjects entity)
        {
            //if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.UP) { this.SetPosition(new Vector2(this.Position.X * 0, this.Position.Y * 0)); }
            if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.UP) { this.Position = new Vector2(this.Position.X, (int)entity.Position.Y + (int)entity.CollisionBox.Height); }

            else if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.DOWN) { this.Position = new Vector2(this.Position.X, (int)entity.Position.Y - (int)this.CollisionBox.Height); }

            changeStateUponCollision(entity);//Change state upon collision

            this.CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, CollisionBox.Width, CollisionBox.Height);

            //changeStateUponCollision(entity);//Change state upon collision
        }

        private void CollisionWithEnemy(IGameObjects enemy)
        {
            if (EntityManager.DetectCollisionDirection(this, enemy) == (int)CollisionDirection.LEFT ||
                EntityManager.DetectCollisionDirection(this, enemy) == (int)CollisionDirection.RIGHT || 
                EntityManager.DetectCollisionDirection(this, enemy) == (int)CollisionDirection.DOWN)
            {
                this.DeadTransition();

                if (EntityManager.DetectCollisionDirection(this, enemy) == (int)CollisionDirection.LEFT) { this.Position = new Vector2((int)enemy.Position.X - (int)this.CollisionBox.Width, (int)this.Position.Y); }

                else if (EntityManager.DetectCollisionDirection(this, enemy) == (int)CollisionDirection.RIGHT) { this.Position = new Vector2((int)enemy.Position.X + (int)enemy.CollisionBox.Width, (int)this.Position.Y); }

                else if (EntityManager.DetectCollisionDirection(this, enemy) == (int)CollisionDirection.DOWN) { this.Position = new Vector2(this.Position.X, (int)enemy.Position.Y - (int)this.CollisionBox.Height); }
                
                changeStateUponCollision(enemy);
                this.CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, CollisionBox.Width, CollisionBox.Height);
            }

            else
            {
                MoveObjectOffset(0, 10);//perform bounce?
                //PreformBounce(0, 10);
                changeStateUponCollision(enemy);

            }
        }

        private void StopAnyMotion()
        {
            this.Velocity = new Vector2(0, 0);
        }

        private void CollisionWithBumpBlock(IGameObjects entity)
        {

            if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.LEFT) { this.Position = new Vector2((int)entity.Position.X - (int)this.CollisionBox.Width, (int)this.Position.Y); }

            else if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.RIGHT) { this.Position = new Vector2((int)entity.Position.X + (int)entity.CollisionBox.Width, (int)this.Position.Y); }

            else if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.UP) 
            {
                this.Velocity = new Vector2(this.Velocity.X, this.Velocity.Y - this.Velocity.Y);
                this.FallingTransition(); // broken 
            }

            else if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.DOWN) 
            {
                this.Position = new Vector2(this.Position.X, (int)entity.Position.Y - (int)this.CollisionBox.Height);
            }

            

            changeStateUponCollision(entity);//Change state upon collision
            this.CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, CollisionBox.Width, CollisionBox.Height);
        }

        private void CollisionWithBrickBlock(IGameObjects entity)
        {

            if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.LEFT) { this.Position = new Vector2((int)entity.Position.X - (int)this.CollisionBox.Width, (int)this.Position.Y); }

            else if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.RIGHT) { this.Position = new Vector2((int)entity.Position.X + (int)entity.CollisionBox.Width, (int)this.Position.Y); }

            else if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.UP)
            {
                this.Velocity = new Vector2(this.Velocity.X, this.Velocity.Y - this.Velocity.Y);
                this.FallingTransition(); // broken 

            }

            else if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.DOWN)
            {
                this.Position = new Vector2(this.Position.X, (int)entity.Position.Y - (int)this.CollisionBox.Height);
            }



            changeStateUponCollision(entity);//Change state upon collision
            this.CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, CollisionBox.Width, CollisionBox.Height);
        }

        private void CollisionWithUsedBlock(IGameObjects entity)
        {

            if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.LEFT) { this.Position = new Vector2((int)entity.Position.X - (int)this.CollisionBox.Width, (int)this.Position.Y); }

            else if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.RIGHT) { this.Position = new Vector2((int)entity.Position.X + (int)entity.CollisionBox.Width, (int)this.Position.Y); }

            else if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.UP) { this.Position = new Vector2(this.Position.X, (int)entity.Position.Y + (int)entity.CollisionBox.Height); }

            else if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.DOWN) { this.Position = new Vector2(this.Position.X, (int)entity.Position.Y - (int)this.CollisionBox.Height); }
            

            changeStateUponCollision(entity);//Change state upon collision
            this.CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, CollisionBox.Width, CollisionBox.Height);

        }

        private void CollisionWithHiddenBlock(IGameObjects entity)
        {
            HiddenBlock hBlock = (HiddenBlock)entity;
            if(hBlock.hasCollided == false)
            {
                //if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.UP) { this.Position = new Vector2(this.Position.X, (int)entity.Position.Y + (int)entity.CollisionBox.Height); FallingTransition(); }
                
            }
            else if(hBlock.hasCollided == true)
            {
                if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.LEFT) { this.Position = new Vector2((int)entity.Position.X - (int)this.CollisionBox.Width, (int)this.Position.Y); }

                else if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.RIGHT) 
                {
                    this.Velocity = new Vector2(this.Velocity.X, this.Velocity.Y - this.Velocity.Y);

                }

                else if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.UP) {

                    this.Velocity = new Vector2(this.Velocity.X, this.Velocity.Y - this.Velocity.Y); 
                    this.Position = new Vector2(this.Position.X, (int)entity.Position.Y + (int)entity.CollisionBox.Height); 
                
                }

                else if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.DOWN) { this.Position = new Vector2(this.Position.X, (int)entity.Position.Y - (int)this.CollisionBox.Height); }
                changeStateUponCollision(entity);
            }




                this.CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, CollisionBox.Width, CollisionBox.Height);

        }

        private void CollisionWithFireFlower(IGameObjects entity)
        {
            //Direction doesn't matter for FireFlower Collision, going to change Power-Up either way
            this.fireMarioTransformation();
        }

        private void CollisionWithSuperShroom(IGameObjects entity)
        {
            //Direction doesn't matter for SUPERSHROOM Collision, going to change Power-Up either way
            this.bigMarioTransformation();
        }

        private void MoveObjectOffset(int offsetX, int offsetY)
        {
            //this.CollisionBox = new Rectangle((int)(Position.X - offsetX) + Sprite.Texture.Width / 2,
            //(int)(Position.Y - offsetY), Sprite.Texture.Width / 12, Sprite.Texture.Height / 6);
            //this.CollisionBox = new Rectangle((int)(CollisionBox.X - offsetX), (int)CollisionBox.Y - offsetY, CollisionBox.Width, CollisionBox.Height);
            //(int)(Position.Y - offsetY), Sprite.Texture.Width / 12, Sprite.Texture.Height / 6);
            this.Position = new Vector2((int)(Position.X - offsetX), (int)(Position.Y - offsetY));
            this.CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, CollisionBox.Width, CollisionBox.Height);
        }

        private void PreformBounce(int offsetX, int offsetY)
        {
            this.Position = new Vector2((int)(Position.X - offsetX), (int)(Position.Y - offsetY));
            this.CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, CollisionBox.Width, CollisionBox.Height);
        }

        public void ToggleCollisionBoxes()
        {
            drawBox = !drawBox;
        }
    }
}