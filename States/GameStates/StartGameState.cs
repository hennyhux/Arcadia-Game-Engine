using GameSpace.Commands;
using GameSpace.Machines;
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
                //load game command
            };
        }
        public Dictionary<Keys, ICommand> GetCommand => keyboardCommands;
    }


    public class StartGameState : State
    {

        private readonly Texture2D menuBackGroundTexture;
        private List<IController> controllers;

        public StartGameState(GameRoot game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {

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
            HUDHandler.GetInstance().LoadContent(content, marioLives, game);
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
