using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameSpace
{
    public class KeyboardInput : IController
    {

        private KeyboardState previousState;
        private protected CommandList commands;
        private readonly ICommand executeCommand;
        private readonly Dictionary<Keys, ICommand> command;

        public KeyboardInput(GameRoot game)
        {
            previousState = new KeyboardState();
            commands = new CommandList(game);
        }

        public void Update()
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();
            Keys[] keysPressed = currentKeyboardState.GetPressedKeys();

            foreach (Keys key in keysPressed)
            {
                if (!previousState.IsKeyDown(key))
                {
                    try
                    {
                        commands.GetCommand[key].Execute();
                    }

                    catch (KeyNotFoundException)
                    {

                    }
                }
            }
            previousState = currentKeyboardState;
        }
    }
}
