using GameSpace.Abstracts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GameSpace.GameObjects.ItemObjects;//TEMP
using GameSpace.Sprites;//TEMP
using GameSpace.EntityManaging;
using GameSpace.Enums;

namespace GameSpace.Machines
{
    public class HUDHandler : AbstractHandler
    {
        private SpriteFont HeadsUpDisplay;
        private Texture2D chungus;
        private Vector2 HudPosition;
        public static long ticks;
        public static long seconds;
        public static long ticksMax = 4010000000;
        //public static long ticksMax = 40100000; Testing timer and lose of life from running out of time
        public static long convertToSeconds = 10000000;
        public static int bonusPoints;
        private Coin HUDCoin;// = new Coin(new Vector2(0, 0));
        //private Coin testCoin = new Coin(new Vector2(0, 0)); 
        //Coin coin = new Coin(new Vector2(0, 0));//Get Coin to draw animated in HUD

        private static readonly HUDHandler instance = new HUDHandler();
        public static HUDHandler GetInstance()
        {
            return instance;
        }

        private HUDHandler()
        {
            
        }

        #region Death Timer
        public void ResetTimer()
        {
            timer = 0;
        }

        public void UpdateTimer()
        {
            timer += internalGametime.ElapsedGameTime.Ticks;
            ticks = ticksMax - timer;
            seconds = ticks / convertToSeconds;
            if (seconds == 50) MusicHandler.GetInstance().PlaySoundEffect(11);

            if (seconds == 0 || game.GetMario.marioActionState is GameSpace.States.MarioStates.DeadMarioState)
            {
                if(seconds <= 0 && game.CurrentState is GameSpace.States.GameStates.PlayingGameState)// AND YOU DIDNT COMPLETE OBJECTIVE
                {
                    mario.DeadTransition();//Lose a life if timer reaches 0
                }
                ResetTimer();
            }
            else
            {
                bonusPoints = (int)seconds;
                //Add bonusPoints to Point Tracking System
            }
        }
        #endregion

        public void LoadContent(ContentManager content, GameRoot gameRoot)
        {
            HeadsUpDisplay = content.Load<SpriteFont>("font");
            chungus = content.Load<Texture2D>("Background/Untitled");
            HudPosition.X = 10;
            HudPosition.Y = 10;
            game = gameRoot;
            ResetTimer();
            

        }

        public void LoadGameTime(GameTime gameTime)
        {
            internalGametime = gameTime;

        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.DrawString(HeadsUpDisplay, "[" + mario.Player + "]\n", HudPosition, Color.Black);
            spritebatch.DrawString(HeadsUpDisplay, "Score: " + mario.score.ToString("D6"), new Vector2(HudPosition.X, HudPosition.Y+40), Color.Black);
            spritebatch.DrawString(HeadsUpDisplay, "Time\n" + seconds, new Vector2(HudPosition.X + 200, HudPosition.Y), Color.Black);
            spritebatch.DrawString(HeadsUpDisplay, "World\n  1-1", new Vector2(HudPosition.X + 320, HudPosition.Y), Color.Black);

            spritebatch.Draw(FinderHandler.GetInstance().FindItem((int)ItemID.HUDCOIN).Sprite.Texture, new Vector2((int)HudPosition.X + 440, (int)HudPosition.Y + 25),//Draws Animated Coin
                ((CoinSprite)FinderHandler.GetInstance().FindItem((int)ItemID.HUDCOIN).Sprite).getCurrentSpriteRect(), Color.White);
          
            spritebatch.DrawString(HeadsUpDisplay, "X  " + mario.numCoinsCollected, new Vector2(HudPosition.X + 460, HudPosition.Y + 20), Color.Black); //Update to display coins

            SpriteEffects facing = SpriteEffects.None;
            if (MarioHandler.mario.Facing == eFacing.RIGHT) facing = SpriteEffects.FlipHorizontally; //float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
            spritebatch.Draw(MarioHandler.mario.sprite.Texture, new Vector2((int)HudPosition.X + 640, (int)HudPosition.Y + 25), (Rectangle)MarioHandler.mario.sprite.getCurrentSpriteRect(), Color.White, 
                (float)0, new Vector2(1, 1),  new Vector2(1, 1), facing ,(float)1);
            spritebatch.DrawString(HeadsUpDisplay, "X  " + MarioHandler.marioLives, new Vector2(HudPosition.X + 660, HudPosition.Y + 20), Color.Black);

            UpdateHudPosition();
        }

