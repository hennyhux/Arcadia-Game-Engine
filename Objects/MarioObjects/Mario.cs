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
    public class Mario : IMario
    {

        //actionStateMachine
        //powerupStateMachine
        //
        //public Texture2D Texture { get; set; }
        private int x;
        private int y;
        public static int X { get; set; }
        public static int Y { get; set; }


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

        //public IMarioPowerUpStates marioPowerUpState { get; set; }

        //private MarioPowerUpStates previousMarioActionState { get; set; }
        // private MarioPowerUpStates previousMarioPowerUpState;



        //public Mario(Game1 game, Texture2D texture)
        public Mario(GameRoot game)
        {
            Debug.WriteLine("Mario.cs(50) CREATED MARIO \n");
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

        public void Draw(SpriteBatch spritebatch, Vector2 location)
        {
            if (Facing == eFacing.Left)
                //sprite.facingRight = 0;
                sprite.facing = SpriteEffects.None;// swap if
            else
                //sprite.facingRight = 0;
                sprite.facing = SpriteEffects.FlipHorizontally;// swap if base facing direction of sprite is right
            this.sprite.Draw(spritebatch, new Vector2(500, 400));
        }
        public void Update(GameTime gametime)
        {
            
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

        public  void bigMarioTransformation() { marioPowerUpState.bigMarioTransformation(); }

        public  void fireMarioTransformation() { marioPowerUpState.fireMarioTransformation(); }

        public  void DeadTransition() { marioPowerUpState.DeadTransition(); }
       
    }
}