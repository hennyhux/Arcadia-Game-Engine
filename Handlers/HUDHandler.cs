using GameSpace.Abstracts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.Machines
{
    public static class DeathTimer
    {
        public static long timer;
        public static long ticks;
        public static long seconds;
        public static long ticksMax = 4000000000;
        public static long convertToSeconds = 10000000;

        public static void ResetTimer()
        {
            timer = 0;
        }

        public static void UpdateTimer(GameTime game, int numberOfLives)
        {
            timer += game.ElapsedGameTime.Ticks;
            ticks = ticksMax - timer;
            seconds = ticks / convertToSeconds;
            if (seconds == 0)
            {
                numberOfLives--;
                ResetTimer();
            }
        }

        public static void Draw(SpriteBatch spriteBatch, SpriteFont font, Vector2 location)
        {
            spriteBatch.DrawString(font, "Time\n " + seconds, location, Color.DarkBlue);
        }
    }
    public class HUDHandler : AbstractHandler
    {
        private SpriteFont HeadsUpDisplay;
        private Texture2D chungus;
        private Vector2 HudPosition;

        private static readonly HUDHandler instance = new HUDHandler();
        public static HUDHandler GetInstance()
        {
            return instance;
        }

        private HUDHandler()
        {

        }

        public void LoadContent(ContentManager content)
        {
            HeadsUpDisplay = content.Load<SpriteFont>("font");
            chungus = content.Load<Texture2D>("Background/Untitled");
            HudPosition.X = 10;
            HudPosition.Y = 10;

        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.DrawString(HeadsUpDisplay, "Score: " + mario.score.ToString(), HudPosition, Color.Black);
            spritebatch.DrawString(HeadsUpDisplay, "Mario Lives: " + mario.marioLives.ToString(), new Vector2(HudPosition.X, HudPosition.Y + 20), Color.Black);
            DeathTimer.Draw(spritebatch, HeadsUpDisplay, new Vector2(HudPosition.X, HudPosition.Y + 40));
            DeathTimer.UpdateTimer(internalGametime, mario.marioLives);
            UpdateHudPosition();
        }

        public void DrawStartingPanel(SpriteBatch spritebatch)
        {
            spritebatch.DrawString(HeadsUpDisplay, "PRESS N FOR NEW GAME", HudPosition, Color.Black);
            spritebatch.DrawString(HeadsUpDisplay, "PRESS L TO LOAD GAME (FUTURE FEATURE)", new Vector2(HudPosition.X, HudPosition.Y + 40), Color.Black);
            spritebatch.Draw(chungus, new Vector2(HudPosition.X, HudPosition.Y + 200), Color.White);
            
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
