using GameSpace.Abstracts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.Machines
{
    public class HUDHandler : AbstractHandler
    {
        SpriteFont HeadsUpDisplay;
        Vector2 HudPosition;

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
            HudPosition.X = 10;
            HudPosition.Y = 10;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.DrawString(HeadsUpDisplay, "Score: " + marioScores.ToString(), HudPosition, Color.Black);
            UpdateHudPosition();
        }

        private void UpdateHudPosition()
        {
            if (cameraCopy.Position.X > HudPosition.X) HudPosition.X++;
        }
    }
}
