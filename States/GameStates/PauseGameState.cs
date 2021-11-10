using GameSpace.Commands;
using GameSpace.Machines;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameSpace.States.GameStates
{
    public class CommandListPause
    {
        private readonly Dictionary<Keys, ICommand> keyboardCommands;
        private readonly Dictionary<Buttons, ICommand> controllerCommands;
        private bool paused = false;

        public CommandListPause(GameRoot game)
        {
            keyboardCommands = new Dictionary<Keys, ICommand>()
            {
                {Keys.P, new PauseGameCommand(game, paused)},
            };
        }
        public Dictionary<Keys, ICommand> GetCommand => keyboardCommands;
    }

    public class PauseGameState : State
    {
        private List<IController> controllers;
        public PauseGameState(GameRoot game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {

        }

        public override void Reset()
        {

        }

        public override void Restart()
        {

        }
        public override void LoadContent()
        {
            HUDHandler.GetInstance().LoadContent(content, game);
            controllers = new List<IController>()
            {
                new KeyboardInput(game), new ControllerInput(game)
            };
        }

        public override void Update(GameTime gameTime)
        {

        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }
    }
}
