using GameSpace.EntityManaging;
using GameSpace.States.GameStates;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameSpace
{
    public class KeyboardInput : IController
    {

        private KeyboardState previousState;
        private protected CommandList commands;
        private protected CommandListStart commandsStart;
        private CommandListVictory commandsVictory;
        private CommandListGameOver commandsGame;
        private readonly ICommand executeCommand;
        private readonly Dictionary<Keys, ICommand> command;

        public KeyboardInput(GameRoot game)
        {
            previousState = new KeyboardState();

            commands = new CommandList(game);

            commandsVictory = new CommandListVictory(game);

            commandsStart = new CommandListStart(game);

          commandsGame = new CommandListGameOver(game);


        }

        // due to the lack of command design this is smelly... I smell the smelly smell 
        public void Update()
        {
            KeyboardState currentKeyboardState = Keyboard.GetState();
            Keys[] keysPressed = currentKeyboardState.GetPressedKeys();
            GameRoot game = FinderHandler.GetInstance().FindGameRoot();

            foreach (Keys key in keysPressed)
            {
                if (!previousState.IsKeyDown(key))
                {
                    try
                    {
                        if (game.CurrentState is PlayingGameState) commands.GetCommand[key].Execute();

                        else if (game.CurrentState is StartGameState) commandsStart.GetCommand[key].Execute();

                        else if (game.CurrentState is VictoryGameState) commandsVictory.GetCommand[key].Execute();

                        else if (game.CurrentState is GameOverState) commandsGame.GetCommand[key].Execute();

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
