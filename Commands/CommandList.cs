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
                {Keys.P, new PauseGameCommand(game)},
                {Keys.M, new MuteCommand(game)},
                {Keys.F, new ToggleFullscreenCommand(game)},
                {Keys.D, new MoveRightCommand(game)},
                {Keys.A, new MoveLeftCommand(game)},
                {Keys.W, new MoveUpCommand(game)},
                {Keys.S, new MoveDownCommand(game)},
                {Keys.K, new ChangeBlockCommand(game)},
                {Keys.L, new ShowHiddenBlockCommand(game)},
                {Keys.J, new ChangeQuestionBlockCommand(game)}
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
