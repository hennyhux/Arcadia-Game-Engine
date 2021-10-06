
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSpace
{
    public class ToggleFullscreenCommand : ICommand
    {
        private GameRoot game;

        public ToggleFullscreenCommand(GameRoot game)
        {
            this.game = game;
        }

        public void Execute()
        {
            this.game.Graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            this.game.Graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            this.game.Graphics.ToggleFullScreen();           
        }

        public void Unexecute()
        {
            throw new NotImplementedException();
        }
    }
}
