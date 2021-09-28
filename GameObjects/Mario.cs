using GameSpace.Factories;
using GameSpace.Interfaces;
using GameSpace.States.BlockStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.GameObjects.BlockObjects
{
    public class Mario
    {
        public Texture2D Texture { get; set; }
        private int X;
        private int Y;
        private protected int currentFrame;

        private protected int totalFrames;
        private bool IsVisible;

        private protected int timeSinceLastFrame;
        private protected int milliSecondsPerFrame;

        //private int actionState; //[Idling, Crouching, Walking, Running, Jumping, Falling, Dying]
        //private int marioPower;// [Small, Big, Fire, Star, Dead]
        // private int facingRight;// left = 0, right = 1
        //private SpriteEffects facing;
        //private bool newState;
        private MarioStates state;

        public MarioStates states { get; set; }

        //public Mario(Game1 game, Texture2D texture)
        public Mario(Game1 game)
        {
            this.state = new MarioStates(game);
        }

        public MarioStates getStates()
        {
            return this.state;
        }

        public void Draw(SpriteBatch spritebatch, Vector2 location)
        {
            this.state.Draw(spritebatch, new Vector2(500, 400));
        }
        public void Update(GameTime gametime)
        {
            this.state.Update(gametime);
        }

        public void Idle()
        {
            state.Idle();
        }

        public void Crouch()
        {
            state.Crouch();
        }

        public void Walk()
        {
            state.Walk();
        }

        public void Run()
        {
            state.Run();
        }

        public void Jump()
        {
            state.Jump();
        }

        public void Fall()
        {
            state.Fall();
        }

        public void Die()
        {
            state.Die();
        }

        public void Small()
        {
            state.Small();
        }

        public void Big()
        {
            state.Big();
        }

        public void Fire()
        {
            state.Fire();
        }

        public void Star()
        {
            state.Star();
        }

        public void Dead()
        {
            state.Dead();
        }

        public void FaceLeft()
        {
            state.FaceLeft(); ;
        }

        public void FaceRight()
        {
            state.FaceRight();
        }
    }
}
