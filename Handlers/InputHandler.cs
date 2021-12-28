using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GameSpace.Handlers
{
    public class InputHandler
    {
        KeyboardState previousKeyboardState;
        public ICommand ExitCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        public ICommand NewGameCommand { get; set; }

        public InputHandler()
        {
            previousKeyboardState = Keyboard.GetState();
        }

        public void UpdateInput()
        {
            // Get the current gamepad state.
            KeyboardState currentState = Keyboard.GetState();

            Keys[] keysPressed = currentState.GetPressedKeys();
            foreach (Keys key in keysPressed)
                if (!previousKeyboardState.IsKeyDown(key))
                {
                    switch (key)
                    {
                        case Keys.Q:
                            if (ExitCommand != null)
                                ExitCommand.Execute();
                            break;

                        case Keys.R:
                            if (ResetCommand != null)
                                ResetCommand.Execute();
                            break;

                        case Keys.N:
                            if (ResetCommand != null)
                                NewGameCommand.Execute();
                            break;

                        default:
                            Debug.WriteLine(key.ToString() + " Pressed!");
                            break;
                    }
                }
            // Update previous gamepad state.
            previousKeyboardState = currentState;
        }

    }
}

