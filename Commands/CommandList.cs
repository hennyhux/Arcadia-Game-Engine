using GameSpace.Commands;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameSpace
{
    public class CommandList
    {
        private readonly Dictionary<Keys, ICommand> keyboardCommands;
        private readonly Dictionary<Buttons, ICommand> controllerCommands;

        public CommandList(GameRoot game)
        {
            keyboardCommands = new Dictionary<Keys, ICommand>()
            {
                {Keys.Q, new ExitCommand(game)},
                {Keys.P, new PauseGameCommand(game)},
                {Keys.M, new MuteCommand(game)},
                {Keys.R, new ResetCommand(game)},
                {Keys.N, new StartNewGameCommand()},
                {Keys.X, new RestartCommand(game)},
                {Keys.F, new ToggleFullscreenCommand(game)},
                {Keys.D, new MoveRightCommand(game)},
                {Keys.Right, new MoveRightCommand(game)},
                {Keys.A, new MoveLeftCommand(game)},
                {Keys.Left, new MoveLeftCommand(game)},
                {Keys.W, new MoveUpCommand(game)},
                {Keys.Up, new MoveUpCommand(game)},
                {Keys.S, new MoveDownCommand(game)},
                {Keys.Down, new MoveDownCommand(game)},
                {Keys.Y, new StateStandardMarioCommand(game)},
                {Keys.U, new StateSuperMarioCommand(game)},
                {Keys.I, new StateFireMarioCommand(game)},
                {Keys.O, new StateDeadMarioCommand(game)},//DamageTransition
                {Keys.C, new ToggleCollisionBoxes()},
                {Keys.Space, new ThrowFireBallCommand(game)},
            };

            controllerCommands = new Dictionary<Buttons, ICommand>()
            {
                {Buttons.Start, new ExitCommand(game)},
                {Buttons.DPadLeft, new MoveLeftCommand(game)},
                {Buttons.DPadRight, new MoveRightCommand(game)},
                {Buttons.DPadDown, new MoveDownCommand(game)},
            };
        }

        public Dictionary<Keys, ICommand> GetCommand => keyboardCommands;

        public Dictionary<Buttons, ICommand> GetControllerCommands => controllerCommands;
    }
}
