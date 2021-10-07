using GameSpace.Commands;
using GameSpace.EntitiesManager;
using GameSpace.Enums;
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

        public CommandList(GameRoot game)
        {
            keyboardCommands = new Dictionary<Keys, ICommand>()
            {
                {Keys.Q, new ExitCommand(game)},
                {Keys.P, new PauseGameCommand(game)},
                {Keys.M, new MuteCommand(game)},
                {Keys.F, new ToggleFullscreenCommand(game)},
                {Keys.D, new MoveRightCommand(game)},
                {Keys.Right, new MoveRightCommand(game)},
                {Keys.A, new MoveLeftCommand(game)},
                {Keys.Left, new MoveLeftCommand(game)},
                {Keys.W, new MoveUpCommand(game)},
                {Keys.Up, new MoveUpCommand(game)},
                //{Keys.W, new MoveUpCommand(EntityManager.FindBlock((int)BLOCKID.BRICKBLOCK))},
               // {Keys.S, new MoveDownCommand(game)},
                {Keys.S, new MoveDownCommand(game)},
                {Keys.Down, new MoveDownCommand(game)},
                {Keys.Y, new StateStandardMarioCommand(game)},
                {Keys.U, new StateSuperMarioCommand(game)},
                {Keys.I, new StateFireMarioCommand(game)},
                {Keys.O, new StateDeadMarioCommand(game)},
                {Keys.B, new ChangeBlockCommand(EntityManager.FindBlock((int)BLOCKID.BRICKBLOCK))},
                {Keys.H, new ShowHiddenBlockCommand(EntityManager.FindBlock((int)BLOCKID.HIDDENBLOCK))},
                {Keys.OemQuestion, new ChangeQuestionBlockCommand(EntityManager.FindBlock((int)BLOCKID.QUESTIONBLOCK))},
                {Keys.C, new ToggleCollisionBoxes()}

                // Jump, crouch and dash/throw fireball
            };

            controllerCommands = new Dictionary<Buttons, ICommand>()
            {
                {Buttons.Start, new ExitCommand(game)},
                {Buttons.DPadLeft, new MoveLeftCommand(game)},
                {Buttons.DPadRight, new MoveRightCommand(game)},
                {Buttons.DPadDown, new MoveDownCommand(game)},
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
