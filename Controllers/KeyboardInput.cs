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
        private protected BuildCommands commands;

        public KeyboardInput(Game1 game)
        {
            previousState = new KeyboardState();
            commands = new BuildCommands(game);
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
