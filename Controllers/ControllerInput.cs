using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

//Please note that I do not have a controller and cant test this code
namespace GameSpace
{
    public class ControllerInput : IController
    {
        private GamePadState previousState;
        private GamePadState emptyInput;
        private protected CommandList commands;

        public ControllerInput(GameRoot game)
        {
            previousState = GamePad.GetState(PlayerIndex.One);
            emptyInput = new GamePadState();
            commands = new CommandList(game);
        }

        public void Update()
        {
            GamePadState currentGamePadState = GamePad.GetState(PlayerIndex.One);

            if (currentGamePadState.IsConnected)
            {
                if (currentGamePadState != emptyInput)
                {
                    Buttons[] buttonList = (Buttons[])Enum.GetValues(typeof(Buttons));

                    foreach (Buttons button in buttonList)
                    {
                        if (currentGamePadState.IsButtonDown(button) &&
                            !previousState.IsButtonDown(button))
                        {
                            try
                            {
                                commands.GetControllerCommands[button].Execute();
                            }

                            catch (KeyNotFoundException)
                            {

                            }
                        }
                    }
                }
            }

            previousState = currentGamePadState;
        }
    }
}
