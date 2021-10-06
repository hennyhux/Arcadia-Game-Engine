using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSpace
{
    public class KeyboardInput : IController
    {

        private KeyboardState previousState;
        private protected CommandList commands;
        private ICommand executeCommand;
        private Dictionary<Keys, ICommand> command;

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
                if(!previousState.IsKeyDown(key))
                {   
                    try
                    {
                        commands.GetCommand[key].Execute();
                    }
                    
                    catch(KeyNotFoundException e)
                    {
                        
                    }
                }
            }
            previousState = currentKeyboardState;
        }
    }
}
