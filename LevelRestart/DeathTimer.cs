using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.Level
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

        public static void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.DrawString(font, "Time\n " + seconds, new Vector2(100, 0), Color.DarkBlue);
        }
    }
}
