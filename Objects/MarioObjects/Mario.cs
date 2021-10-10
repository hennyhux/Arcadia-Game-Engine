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

        public IMarioPowerUpStates marioPowerUpState { get ; set; }

        public IMarioActionStates marioActionState { get; set; }
        public ISprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public Rectangle CollisionBox { get; set; }

        public int ObjectID => throw new NotImplementedException();

        //public IMarioPowerUpStates marioPowerUpState { get; set; }

        //private MarioPowerUpStates previousMarioActionState { get; set; }
        // private MarioPowerUpStates previousMarioPowerUpState;



        //public Mario(Game1 game, Texture2D texture)
        public Mario(Vector2 initLocation)
        {
            Debug.WriteLine("Mario.cs(50) CREATED MARIO \n");
            drawBox = false;
            this.Position = new Vector2((int)initLocation.X, (int)initLocation.Y);
            this.CollisionBox = new Rectangle((int)initLocation.X - 3, (int)initLocation.Y, 32, 32);
            drawBox = false;
            // this.state = new MarioStates(game);
            // this.sprite = MarioFactory.GetInstance().ReturnMarioS1tandingLeftSprite();
            //this.marioPowerUpState = new SmallMarioState(this);
            int big =0;
           if(big == 0)
            {
                this.sprite = MarioFactory.GetInstance().CreateSprite(1);
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
            this.CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, CollisionBox.Width, CollisionBox.Height);
            this.marioPowerUpState.Update(gametime);
            this.marioActionState.Update(gametime);
            this.sprite.Update(gametime);

        }

        public  void Exit() { }

        public  void StandingTransition()
        {
            //Debug.WriteLine("MARIO STAND");
            // Debug.WriteLine("Super Stand Trans");
            marioActionState.StandingTransition();
        }
        public  void CrouchingTransition() { marioActionState.CrouchingTransition();  }
        public  void WalkingTransition() { marioActionState.WalkingTransition(); }
        public  void RunningTransition() { marioActionState.RunningTransition(); } //Longer you hold running you increase velocity and speed of animation
        public  void JumpingTransition() { marioActionState.JumpingTransition(); }
        public  void FallingTransition() { marioActionState.FallingTransition(); }

        public  void FaceLeftTransition() { marioActionState.FaceLeftTransition(); }
        public  void FaceRightTransition() { marioActionState.FaceRightTransition(); }

        public  void CrouchingDiscontinueTransition() { marioActionState.CrouchingDiscontinueTransition(); }//when you exit crouch, release down key
        public  void FaceLeftDiscontinueTransition() { marioActionState.FaceLeftDiscontinueTransition(); }//generic entering walk and run, face left then start walking, then start running
        public  void FaceRightDiscontinueTransition() { marioActionState.FaceRightDiscontinueTransition(); }
        public  void WalkingDiscontinueTransition() { marioActionState.WalkingDiscontinueTransition(); }//decelerata and go to standing
        public  void RunningDiscontinueTransition() { marioActionState.RunningDiscontinueTransition(); }//decelerate and go to walking dis
        public  void JumpingDiscontinueTransition() { marioActionState.JumpingDiscontinueTransition(); }//abort jump or force jump to disc bc you reached apex of jump

        public  void Enter(IMarioPowerUpStates previousPowerUpState) { }
        //ublic  void Exit() { }

        public void smallMarioTransformation() { marioPowerUpState.smallMarioTransformation(); }

        public  void bigMarioTransformation() 
        {
            marioPowerUpState.bigMarioTransformation();
            this.CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, 32, 64);
        }

        public  void fireMarioTransformation() { marioPowerUpState.fireMarioTransformation(); }

        public  void DeadTransition() { marioPowerUpState.DeadTransition(); }

        public void Trigger()
        {
            throw new NotImplementedException();
        }

        public void SetPosition(Vector2 location)
        {
            this.Position = location;
            //throw new NotImplementedException();
        }

        public void HandleCollision(IGameObjects entity)
        {
            throw new NotImplementedException();
        }

        public void ToggleCollisionBoxes()
        {
            drawBox = !drawBox;
        }
    }
}