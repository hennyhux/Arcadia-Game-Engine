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
                //load game command
            };
        }
        public Dictionary<Keys, ICommand> GetCommand => keyboardCommands;
    }



    public class StartGameState : GameState
    {

        private readonly Texture2D menuBackGroundTexture;
        private List<IController> controllers;
        private ScriptHandler scriptHandler;
        private List<Component> componentsList;

        public StartGameState(GameRoot game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            Input = new InputHandler();
            scriptHandler = new ScriptHandler(this);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.Green);
            spriteBatch.Begin();
            foreach (var component in componentsList)
                component.Draw(gameTime, spriteBatch);
            HUDHandler.GetInstance().DrawStartingPanel(spriteBatch);
            spriteBatch.End();
        }

        public override void LoadContent()
        {
            HUDHandler.GetInstance().LoadContent(content, game);
            scriptHandler.Initialize();
            //controllers = new List<IController>()
            //{
            //    new KeyboardInput(game), new ControllerInput(game)
            //};

            var startButton = new Button(content.Load<Texture2D>("Button"), content.Load<SpriteFont>("font"))
            {
                Position = new Vector2(350, 200),
                Text = "Start"
            };

            var quitButton = new Button(content.Load<Texture2D>("Button"), content.Load<SpriteFont>("font"))
            {
                Position = new Vector2(350, 250),
                Text = "Quit"
            };

            startButton.Click += StartButton_Click;
            quitButton.Click += QuitButton_Click;

            componentsList = new List<Component>()
            {
                startButton,
                quitButton,
            };
        }

        private void QuitButton_Click(object sender, System.EventArgs e)
        {
            game.Exit();
        }

        private void StartButton_Click(object sender, System.EventArgs e)
        {
            
        }

        public override void Restart()
        {

        }

        public override void Reset()
        {

        }

        public override void Update(GameTime gameTime)
        {
            //foreach (IController controller in controllers)
            //{
            //    controller.Update();
            //}

            foreach (var component in componentsList)
                component.Update(gameTime);
        }
    }
}
