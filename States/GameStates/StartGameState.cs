using GameSpace.Abstracts;
using GameSpace.Commands;
using GameSpace.Handlers;
using GameSpace.Machines;
using GameSpace.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameSpace.States.GameStates
{

    public class CommandListStart
    {
        private readonly Dictionary<Keys, ICommand> keyboardCommands;
        private readonly Dictionary<Buttons, ICommand> controllerCommands;

        public CommandListStart(GameRoot game)
        {
            keyboardCommands = new Dictionary<Keys, ICommand>()
            {
                {Keys.Q, new ExitCommand(game)},
                {Keys.N, new StartNewGameCommand()},
                {Keys.OemTilde, new BringUpConsoleCommand(game)}
                //load game command
            };
        }
        public Dictionary<Keys, ICommand> GetCommand => keyboardCommands;
    }



    public class StartGameState : GameState
    {

        private List<IController> controllers;
        private readonly ScriptHandler scriptHandler;

        public StartGameState(GameRoot game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            Input = new InputHandler();
            scriptHandler = new ScriptHandler(this);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.Green);
            spriteBatch.Begin();
            HUDHandler.GetInstance().DrawStartingPanel(spriteBatch);
            spriteBatch.End();
        }

        public override void LoadContent()
        {
            HUDHandler.GetInstance().LoadContent(content, game);
            scriptHandler.Initialize();
            controllers = new List<IController>()
            {
                new KeyboardInput(game), new ControllerInput(game)
            };


        }

        public override void Restart()
        {

        }

        public override void Reset()
        {

        }

        public override void Update(GameTime gameTime)
        {
            foreach (IController controller in controllers)
            {
                controller.Update();
            }
        }
    }
}
