using GameSpace.Abstracts;
using GameSpace.Commands;
using GameSpace.EntityManaging;
using GameSpace.Enums;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.States.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QuakeConsole;

namespace GameSpace
{
    public class GameRoot : Game
    {
        private protected GraphicsDeviceManager graphicsDevice;
        private protected SpriteBatch spriteBatch;
        public ConsoleComponent console;
        public GameState CurrentState { get; private set; }
        private GameState nextState;
        public GraphicsDeviceManager Graphics => graphicsDevice;
        public Mario GetMario => (Mario)FinderHandler.GetInstance().FindItem((int)AvatarID.MARIO);
        public GameRoot()
        {
            graphicsDevice = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            TheaterHandler.GetInstance().InitializeGameroot(this);
            console = new ConsoleComponent(this);
            var interpreter = new RoslynInterpreter();
            console.Interpreter = interpreter;
            interpreter.AddVariable("GameRoot", this);
            Components.Add(console);
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            base.Initialize();
        }

        public void RestartCurrentState()
        {
            CurrentState.Restart();
        }

        public void ResetCurrentState()
        {
            CurrentState.Reset();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            CurrentState = new StartGameState(this, GraphicsDevice, Content);
            CurrentState.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (nextState != null)
            {
                CurrentState = nextState;
                CurrentState.LoadContent();
                nextState = null;
            }
            CurrentState.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            CurrentState.Draw(gameTime, spriteBatch);
            base.Draw(gameTime);
        }

        public void ChangeToPlayState()
        {
            nextState = new PlayingGameState(this, GraphicsDevice, Content);
        }

        public void ChangeToVictoryState()
        {
            nextState = new VictoryGameState(this, GraphicsDevice, Content);
        }

        public void GameOverState()
        {
            nextState = new GameOverState(this, GraphicsDevice, Content);
        }
    }
}
