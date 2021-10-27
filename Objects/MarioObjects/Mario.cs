using GameSpace.Factories;
using GameSpace.Interfaces;
using GameSpace.States.BlockStates;
using GameSpace.States.ItemStates;
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
using GameSpace.GameObjects.EnemyObjects;
using GameSpace.States.EnemyStates;


namespace GameSpace.GameObjects.BlockObjects
{
    public class Mario : IGameObjects
    {

        private Boolean hasCollided;
        private Boolean drawBox;

        public MarioSprite sprite { get; set; }
        public eFacing Facing { get; set; }
        public IMarioPowerUpStates marioPowerUpState { get; set; }

        public IMarioActionStates marioActionState { get; set; }
        public ISprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public Rectangle CollisionBox { get; set; }
        public int ObjectID { get; set; }
        public int numFireballs;

        public int marioLives { get; set; }

        public Mario(Vector2 initLocation)
        {
            Debug.WriteLine("Mario.cs(50) CREATED MARIO \n");
            this.ObjectID = (int)AvatarID.MARIO;
            drawBox = false;
            hasCollided = false;
            this.Position = new Vector2((int)initLocation.X, (int)initLocation.Y);
            this.CollisionBox = new Rectangle((int)initLocation.X - 3, (int)initLocation.Y, 32, 32);
            this.numFireballs = 0;
            this.marioLives = 3;
            this.Acceleration = new Vector2(0, 100);//NEW


            this.sprite = MarioFactory.GetInstance().CreateSprite(1);
            this.marioPowerUpState = new SmallMarioState(this);
            this.marioActionState = new SmallMarioStandingState(this);
            
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
            //Debug.WriteLine("Mario velocity, {0}", Velocity.Y);
            //Velocity += Acceleration * (float)gametime.ElapsedGameTime.TotalSeconds;
            Vector2 newLocation = Velocity * (float)gametime.ElapsedGameTime.TotalSeconds;
            if (!IsGoingToBeOutOfBounds(newLocation))
            {
                
                Position += newLocation;
            }
            //this.CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, CollisionBox.Width, CollisionBox.Height);
            if(!(marioPowerUpState is GameSpace.States.BlockStates.DeadMarioState)){
                if (marioPowerUpState is SmallMarioState)
                {
                    this.CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, 32, 32);
                }
                else if(marioActionState is FireMarioCrouchingState || marioActionState is BigMarioCrouchingState)
                {
                    this.CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, 32, 44);
                }
                else
                {
                    //this.CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, this.sprite.Width * 2, this.sprite.Height * 2);
                    this.CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, 32, 64);
                }
            }
           
            
            //GetMario.sprite.Height
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

        public void DeadTransition() 
        {
            this.CollisionBox = new Rectangle(0, 0, 0, 0);
            marioPowerUpState.DeadTransition();
        }

        public void Trigger()
        {
            
        }

        //unused testing method 
        public void UpdatePosition(Vector2 location, GameTime gameTime)
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


        //BRICKBLOCK = 0,
        //QUESTIONBLOCK = 1,
        //FLOORBLOCK = 2,
        //HIDDENBLOCK = 3,
        //STAIRBLOCK = 4,
        //USEDBLOCK = 5,
        public void HandleCollision(IGameObjects entity)
        {
            hasCollided = true;
            switch (entity.ObjectID)
            {
                case (int)ItemID.FIREFLOWER:
                    CollisionWithFireFlower(entity);
                    break;

                case (int)ItemID.SUPERSHROOM:
                    CollisionWithSuperShroom(entity);
                    break;

                case (int)BlockID.QUESTIONBLOCK:
                case (int)BlockID.BRICKBLOCK:
                case (int)BlockID.FLOORBLOCK:
                case (int)BlockID.STAIRBLOCK:
                case (int)BlockID.USEDBLOCK:
                case (int)BlockID.COINBRICKBLOCK:
                    CollisionWithBlock(entity);
                    break;

                case (int)BlockID.HIDDENBLOCK:
                    CollisionWithHiddenBlock(entity);
                    break;

                case (int)EnemyID.GOOMBA:
                    CollisionWithGoomba(entity);
                    break;
                case (int)EnemyID.GREENKOOPA:
                    CollisionWithGreenKoopa(entity);
                    break;
                case (int)EnemyID.REDKOOPA:
                    CollisionWithRedKoopa(entity);
                    break;

                case (int)ItemID.FIREBALL:
                    CollisionWithFireball(entity);
                    break;
            }

            
            
        }

        private bool IsGoingToBeOutOfBounds(Vector2 newLocation)
        {
            if (Position.X + newLocation.X <= 0) { marioActionState.StandingTransition(); return true; }
            if (Position.X + (CollisionBox.Width) + newLocation.X > ((Rectangle)EntityManager.Camera.Limits).Width) { marioActionState.StandingTransition(); return true; }//should be max X value of level
            if (Position.Y + newLocation.Y <= 0) {this.Velocity = new Vector2(this.Velocity.X, 50); marioActionState.FallingTransition(); return true; }
           
            if (Position.Y + newLocation.Y + CollisionBox.Height >= ((Rectangle)EntityManager.Camera.Limits).Height) { marioPowerUpState.DeadTransition();  return true; } 
            return false;
        }

        private void changeStateUponCollision(IGameObjects entity)
        {
            if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.LEFT) { StandingTransition(); }

            else if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.RIGHT) { StandingTransition(); }

            else if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.UP) { FallingTransition(); }

            else if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.DOWN) 
            { 
                if(marioActionState is SmallMarioFallingState || marioActionState is BigMarioFallingState || marioActionState is FireMarioFallingState)
                {
                    DownTransition();
                    
                }
                //StopAnyMotion(); 
            }
        }


        private void CollisionWithGoomba(IGameObjects enemy)
        {
            if (EntityManager.DetectCollisionDirection(this, enemy) == (int)CollisionDirection.LEFT ||
                EntityManager.DetectCollisionDirection(this, enemy) == (int)CollisionDirection.RIGHT || 
                EntityManager.DetectCollisionDirection(this, enemy) == (int)CollisionDirection.DOWN)
            {
                this.CollisionBox = new Rectangle(1, 1, 0, 0);
                this.DeadTransition();
                this.CollisionBox = new Rectangle(1, 1, 0, 0);

                if (EntityManager.DetectCollisionDirection(this, enemy) == (int)CollisionDirection.LEFT) { this.Position = new Vector2((int)enemy.Position.X - (int)this.CollisionBox.Width - 5, (int)this.Position.Y); }

                else if (EntityManager.DetectCollisionDirection(this, enemy) == (int)CollisionDirection.RIGHT) { this.Position = new Vector2((int)enemy.Position.X + (int)enemy.CollisionBox.Width + 5, (int)this.Position.Y); }

                else if (EntityManager.DetectCollisionDirection(this, enemy) == (int)CollisionDirection.DOWN) { this.Position = new Vector2(this.Position.X, (int)enemy.Position.Y - (int)this.CollisionBox.Height); }
                
                changeStateUponCollision(enemy);
                this.CollisionBox = new Rectangle(1, 1, 0, 0);

                hasCollided = false; 
            }

            else
            {
                PrefromBounce(0, 10);
                StandingTransition();
            }
        }

        private void CollisionWithRedKoopa(IGameObjects enemy)
        {
            RedKoopa redKoopa = (RedKoopa)enemy;
            
            if(redKoopa.state is StateRedKoopaDead)
            {
                if (EntityManager.DetectCollisionDirection(this, enemy) == (int)CollisionDirection.LEFT ||
                    EntityManager.DetectCollisionDirection(this, enemy) == (int)CollisionDirection.RIGHT ||
                    EntityManager.DetectCollisionDirection(this, enemy) == (int)CollisionDirection.DOWN)
                {
                    StandingTransition();
                    PrefromBounce(0, 10);
                }
            } 
            else
            {

                this.CollisionBox = new Rectangle(1, 1, 0, 0);
                this.DeadTransition();
                this.CollisionBox = new Rectangle(1, 1, 0, 0);

                if (EntityManager.DetectCollisionDirection(this, enemy) == (int)CollisionDirection.LEFT) { this.Position = new Vector2((int)enemy.Position.X - (int)this.CollisionBox.Width - 5, (int)this.Position.Y); }

                else if (EntityManager.DetectCollisionDirection(this, enemy) == (int)CollisionDirection.RIGHT) { this.Position = new Vector2((int)enemy.Position.X + (int)enemy.CollisionBox.Width + 5, (int)this.Position.Y); }

                else if (EntityManager.DetectCollisionDirection(this, enemy) == (int)CollisionDirection.DOWN) { this.Position = new Vector2(this.Position.X, (int)enemy.Position.Y - (int)this.CollisionBox.Height); }

                changeStateUponCollision(enemy);
                this.CollisionBox = new Rectangle(1, 1, 0, 0);

                hasCollided = false;
            }
        }

        private void CollisionWithGreenKoopa(IGameObjects enemy)
        {
            GreenKoopa redKoopa = (GreenKoopa)enemy;

            if (redKoopa.state is StateGreenKoopaDead)
            {
                if (EntityManager.DetectCollisionDirection(this, enemy) == (int)CollisionDirection.LEFT ||
                    EntityManager.DetectCollisionDirection(this, enemy) == (int)CollisionDirection.RIGHT ||
                    EntityManager.DetectCollisionDirection(this, enemy) == (int)CollisionDirection.DOWN)
                {
                    StandingTransition();
                    PrefromBounce(0, 10);
                }
            }
            else
            {

                this.CollisionBox = new Rectangle(1, 1, 0, 0);
                this.DeadTransition();
                this.CollisionBox = new Rectangle(1, 1, 0, 0);

                if (EntityManager.DetectCollisionDirection(this, enemy) == (int)CollisionDirection.LEFT) { this.Position = new Vector2((int)enemy.Position.X - (int)this.CollisionBox.Width - 5, (int)this.Position.Y); }

                else if (EntityManager.DetectCollisionDirection(this, enemy) == (int)CollisionDirection.RIGHT) { this.Position = new Vector2((int)enemy.Position.X + (int)enemy.CollisionBox.Width + 5, (int)this.Position.Y); }

                else if (EntityManager.DetectCollisionDirection(this, enemy) == (int)CollisionDirection.DOWN) { this.Position = new Vector2(this.Position.X, (int)enemy.Position.Y - (int)this.CollisionBox.Height); }

                changeStateUponCollision(enemy);
                this.CollisionBox = new Rectangle(1, 1, 0, 0);

                hasCollided = false;
            }
        }

        private void CollisionWithFireball(IGameObjects enemy)
        {
            this.CollisionBox = new Rectangle(1, 1, 0, 0);
            this.DeadTransition();
            this.CollisionBox = new Rectangle(1, 1, 0, 0);

            if (EntityManager.DetectCollisionDirection(this, enemy) == (int)CollisionDirection.LEFT) { this.Position = new Vector2((int)enemy.Position.X - (int)this.CollisionBox.Width - 5, (int)this.Position.Y); }

            else if (EntityManager.DetectCollisionDirection(this, enemy) == (int)CollisionDirection.RIGHT) { this.Position = new Vector2((int)enemy.Position.X + (int)enemy.CollisionBox.Width + 5, (int)this.Position.Y); }

            else if (EntityManager.DetectCollisionDirection(this, enemy) == (int)CollisionDirection.DOWN) { this.Position = new Vector2(this.Position.X, (int)enemy.Position.Y - (int)this.CollisionBox.Height); }

            changeStateUponCollision(enemy);
            this.CollisionBox = new Rectangle(1, 1, 0, 0);

            hasCollided = false;
        }

        private void StopAnyMotion()
        {
            this.Velocity = new Vector2(0, 0);
        }

        private void CollisionWithBlock(IGameObjects entity)
        {

            if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.LEFT) 
            {
                this.Position = new Vector2((int)entity.Position.X - (int)this.CollisionBox.Width, (int)this.Position.Y); 
            }

            else if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.RIGHT) 
            { 
                this.Position = new Vector2((int)entity.Position.X + (int)entity.CollisionBox.Width, (int)this.Position.Y); 
            }

            else if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.UP) 
            {
                //this.Position = new Vector2(this.Position.X, (int)entity.Position.Y + (int)entity.CollisionBox.Height);
                this.Velocity = new Vector2(this.Velocity.X, 50);
            }

            else if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.DOWN) 
            {
               
                this.Position = new Vector2(this.Position.X, (int)entity.Position.Y - (int)this.CollisionBox.Height);
            }

            changeStateUponCollision(entity);//Change state upon collision
            this.CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, CollisionBox.Width, CollisionBox.Height);
        }

        private void CollisionWithHiddenBlock(IGameObjects entity)
        {
            HiddenBlock hBlock = (HiddenBlock)entity;
            if (hBlock.hasCollided)
            {
                if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.LEFT) 
                {        
                    this.Position = new Vector2((int)entity.Position.X - (int)this.CollisionBox.Width, (int)this.Position.Y); 
                }

                else if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.RIGHT)
                {
                    this.Position = new Vector2((int)entity.Position.X + (int)entity.CollisionBox.Width, (int)this.Position.Y);
                }

                else if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.UP)
                {
                    this.Velocity = new Vector2(this.Velocity.X, 50);
                    this.Position = new Vector2(this.Position.X, (int)entity.Position.Y + (int)entity.CollisionBox.Height);
                }

                else if (EntityManager.DetectCollisionDirection(this, entity) == (int)CollisionDirection.DOWN)
                
                { 
                    this.Position = new Vector2(this.Position.X, (int)entity.Position.Y - (int)this.CollisionBox.Height); 
                }

                changeStateUponCollision(entity);
            }

            this.CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, CollisionBox.Width, CollisionBox.Height);
        }

        private void CollisionWithFireFlower(IGameObjects entity)
        {
            //Direction doesn't matter for FireFlower Collision, going to change Power-Up either way
            if (marioPowerUpState is SmallMarioState)
            {
                this.bigMarioTransformation();
            }
            else if (marioPowerUpState is BigMarioState)
            {
                this.fireMarioTransformation();
            }
            
        }

        private void CollisionWithSuperShroom(IGameObjects entity)
        {
            //Direction doesn't matter for SUPERSHROOM Collision, going to change Power-Up either way
            if(!(marioPowerUpState is FireMarioState))
            {
                this.bigMarioTransformation();
            }
            
        }

        public void PrefromBounce(int offsetX, int offsetY)
        {
            this.Position = new Vector2((int)(Position.X - offsetX), (int)(Position.Y - offsetY));
            this.CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, CollisionBox.Width, CollisionBox.Height);
        }

        public void ToggleCollisionBoxes()
        {
            drawBox = !drawBox;
        }

        public bool IsCurrentlyColliding()
        {
            return hasCollided;
        }

        
    }
}