        public void DrawStartingPanel(SpriteBatch spritebatch)
        {
            spritebatch.DrawString(HeadsUpDisplay, "PRESS N FOR NEW GAME", HudPosition, Color.Black);
            spritebatch.DrawString(HeadsUpDisplay, "PRESS L TO LOAD GAME (FUTURE FEATURE)", new Vector2(HudPosition.X, HudPosition.Y + 40), Color.Black);
            spritebatch.DrawString(HeadsUpDisplay, "PRESS Q TO QUIT GAME", new Vector2(HudPosition.X, HudPosition.Y + 80), Color.Black);
            spritebatch.DrawString(HeadsUpDisplay, "MAIN MENU WIP", new Vector2(HudPosition.X, HudPosition.Y + 120), Color.Black);
            spritebatch.Draw(chungus, new Vector2(HudPosition.X, HudPosition.Y + 200), Color.White);
            //UpdateHudPosition();
        }

        public void DrawEndingResetPanel(SpriteBatch spritebatch)
        {
            
            spritebatch.DrawString(HeadsUpDisplay, "[" + mario.Player + "]\n", new Vector2(0, 0), Color.Black);
            spritebatch.DrawString(HeadsUpDisplay, "Score: " + mario.score.ToString("D6"), new Vector2(0, 40), Color.Black);
            spritebatch.DrawString(HeadsUpDisplay, "Time\n" + seconds, new Vector2(200, 0), Color.Black);
            spritebatch.DrawString(HeadsUpDisplay, "World\n  1-1", new Vector2(320, 0), Color.Black);

            
           // ((CoinSprite)FinderHandler.GetInstance().FindItem((int)ItemID.HUDCOIN).Sprite).Draw(spritebatch, new Vector2(460, 20));//Draws Animated Coin
            Coin HUDCoin = new Coin(new Vector2(0, 0));
            HUDCoin.Sprite.Draw(spritebatch, new Vector2(460, 20));//Draws Unanimated Coin
            //spritebatch.Draw(FinderHandler.GetInstance().FindItem((int)ItemID.HUDCOIN).Sprite.Texture, new Vector2(640, 25), 
               // ((CoinSprite)FinderHandler.GetInstance().FindItem((int)ItemID.HUDCOIN).Sprite).getCurrentSpriteRect(), Color.White);
            spritebatch.DrawString(HeadsUpDisplay, "X  " + mario.numCoinsCollected, new Vector2(480, 20), Color.Black); //Update to display coins
                                                                                                                                                        
            spritebatch.Draw(MarioHandler.mario.sprite.Texture, new Vector2(640, 25), (Rectangle)MarioHandler.mario.sprite.getCurrentSpriteRect(), Color.White);
            spritebatch.DrawString(HeadsUpDisplay, "X  " + MarioHandler.marioLives, new Vector2(660, 20), Color.Black);//Lives

            spritebatch.DrawString(HeadsUpDisplay, "PRESS R FOR NEW GAME", new Vector2(0, 120), Color.Black);
            spritebatch.DrawString(HeadsUpDisplay, "PRESS Q TO QUIT GAME", new Vector2(0, 200), Color.Black);
            UpdateHudPosition();
        }

        private void UpdateHudPosition()
        {
            if (cameraCopy.Position.X + 10> HudPosition.X || HudPosition.X > cameraCopy.Position.X)
            {
                HudPosition.X = cameraCopy.Position.X + 10;
            }
        }

    }
}
