using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameSpace.Factories;
using GameSpace.Interfaces;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.Sprites;

namespace GameSpace.States.BlockStates
{
    public class MarioStates : ISprite
    {

        private MarioSprite sprite;
        private MarioFactory marioFactory;

        public Texture2D Texture { get; set; }
        private int X;
        private int Y;
        private protected int currentFrame;

        private protected int totalFrames;
        private bool IsVisible;

        private protected int timeSinceLastFrame;
        private protected int milliSecondsPerFrame;

        private int actionState; //[Idling, Crouching, Walking, Running, Jumping, Falling, Dying]
        private int marioPower;// [Small, Big, Fire, Star, Dead]
        private int facingRight;// left = 0, right = 1
        private SpriteEffects facing;
        private bool newState;

        /* Array Format is
         * First 34 is small, next 34 is big, next 34 is fire, then star, then dead
         * 0-8 and 18-25 is Left Actions and 9-17 and 26-34 is Right Actions
         * 0-8 [Death, Crouch, Jump, Turn Around/Skid, Fireball, Run/Walk x3, Idle] Flipped to Right Side [Idle, Run/Walk x3, Fireball, Turn Around/Skid, Jump, Crouch, Death] 9-12
         * 18-25 [Climb x2, Swim x6] Then Flipped to Right Side [Swim x6, Climb x2] 26-34
         * 
         * Small Mario does not have textures for crouching, fireball and Swim 5/6
         * Big Mario does not have textures for dying and fireball
         * Fire Mario does not have textures for dying
         *      Meaning the corresponding indexes will be 0's
         */
        private int[] XFrame = new int[] { 0, 0, 29, 60, 0, 89, 121, 150, 181, 211, 241, 272, 300, 0, 331, 359, 0, 390, 30, 61, 0, 0, 90, 120, 151, 180, 210, 241, 271, 301, 0, 0, 331, 361, 0, 0, 30, 60, 0, 90, 121, 150, 180, 209, 239, 270, 299, 0, 329, 359, 389, 0, 1, 28, 52, 78, 103, 127, 152, 180, 209, 237, 262, 288, 313, 337, 363, 391, 0, 0, 27, 52, 77, 102, 128, 152, 180, 209, 237, 264, 287, 312, 337, 362, 389, 0, 1, 28, 52, 78, 103, 127, 152, 180, 209, 237, 262, 288, 313, 337, 363, 391 };
        private int[] YFrame = new int[] { 16, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 15, 30, 30, 0, 0, 30, 30, 30, 30, 30, 30, 30, 30, 0, 0, 30, 30, 0, 57, 52, 52, 0, 53, 52, 52, 52, 52, 52, 52, 52, 0, 52, 52, 57, 0, 88, 89, 88, 88, 88, 88, 88, 88, 88, 88, 88, 88, 88, 88, 89, 88, 0, 127, 122, 122, 123, 123, 122, 122, 122, 122, 122, 121, 123, 123, 122, 122, 127, 0, 159, 160, 159, 159, 159, 159, 159, 159, 159, 159, 159, 159, 159, 159, 160, 159 };
        private int[] XWidth = new int[] { 15, 0, 17, 14, 0, 16, 12, 14, 13, 13, 14, 12, 16, 0, 14, 17, 0, 15, 14, 13, 0, 0, 14, 14, 13, 15, 15, 13, 14, 14, 0, 0, 13, 14, 0, 16, 16, 16, 0, 16, 14, 16, 16, 16, 16, 14, 16, 0, 16, 16, 16, 0, 13, 14, 16, 14, 14, 16, 16, 16, 16, 16, 16, 14, 14, 16, 14, 13, 0, 16, 16, 16, 16, 16, 14, 17, 16, 16, 16, 14, 16, 16, 16, 16, 16, 0, 13, 14, 16, 14, 14, 16, 16, 16, 16, 16, 16, 14, 14, 16, 14, 13 };
        private int[] YHeight = new int[] { 14, 0, 16, 16, 0, 15, 16, 15, 16, 16, 15, 16, 15, 0, 16, 16, 0, 14, 16, 15, 0, 0, 15, 15, 15, 15, 15, 15, 15, 15, 0, 0, 15, 16, 0, 22, 32, 32, 0, 30, 31, 32, 32, 32, 32, 31, 30, 0, 32, 32, 22, 0, 30, 27, 13, 30, 30, 29, 29, 29, 29, 29, 29, 30, 30, 30, 27, 30, 0, 22, 32, 32, 30, 30, 31, 32, 32, 32, 32, 31, 30, 30, 32, 32, 22, 0, 30, 27, 13, 30, 30, 29, 29, 29, 29, 29, 29, 30, 30, 30, 27, 30 };
        private int[] totalFramesAnimation = new int[] { 1, 1, 1, 1, 1, 3, 3, 3, 1, 1, 3, 3, 3, 1, 1, 1, 1, 1, 2, 2, 0, 0, 3, 3, 3, 1, 1, 3, 3, 3, 0, 0, 2, 2, 0, 1, 1, 1, 0, 3, 3, 3, 1, 1, 3, 3, 3, 0, 1, 1, 1, 0, 2, 2, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 2, 2, 0, 1, 1, 1, 2, 3, 3, 3, 1, 1, 3, 3, 3, 1, 1, 1, 1, 0, 2, 2, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 2, 2 };

