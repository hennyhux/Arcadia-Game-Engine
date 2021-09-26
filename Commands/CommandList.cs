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

                // Jump, crouch and dash/throw fireball
            };

            controllerCommands = new Dictionary<Buttons, ICommand>()
            {
                {Buttons.Start, new ExitCommand(game)},
                {Buttons.DPadUp, new ToggleFullscreenCommand(game)},
                
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
