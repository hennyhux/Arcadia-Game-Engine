using GameSpace.EntityManaging;
using GameSpace.Enums;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.States.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace
{
    public class GameRoot : Game
    {
        private protected GraphicsDeviceManager graphicsDevice;
        private protected SpriteBatch spriteBatch;

        public State CurrentState { get; private set; }
        private State nextState;

        public GraphicsDeviceManager Graphics => graphicsDevice;
        public Mario GetMario => (Mario)FinderHandler.GetInstance().FindItem((int)AvatarID.MARIO);
        public int marioLives = 3;
        public GameRoot()
        {
            graphicsDevice = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            TheaterHandler.GetInstance().InitializeGameroot(this);
        }

        protected override void Initialize()
        {
            base.Initialize();
            LoadContent();
        }

        public void RestartCurrentState()
        {
            CurrentState.Restart();
        }

        public void ResetCurrentState()
        {
            marioLives = 3;
            CurrentState.Reset();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            CurrentState = new StartGameState(this, GraphicsDevice, Content, marioLives);
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
            nextState = new PlayingGameState(this, GraphicsDevice, Content, marioLives);
        }

        public void ChangeToVictoryState()
        {

        }
    }
}
