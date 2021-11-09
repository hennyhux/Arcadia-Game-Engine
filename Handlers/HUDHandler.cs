using GameSpace.Abstracts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


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
            spritebatch.DrawString(HeadsUpDisplay, "Score\n    " + mario.score.ToString(), HudPosition, Color.Black);
            spritebatch.DrawString(HeadsUpDisplay, "Time\n " + seconds, new Vector2(HudPosition.X + 160, HudPosition.Y), Color.Black);
            spritebatch.DrawString(HeadsUpDisplay, "World\n  1-1", new Vector2(HudPosition.X + 320, HudPosition.Y), Color.Black);
            spritebatch.DrawString(HeadsUpDisplay, "Coins\n    " + mario.numCoinsCollected, new Vector2(HudPosition.X + 480, HudPosition.Y), Color.Black); //Update to display coins
            //spritebatch.DrawString(HeadsUpDisplay, "Lives\n   " + marioLives.ToString(), new Vector2(HudPosition.X + 640, HudPosition.Y), Color.Black); MarioHandler.GetInstance().DecrementMarioLives();
            spritebatch.DrawString(HeadsUpDisplay, "Lives\n   " + MarioHandler.marioLives, new Vector2(HudPosition.X + 640, HudPosition.Y), Color.Black);
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
            int score = MarioHandler.GetInstance().CalculateScore();
               spritebatch.DrawString(HeadsUpDisplay, "Score\n    " + mario.score.ToString(), new Vector2(0, 0), Color.Black);
            spritebatch.DrawString(HeadsUpDisplay, "Time\n " + seconds, new Vector2(160, 0), Color.Black);
            spritebatch.DrawString(HeadsUpDisplay, "World\n  1-1", new Vector2(+ 320, 0), Color.Black);
            spritebatch.DrawString(HeadsUpDisplay, "Coins\n    " + mario.numCoinsCollected, new Vector2(480,0), Color.Black); //Update to display coins
            //spritebatch.DrawString(HeadsUpDisplay, "Lives\n   " + marioLives.ToString(), new Vector2(640, 0), Color.Black);
            spritebatch.DrawString(HeadsUpDisplay, "Lives\n   " + MarioHandler.marioLives.ToString(), new Vector2(640, 0), Color.Black);
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
