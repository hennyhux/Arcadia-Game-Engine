using GameSpace.Abstracts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.Machines
{
    public class HUDHandler : AbstractHandler
    {
        private SpriteFont HeadsUpDisplay;
        private Vector2 HudPosition;
        public static long ticks;
        public static long seconds;
        public static long ticksMax = 4000000000;
        public static long convertToSeconds = 10000000;

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
            if (seconds == 0 || game.GetMario.marioActionState is GameSpace.States.MarioStates.DeadMarioState)
            {
                ResetTimer();
            }
        }
        #endregion

        public void LoadContent(ContentManager content, int lives, GameRoot gameRoot)
        {
            HeadsUpDisplay = content.Load<SpriteFont>("font");
            HudPosition.X = 50;
            HudPosition.Y = 10;
            game = gameRoot;
            marioLives = lives;
            ResetTimer();
        }

        public void LoadGameTime(GameTime gameTime)
        {
            internalGametime = gameTime;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.DrawString(HeadsUpDisplay, "Score\n    " + mario.score.ToString(), HudPosition, Color.Black);
            spritebatch.DrawString(HeadsUpDisplay, "Time\n   " + seconds, new Vector2(HudPosition.X + 160, HudPosition.Y), Color.Black);
            spritebatch.DrawString(HeadsUpDisplay, "World\n  1-1", new Vector2(HudPosition.X + 320, HudPosition.Y), Color.Black);
            spritebatch.DrawString(HeadsUpDisplay, "Coins\n    " + 0, new Vector2(HudPosition.X + 480, HudPosition.Y), Color.Black); //Update to display coins
            spritebatch.DrawString(HeadsUpDisplay, "Lives\n   " + marioLives.ToString(), new Vector2(HudPosition.X +  640, HudPosition.Y), Color.Black);
            UpdateHudPosition();
        }

        private void UpdateHudPosition()
        {
            if (cameraCopy.Position.X > HudPosition.X)
            {
                HudPosition.X++;
            }
        }

        internal void EnterVictoryMode(GameRoot game)
        {
            cameraCopy.LookAt(new Vector2(100, 2000));
        }
    }
}
