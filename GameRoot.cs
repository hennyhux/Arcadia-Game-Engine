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
        public List<SoundEffect> soundEffects;


        //HERE ARE THE VERY IMPORTANT CHANGES: ADDED NEW GAME STAE
        private State currentState;
        private State nextState;

        public GraphicsDeviceManager Graphics => graphicsDevice;
        public Mario GetMario => (Mario)FinderHandler.GetInstance().FindItem((int)AvatarID.MARIO);


        public GameRoot()
        {
            graphicsDevice = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            LoadContent();
        }
        public void Reset()
        {
            Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            currentState = new PlayingGameState(this, GraphicsDevice, Content);
            currentState.LoadContent();
        }

        public void Restart(Vector2 position)
        {
            currentState.Restart(position);
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

        public void ChangeState(State state)
        {
            nextState = state;
        }
    }
}