        public MarioStates(Game1 game)
        //public MarioStates(Texture2D texture, int x, int y, int isFacingRight, int powerup, int action)
        {
            //marioFactory = game.GetMarioFactory.ReturnMario();
            Debug.WriteLine("IN STATE: ");
            //this.sprite = marioFactory.ReturnMarioStandingLeftSprite();
            this.sprite = game.GetMarioFactory.ReturnMarioStandingLeftSprite();


            facingRight = 0;
            marioPower = 0;
            actionState = 0;
            newState = true;
            facing = SpriteEffects.None; 

            /*Texture = texture;
            X = x;
            Y = y;
            currentFrame = 0;
            totalFrames = 0;
            IsVisible = true;

            milliSecondsPerFrame = 135;

            facingRight = isFacingRight;
            marioPower = powerup;
            actionState = action;
            newState = true;
            facing = SpriteEffects.None;*/
        }

        public MarioStates getState(Game1 game)
        //public MarioStates(Texture2D texture, int x, int y, int isFacingRight, int powerup, int action)
        {
            return this;
        }
        public void SetVisible() { IsVisible = !IsVisible; }

        public void Idle()
        {
            if(actionState != 0)
            {
                newState = true;
                sprite.setAction(0);
            }
            actionState = 0;
        }

        public void Crouch()
        {
            if(marioPower != 0)
            {
                actionState = 1;
                sprite.setAction(1);
                newState = true;
            }
        }

        public void Walk()
        {
            if (actionState != 2)
            {
                newState = true;
                sprite.setAction(2);
            }
            actionState = 2;
        }

        public void Run()
        {
            if (actionState != 3)
            {
                newState = true;
                sprite.setAction(3);
            }
            actionState = 3;
        }

        public void Jump()
        {
            if (actionState != 4)
            {
                newState = true;
                sprite.setAction(4);
            }
            actionState = 4;
        }

        public void Fall()
        {
            if (actionState != 5)
            {
                newState = true;
                sprite.setAction(5);

            }
            actionState = 5;
        }

        public void Die()
        {
            if (actionState != 6)
            {
                newState = true;
                sprite.setAction(6);
            }
            actionState = 6;
        }

        public void Small()
        {
            marioPower = 0;
            sprite.setPower(0);
        }

        public void Big()
        {
            marioPower = 1;
            sprite.setPower(1);
        }

        public void Fire()
        {
            marioPower = 2;
            sprite.setPower(2);
        }

        public void Star()
        {
            //marioPower = 4;
            //Not Implemented Yet
            sprite.setPower(3);
        }

        public void Dead()
        {
            sprite.setPower(4);
            //marioPower = 5;
        }

