using GameSpace.Factories;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using GameSpace.EntitiesManager;
using GameSpace.TileMapDefinition;
using System.Diagnostics;
using GameSpace.Enums;

namespace GameSpace
{
    public class GameRoot : Game
    {
        private protected readonly GraphicsDeviceManager graphics;
        private protected SpriteBatch spriteBatch;

        #region Lists
        private List<IController> controllers;
        private List<IGameObjects> objects;
        private List<IGameObjects> avatars;
        #endregion

        public Mario GetMario { get => (Mario)EntityManager.FindItem((int)AvatarID.MARIO);  }
        public GraphicsDeviceManager Graphics { get => graphics; }

        string xmlFileName = "../../../TileMapDefinition/Testing.xml";
        public GameRoot()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            #region Loading Factories
            SpriteBlockFactory.GetInstance().LoadContent(Content);
            MarioFactory.GetInstance(this).LoadContent(Content);
            SpriteEnemyFactory.GetInstance().LoadContent(Content);
            BackgroundFactory.GetInstance().LoadContent(Content);
            SpriteItemFactory.GetInstance().LoadContent(Content);
            BackgroundFactory.GetInstance().LoadContent(Content);
            #endregion

            #region Loading Lists
            //objects = Loader.Load(xmlFileName);
            objects = Loader.LoadEverything("../../../TileMapDefinition/Level1.xml");
            #endregion

            #region Load EntityManager
            EntityManager.LoadList(objects);

            #endregion

            #region Loading Controllers
            controllers = new List<IController>()
            {
                new KeyboardInput(this), new ControllerInput(this)
            };
            #endregion

            avatars = Loader.LoadAvatars(xmlFileName);
            foreach(IGameObjects avatar in avatars)
            {
                EntityManager.AddEntity(avatar);
            }

        }

        protected override void Update(GameTime gameTime)
        {
            foreach (IController controller in controllers) controller.Update();
            EntityManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(blendState: BlendState.AlphaBlend);
            EntityManager.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
