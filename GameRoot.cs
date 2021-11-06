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
        private protected readonly GraphicsDeviceManager graphicsDevice;
        private protected SpriteBatch spriteBatch;


        //HERE ARE THE VERY IMPORTANT CHANGES: ADDED NEW GAME STAE
        private State currentState;
        private State nextState;

        public GraphicsDeviceManager Graphics => graphicsDevice;
        public Mario GetMario => (Mario)FinderHandler.GetInstance().FindItem((int)AvatarID.MARIO);

        public GameRoot()
        {
            graphicsDevice = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            TheaterHandler.GetInstance().CopyGameRoot(this);
        }

        protected override void Initialize()
        {
            base.Initialize();
            LoadContent();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            currentState = new StartGameState(this, GraphicsDevice, Content);
            currentState.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (nextState != null)
            {
                currentState = nextState;
                currentState.LoadContent();
                nextState = null;
            }

            currentState.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            currentState.Draw(gameTime, spriteBatch);
            base.Draw(gameTime);
        }

        #region Game State
        public void ChangeToPlayState()
        {
            nextState = new PlayingGameState(this, GraphicsDevice, Content);
        }
        public void RestartCurrentState(Vector2 position)
        {
            currentState.Restart(position);
        }

        public void ResetCurrentState()
        {
            currentState.Initialize();
        }

        #endregion
    }
}
