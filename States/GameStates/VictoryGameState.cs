using GameSpace.Commands;
using GameSpace.Machines;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameSpace.States.GameStates
{
    public class CommandListVictory
    {
        private readonly Dictionary<Keys, ICommand> keyboardCommands;
        private readonly Dictionary<Buttons, ICommand> controllerCommands;

        public CommandListVictory(GameRoot game)
        {
            keyboardCommands = new Dictionary<Keys, ICommand>()
            {
                {Keys.Q, new ExitCommand(game)},
                {Keys.R, new StartNewGameCommand()},
                //load game command
            };
        }
        public Dictionary<Keys, ICommand> GetCommand => keyboardCommands;
    }

    public class VictoryGameState : State
    {
        private List<IController> controllers;
        public VictoryGameState(GameRoot game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.BlueViolet);
            spriteBatch.Begin();

            HUDHandler.GetInstance().DrawEndingResetPanel(spriteBatch);
            spriteBatch.End();
        }

        public override void LoadContent()
        {
            HUDHandler.GetInstance().LoadContent(content, game);
            controllers = new List<IController>()
            {
                new KeyboardInput(game), new ControllerInput(game)
            };
        }

        public override void Reset()
        {

        }

        public override void Restart()
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
