using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Please note that I do not have a controller and cant test this code
namespace GameSpace
{
    public class ControllerInput : IController
    {
        private GamePadState previousState;
        private GamePadState emptyInput;
        private protected BuildCommands commands;

        public ControllerInput(Game1 game)
        {
            previousState = GamePad.GetState(PlayerIndex.One);
            emptyInput = new GamePadState();
            commands = new BuildCommands(game);
        }

        public void Update()
        {
            GamePadState currentGamePadState = GamePad.GetState(PlayerIndex.One);

            if (currentGamePadState.IsConnected)
            {
                if (currentGamePadState != emptyInput)
                {
                    var buttonList = (Buttons[])Enum.GetValues(typeof(Buttons));

                    foreach (var button in buttonList)
                    {
                        if (currentGamePadState.IsButtonDown(button) &&
                            !previousState.IsButtonDown(button))
                        {
                            try
                            {
                                commands.GetControllerCommands[button].Execute();
                            }

                            catch (KeyNotFoundException e)
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
