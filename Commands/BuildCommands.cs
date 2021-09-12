using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSpace
{
    public class BuildCommands
    {
        private Dictionary<Keys, ICommand> keyboardCommands;
        private Dictionary<Buttons, ICommand> controllerCommands;

        public BuildCommands(Game1 game)
        {
            keyboardCommands = new Dictionary<Keys, ICommand>()
            {
                {Keys.Q, new ExitCommand(game)},
                {Keys.Escape, new ExitCommand(game)},
                {Keys.F, new ToggleFullscreenCommand(game)},
                {Keys.W, new DrawSpritesCommand(game, 0)},
                {Keys.E, new DrawSpritesCommand(game, 1)},
                {Keys.T, new DrawSpritesCommand(game, 2)},
                {Keys.R, new DrawSpritesCommand(game, 3)}
            };

            controllerCommands = new Dictionary<Buttons, ICommand>()
            {
                {Buttons.Start, new ExitCommand(game)},
                {Buttons.DPadUp, new ToggleFullscreenCommand(game)},
                {Buttons.A, new DrawSpritesCommand(game, 0)},
                {Buttons.B, new DrawSpritesCommand(game, 1)},
                {Buttons.Y, new DrawSpritesCommand(game, 2)},
                {Buttons.X, new DrawSpritesCommand(game, 3)},
            };
        }

        public Dictionary<Keys, ICommand> GetCommand
        {
            get => keyboardCommands;
        }

        public Dictionary<Buttons, ICommand> GetControllerCommands
        {
            get => controllerCommands;
        }
    }
}
