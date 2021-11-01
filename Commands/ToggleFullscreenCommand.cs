
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameSpace
{
    public class ToggleFullscreenCommand : ICommand
    {
        private readonly GameRoot game;

        public ToggleFullscreenCommand(GameRoot game)
        {
            this.game = game;
        }

        public void Execute()
        {
            game.Graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            game.Graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            game.Graphics.ToggleFullScreen();
        }

        public void Unexecute()
        {
            throw new NotImplementedException();
        }
    }
}
