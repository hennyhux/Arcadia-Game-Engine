using GameSpace.Commands;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSpace
{
    public class CommandList
    {
        private Dictionary<Keys, ICommand> keyboardCommands;
        private Dictionary<Buttons, ICommand> controllerCommands;

        public CommandList(Game1 game)
        {
            keyboardCommands = new Dictionary<Keys, ICommand>()
            {
                {Keys.Q, new ExitCommand(game)},
                {Keys.Escape, new ExitCommand(game)},
                {Keys.F, new ToggleFullscreenCommand(game)},
                {Keys.D, new MoveRightCommand(game)},
                {Keys.A, new MoveLeftCommand(game)},
                {Keys.W, new MoveUpCommand(game)},
                {Keys.S, new MoveDownCommand(game)}
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
