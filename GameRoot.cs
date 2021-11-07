using GameSpace.Camera2D;
using GameSpace.EntitiesManager;
using GameSpace.EntityManaging;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.Interfaces;
using GameSpace.Level;
using GameSpace.Machines;
using GameSpace.States.GameStates;
using GameSpace.TileMapDefinition;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

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
        public int marioLives = 3;
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

        public void RestartCurrentState()
        {
            currentState.Restart();
        }

        public void ResetCurrentState()
        {
            marioLives = 3;
            currentState.Reset();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            currentState = new StartGameState(this, GraphicsDevice, Content, marioLives);
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

        public void ChangeToPlayState()
        {
            nextState = new PlayingGameState(this, GraphicsDevice, Content, marioLives);
        }
    }
}
