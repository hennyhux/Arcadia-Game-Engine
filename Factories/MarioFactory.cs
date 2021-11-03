using GameSpace.GameObjects.BlockObjects;
using GameSpace.Interfaces;
using GameSpace.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace GameSpace.Factories
{
    public class MarioFactory
    {
        private protected static GameRoot instanceGame;
        private Texture2D normalMarioSprite;
        private static MarioFactory instance = new MarioFactory();
        //private int currentState = SmallMarioStanding;

        //private static SpriteBlockFactory instance = new SpriteBlockFactory();
        public static MarioFactory GetInstance()
        {
            return instance;
        }

        //private Sprite
        //He used protected Dictonary<int, Sprite> sprites;
        // public abstract Sprite CreateSprite(int type);

        public enum eMarioSprite
        {
            DeadMario = 0,
            //StandingMario = 0x001,
            //SuperStandingMario = 0x002,
            //BigMarioStanding = 0x002,

            //SmallMask 0x0F
            SmallMarioStanding = 0x01,
            SmallMarioCrouching = 2,
            SmallMarioWalking = 0x03,//0x03, < - Dont Use
            SmallMarioRunning = 0x04,//0x04
            SmallMarioJumping = 0x05,//0x05
            SmallMarioFalling = 0x06,//0x06, 

            SmallMarioDying = 0x07,
            SmallMarioBouncing = 0x08,

            //BigMask = 0x0F0
            BigMarioStanding = 0x010,
            BigMarioCrouching = 0x020,
            BigMarioWalking = 0x030,
            BigMarioRunning = 0x040,
            BigMarioJumping = 0x050,
            BigMarioFalling = 0x060,
            BigMarioDying = 0x070,
            BigMarioBouncing = 0x080,

            //FireMask = 0x0F00
            FireMarioStanding = 0x0100,
            FireMarioCrouching = 0x0200,
            FireMarioWalking = 0x0300,
            FireMarioRunning = 0x0400,
            FireMarioJumping = 0x0500,
            FireMarioFalling = 0x0600,
            FireMarioDying = 0x0700,
            FireMarioBouncing = 0x0800,

            //SmallInvicinbleMask 0x0F000
            InvincibleSmallMarioStanding = 0x01000,
            InvincibleSmallMarioCrouching = 0x02000,
            InvincibleSmallMarioWalking = 0x03000,
            InvincibleSmallMarioRunning = 0x04000,
            InvincibleSmallMarioJumping = 0x05000,
            InvincibleSmallMarioFalling = 0x06000,
            InvincibleSmallMarioDying = 0x07000,
            InvincibleSmallMarioBouncing = 0x08000,

            //BigInvincibleMask 0x0F0000
            InvincibleBigMarioStanding = 0x010000,
            InvincibleBigMarioCrouching = 0x020000,
            InvincibleBigMarioWalking = 0x030000,
            InvincibleBigMarioRunning = 0x040000,
            InvincibleBigMarioJumping = 0x050000,
            InvincibleBigMarioFalling = 0x060000,
            InvincibleBigMarioDying = 0x070000,
            InvincibleBigMarioBouncing = 0x080000,

        }

        //static public int MarioSpriteType(IMarioActionStates currentActionState, IMarioPowerUpStates currentPowerUpState)//, IMarioPowerUpState currentPowerUpState)
        //static public int MarioSpriteType(IMarioPowerUpStates currentActionState)//, IMarioPowerUpState currentPowerUpState)
        public static int MarioSpriteType(IMarioActionStates currentActionState, IMarioPowerUpStates currentPowerUpState)
        {
            Debug.WriteLine("MSTypeFactory(90) currentActionState, {0}", currentActionState);
            Debug.WriteLine("MSTypeFactory(90) currentPowerUpState, {0}", currentPowerUpState);
            int type = (int)eMarioSprite.SmallMarioStanding;
            if (currentActionState is GameSpace.States.MarioStates.DeadMarioState)
            {
                //currentState = (int)eMarioSprite.SmallMarioStanding;
                type = (int)eMarioSprite.DeadMario;
            }

            if (currentActionState is GameSpace.States.MarioStates.SmallMarioStandingState)
            {
                //currentState = (int)eMarioSprite.SmallMarioStanding;
                type = (int)eMarioSprite.SmallMarioStanding;
            }
            else
             if (currentActionState is GameSpace.States.MarioStates.SmallMarioWalkingState)
            {
                type = (int)eMarioSprite.SmallMarioWalking;
            }
            else
             if (currentActionState is GameSpace.States.MarioStates.SmallMarioRunningState)
            {
                type = (int)eMarioSprite.SmallMarioRunning;
            }
            else
             if (currentActionState is GameSpace.States.MarioStates.SmallMarioJumpingState)
            {
                type = (int)eMarioSprite.SmallMarioJumping;
            }
            else
             if (currentActionState is GameSpace.States.MarioStates.SmallMarioFallingState)
            {
                type = (int)eMarioSprite.SmallMarioFalling;
            }

            //BIG MARIO
            if (currentActionState is GameSpace.States.MarioStates.BigMarioStandingState)
            {
                type = (int)eMarioSprite.BigMarioStanding;
            }
            else
            if (currentActionState is GameSpace.States.MarioStates.BigMarioCrouchingState)
            {
                type = (int)eMarioSprite.BigMarioCrouching;
            }
            else
            if (currentActionState is GameSpace.States.MarioStates.BigMarioRunningState)
            {
                type = (int)eMarioSprite.BigMarioRunning;
            }
            else
            if (currentActionState is GameSpace.States.MarioStates.BigMarioJumpingState)
            {
                type = (int)eMarioSprite.BigMarioJumping;
            }
            else
            if (currentActionState is GameSpace.States.MarioStates.BigMarioFallingState)
            {
                type = (int)eMarioSprite.BigMarioFalling;
            }

            //FIRE MARIO
            if (currentActionState is GameSpace.States.MarioStates.FireMarioStandingState)
            {
                type = (int)eMarioSprite.FireMarioStanding;
            }
            else
            if (currentActionState is GameSpace.States.MarioStates.FireMarioCrouchingState)
            {
                type = (int)eMarioSprite.FireMarioCrouching;
            }
            else
            if (currentActionState is GameSpace.States.MarioStates.FireMarioRunningState)
            {
                type = (int)eMarioSprite.FireMarioRunning;
            }
            else
            if (currentActionState is GameSpace.States.MarioStates.FireMarioJumpingState)
            {
                type = (int)eMarioSprite.FireMarioJumping;
            }
            else
            if (currentActionState is GameSpace.States.MarioStates.FireMarioFallingState)
            {
                type = (int)eMarioSprite.FireMarioFalling;
            }

            //Debug.WriteLine("GetType(152)Type: {2}, AcurrentState: {0}, PowerUp, {1}, ", currentActionState, currentPowerUpState, type);
            return type;
        }

        public MarioFactory()
        {

        }

        public static MarioFactory GetInstance(GameRoot game)
        {
            if (instance == null)
            {
                instance = new MarioFactory();
                instanceGame = game;
            }

            return instance;

        }


        public MarioSprite CreateSprite(int type)
        {

            eMarioSprite eType = (eMarioSprite)type;
            MarioSprite sprite = null;

            switch (eType)
            {

                case eMarioSprite.DeadMario:
                    sprite = instance.DeadMarioSprite();
                    break;
                case eMarioSprite.SmallMarioStanding:
                    sprite = instance.SmallMarioStandingSprite();
                    break;
                case eMarioSprite.SmallMarioCrouching: //Doesn't Exist
                    sprite = instance.SmallMarioStandingSprite();
                    break;
                case eMarioSprite.SmallMarioWalking:
                    Debug.WriteLine("MFactory(139) Walking");
                    sprite = instance.SmallMarioWalkingSprite();
                    break;
                case eMarioSprite.SmallMarioRunning:
                    sprite = instance.SmallMarioRunningSprite();
                    break;
                case eMarioSprite.SmallMarioJumping:
                    sprite = instance.SmallMarioJumpingSprite();
                    break;
                case eMarioSprite.SmallMarioFalling:
                    sprite = instance.SmallMarioFallingSprite();
                    break;
                /*case eMarioSprite.SmallMarioDying: 
                    sprite = instance.SmallMarioDyingSprite(); 
                    break;
                case eMarioSprite.SmallMarioBouncing: 
                    sprite = instance.SmallMarioBouncingSprite(); 
                    break;*/


                case eMarioSprite.BigMarioStanding:
                    sprite = instance.BigMarioStandingSprite();
                    break;

                case eMarioSprite.BigMarioCrouching:
                    sprite = instance.BigMarioCrouchingSprite();
                    break;

                case eMarioSprite.BigMarioWalking:
                    sprite = instance.BigMarioWalkingSprite();
                    break;

                case eMarioSprite.BigMarioRunning:
                    sprite = instance.BigMarioRunningSprite();
                    break;

                case eMarioSprite.BigMarioJumping:
                    sprite = instance.BigMarioJumpingSprite();
                    break;

                case eMarioSprite.BigMarioFalling:
                    sprite = instance.BigMarioFallingSprite();
                    break;

                /*case eMarioSprite.BigMarioDying: 
                    sprite = instance.BigMarioDyingSprite(); 
                    break;

                case eMarioSprite.BigMarioBouncing: 
                    sprite = instance.BigMarioBouncingSprite(); 
                    break;*/


                case eMarioSprite.FireMarioStanding:
                    sprite = instance.FireMarioStandingSprite();
                    break;

                case eMarioSprite.FireMarioCrouching:
                    sprite = instance.FireMarioCrouchingSprite();
                    break;

                case eMarioSprite.FireMarioWalking:
                    sprite = instance.FireMarioWalkingSprite();
                    break;

                case eMarioSprite.FireMarioRunning:
                    sprite = instance.FireMarioRunningSprite();
                    break;

                case eMarioSprite.FireMarioJumping:
                    sprite = instance.FireMarioJumpingSprite();
                    break;

                case eMarioSprite.FireMarioFalling:
                    sprite = instance.FireMarioFallingSprite();
                    break;

                    /* case eMarioSprite.FireMarioDying: 
                         sprite = instance.FireMarioDyingSprite(); 
                         break;

                     case eMarioSprite.FireMarioBouncing: 
                         sprite = instance.FireMarioBouncingSprite(); 
                         break;


                     case eMarioSprite.InvincibleSmallMarioStanding: 
                         sprite = instance.InvincibleSmallMarioStandingSprite(); 
                         break;

                     case eMarioSprite.InvincibleSmallMarioCrouching: 
                         sprite = instance.InvincibleSmallMarioCrouchingSprite(); 
                         break;

                     case eMarioSprite.InvincibleSmallMarioWalking: 
                         sprite = instance.InvincibleSmallMarioWalkingSprite(); 
                         break;

                     case eMarioSprite.InvincibleSmallMarioRunning: 
                         sprite = instance.InvincibleSmallMarioRunningSprite(); 
                         break;

                     case eMarioSprite.InvincibleSmallMarioJumping: 
                         sprite = instance.InvincibleSmallMarioJumpingSprite(); 
                         break;

                     case eMarioSprite.InvincibleSmallMarioFalling: 
                         sprite = instance.InvincibleSmallMarioFallingSprite(); 
                         break;

                     case eMarioSprite.InvincibleSmallMarioDying: 
                         sprite = instance.InvincibleSmallMarioDyingSprite(); 
                         break;

                     case eMarioSprite.InvincibleSmallMarioBouncing: 
                         sprite = instance.InvincibleSmallMarioBouncingSprite(); 
                         break;


                     case eMarioSprite.InvincibleBigMarioStanding: 
                         sprite = instance.InvincibleBigMarioStandingSprite(); 
                         break;

                     case eMarioSprite.InvincibleBigMarioCrouching: 
                         sprite = instance.InvincibleBigMarioCrouchingSprite(); 
                         break;

                     case eMarioSprite.InvincibleBigMarioWalking: 
                         sprite = instance.InvincibleBigMarioWalkingSprite(); 
                         break;

                     case eMarioSprite.InvincibleBigMarioRunning: 
                         sprite = instance.InvincibleBigMarioRunningSprite(); 
                         break;

                     case eMarioSprite.InvincibleBigMarioJumping: 
                         sprite = instance.InvincibleBigMarioJumpingSprite(); 
                         break;

                     case eMarioSprite.InvincibleBigMarioFalling: 
                         sprite = instance.InvincibleBigMarioFallingSprite(); 
                         break;

                     case eMarioSprite.InvincibleBigMarioDying: 
                         sprite = instance.InvincibleBigMarioDyingSprite(); 
                         break;

                     case eMarioSprite.InvincibleBigMarioBouncing: 
                         sprite = instance.InvincibleBigMarioBouncingSprite(); 
                         break;*/

            }

            return sprite;
        }

        public void LoadContent(ContentManager content)
        {
            //Debug.WriteLine("MFactory(86) load content");
            normalMarioSprite = content.Load<Texture2D>("AvatarSprite/OGMarioSheet");
        }

        public Mario ReturnMario(Vector2 location)
        {
            return new Mario(location);
        }

        public MarioSprite ReturnMarioStandingLeftSprite()
        {
            return new MarioSprite(normalMarioSprite, 200, 200, 0, 0, 0);
        }

        public MarioSprite DeadMarioSprite()
        {
            Debug.WriteLine("DEAD MARIO\n");
            return new MarioSprite(normalMarioSprite, 350, 200, 0, 4, 0);
        }

        public MarioSprite SmallMarioStandingSprite()
        {
            Debug.WriteLine("SmallMarioStandingSprite\n");
            return new MarioSprite(normalMarioSprite, 999, 999, 0, 0, 0);
        }

        public MarioSprite SmallMarioWalkingSprite()//mario.SmallMarioWalkingSprite
        {
            return new MarioSprite(normalMarioSprite, 350, 200, 0, 0, 2);
        }

        public MarioSprite SmallMarioRunningSprite()//mario.SmallMarioRunningSprite
        {
            return new MarioSprite(normalMarioSprite, 350, 200, 0, 0, 3);
        }

        public MarioSprite SmallMarioJumpingSprite()//mario.SmallMarioJumpingSprite
        {
            return new MarioSprite(normalMarioSprite, 350, 200, 0, 0, 4);
        }

        public MarioSprite SmallMarioFallingSprite()//mario.SmallMarioFallingSprite
        {
            return new MarioSprite(normalMarioSprite, 350, 200, 0, 0, 5);
        }

        public MarioSprite ReturnBigMarioStandingLeftSprite()
        {
            return new MarioSprite(normalMarioSprite, 350, 200, 0, 1, 0);
        }
        public MarioSprite BigMarioStandingSprite()
        {
            return new MarioSprite(normalMarioSprite, 350, 200, 0, 1, 0);
        }

        public MarioSprite BigMarioCrouchingSprite()
        {
            return new MarioSprite(normalMarioSprite, 350, 200, 0, 1, 1);
        }

        public MarioSprite BigMarioWalkingSprite()//mario.SmallMarioWalkingSprite
        {
            return new MarioSprite(normalMarioSprite, 350, 200, 0, 1, 2);
        }

        public MarioSprite BigMarioRunningSprite()//mario.SmallMarioRunningSprite
        {
            return new MarioSprite(normalMarioSprite, 350, 200, 0, 1, 3);
        }

        public MarioSprite BigMarioJumpingSprite()//mario.SmallMarioJumpingSprite
        {
            return new MarioSprite(normalMarioSprite, 350, 200, 0, 1, 4);
        }

        public MarioSprite BigMarioFallingSprite()//mario.SmallMarioFallingSprite
        {
            return new MarioSprite(normalMarioSprite, 350, 200, 0, 1, 5);
        }
        //FIRE MARIO
        public MarioSprite FireMarioStandingSprite()
        {
            return new MarioSprite(normalMarioSprite, 350, 200, 0, 2, 0);
        }

        public MarioSprite FireMarioCrouchingSprite()
        {
            return new MarioSprite(normalMarioSprite, 350, 200, 0, 2, 1);
        }

        public MarioSprite FireMarioWalkingSprite()//mario.SmallMarioWalkingSprite
        {
            return new MarioSprite(normalMarioSprite, 350, 200, 0, 2, 2);
        }

        public MarioSprite FireMarioRunningSprite()//mario.SmallMarioRunningSprite
        {
            return new MarioSprite(normalMarioSprite, 350, 200, 0, 2, 3);
        }

        public MarioSprite FireMarioJumpingSprite()//mario.SmallMarioJumpingSprite
        {
            return new MarioSprite(normalMarioSprite, 350, 200, 0, 2, 4);
        }

        public MarioSprite FireMarioFallingSprite()//mario.SmallMarioFallingSprite
        {
            return new MarioSprite(normalMarioSprite, 350, 200, 0, 2, 5);
        }
    }
}
