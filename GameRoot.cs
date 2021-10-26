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
using GameSpace.Camera2D;
using System;
using System.Text;
using System.Linq;

namespace GameSpace
{
    public class GameRoot : Game
    {
        private protected readonly GraphicsDeviceManager graphics;
        private protected SpriteBatch spriteBatch;
        //private protected Camera camera;

        //Camera Stuff
        Camera camera;
        Vector2 parallax = new Vector2(0.1f);
        Vector2 parallax0 = new Vector2(0f);
        #region Lists
        private List<IController> controllers;
        private List<IGameObjects> objects;
        private List<IGameObjects> avatars;
        #endregion

        public Mario GetMario { get => (Mario)EntityManager.FindItem((int)AvatarID.MARIO);  }
        public GraphicsDeviceManager Graphics { get => graphics; }
        
        //string xmlFileName = "../../../TileMapDefinition/HenryTestingDontEdit.xml";
        string xmlFileName = "../../../TileMapDefinition/JohnTestingDontEdit.xml";
        public GameRoot()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
       
        SpriteBatch spriteBatch1;
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

            #region Loading Factories
            SpriteBlockFactory.GetInstance().LoadContent(Content);
            MarioFactory.GetInstance(this).LoadContent(Content);
            SpriteEnemyFactory.GetInstance().LoadContent(Content);
            SpriteExtraItemsFactory.GetInstance().LoadContent(Content);
            BackgroundFactory.GetInstance().LoadContent(Content);
            SpriteItemFactory.GetInstance().LoadContent(Content);
            BackgroundFactory.GetInstance().LoadContent(Content);
            #endregion

            #region Loading Lists
            objects = Loader.Load(xmlFileName);
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
            //Camera Stuff
            camera = new Camera(GraphicsDevice.Viewport);
            camera.Limits = (new Rectangle(0, 0, 1100, 480));//Should be set to level's max X and Y
            Debug.WriteLine("Viewport limits: {0}", camera.Limits);
            
        }

        protected override void Update(GameTime gameTime)
        {
            foreach (IController controller in controllers) controller.Update();
            
            EntityManager.Update(gameTime);
            base.Update(gameTime);
            //Camera Stuff- Centered Mario
            camera.LookAt(new Vector2(GetMario.Position.X + GetMario.CollisionBox.Width/2, GraphicsDevice.Viewport.Height / 2));
            //Debug.WriteLine("camera.Position {0}", camera.Position);
            //Debug.WriteLine("Mario.Position {0}", GetMario.Position);

        }

        protected override void Draw(GameTime gameTime)
        {
            Vector2 parallax = new Vector2(0.5f);
            Vector2 parallax0 = new Vector2(1f);

            GraphicsDevice.Clear(Color.CornflowerBlue);
            //Background/Scrolling Stuff
            spriteBatch.Begin(SpriteSortMode.Deferred, blendState: BlendState.AlphaBlend, null, null, null, null, camera.GetViewMatrix(parallax));

            /*foreach (IGameObjects obj in EntityManager.backgroundList)//Draw Background and clouds etc
            {
                obj.Draw(spriteBatch);
            }*/
            spriteBatch.End();

            //Normal Sprites
            spriteBatch.Begin(SpriteSortMode.Deferred, blendState: BlendState.AlphaBlend, null, null, null, null, camera.GetViewMatrix(parallax0));
            EntityManager.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);


        }
    }
}