        public void FaceLeft()
        {
            facingRight = 0;
            sprite.setDirection(0);
        }

        public void FaceRight()
        {
            sprite.setDirection(1);
        }

        /*
        public void Update(GameTime gametime)
        {
            if (IsVisible)
            {
                int startingFrame = 0;

                //facingRight = 1;
                //marioPower = 1;// [Small, Big, Fire, Star, Dead]
                //actionState = 3;//[Idling, Crouching, Walking, Running, Jumping, Falling, Dying]
                //newState = false;
                totalFrames = totalFramesAnimation[currentFrame]; // gets previous frame's total frames in animation
                
                facing = SpriteEffects.None;
                if (marioPower == 4)//Mario is dead
                {
                    startingFrame = (0 + 17 * (facingRight));
                }
                else if (actionState == 0)//Idling
                {
                    startingFrame = (8 + facingRight) + (34 * (marioPower));
                }
                else if (actionState == 1)//Crouching
                {
                    startingFrame = (1 + 15 * facingRight) + (34 * (marioPower));
                }
                else if (actionState == 2)//Walking
                {
                    startingFrame = (7 + 3 * (facingRight) + (34 * (marioPower)));
                }
                else if (actionState == 3)//Running
                {
                    startingFrame = (7 + 3 * (facingRight) + (34 * (marioPower)));
                }
                else if (actionState == 4)//Jumping
                {
                    startingFrame = (2 + 13 * (facingRight) + (34 * (marioPower)));
                }
                else if (actionState == 5)//Falling
                {
                    //startingFrame = (15 - 13 * (facingRight) + (34 * (marioPower)));
                    startingFrame = (2 + 13 * (facingRight) + (34 * (marioPower)));
                    facing = SpriteEffects.FlipVertically;
                   // Debug.WriteLine("facing: " + facing);
                }
                else if (actionState == 6)//Dying
                {
                    startingFrame = (0 + 17 * (facingRight));
                }
                

                if (newState == false)
                {
                    timeSinceLastFrame += gametime.ElapsedGameTime.Milliseconds;
                    if (timeSinceLastFrame > milliSecondsPerFrame)
                    {
                        timeSinceLastFrame -= milliSecondsPerFrame;
                       // Debug.WriteLine("milliSecondsPerFrame: " + milliSecondsPerFrame);
                        if (facingRight == 0)
                        {
                            currentFrame = currentFrame - 1;
                        }
                        else
                        {
                            currentFrame = currentFrame + 1;
                        }
                    }

                    if (Math.Abs(currentFrame - startingFrame) >= totalFramesAnimation[startingFrame])
                    {
                       /* Debug.WriteLine("calculation: " + Math.Abs(currentFrame - startingFrame));
                        Debug.WriteLine("answer: " + totalFramesAnimation[startingFrame]);
                        Debug.WriteLine("currentFrame: " + currentFrame);*//*
                        currentFrame = startingFrame;
                        
                    }
                }
                else
                {
                    currentFrame = startingFrame;
                    newState = false;
                }
               

            }
        }*/

        public void Update(GameTime gametime)
        {
            sprite.Update(gametime);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            
           /* int width = XWidth[currentFrame];
            int height = YHeight[currentFrame];

            Rectangle sourceRectangle = new Rectangle(XFrame[currentFrame], YFrame[currentFrame], width, height);
            Rectangle destinationRectangle = new Rectangle(X, Y, width*2, height*2);

            

            //spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            spriteBatch.Draw(Texture,  destinationRectangle, sourceRectangle, Color.White, 0, new Vector2(0, 0), facing, 0);*/

            sprite.Draw(spriteBatch, new Vector2(400, 150));
        }

        /*public void Draw(SpriteBatch spriteBatch, Vector2 Location)
        {
            sprite.Draw(spriteBatch, new Vector2(400, 150));
        }*/

        public void UpdateLocation(int dx, int dy)
        {
            //throw new NotImplementedException();
            X += dx;
            Y += dy;
        }

    }
}
