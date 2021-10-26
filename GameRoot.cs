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
using GameSpace.Sprites.Background;

namespace GameSpace
{
    public class GameRoot : Game
    {
        private protected readonly GraphicsDeviceManager graphics;
        private protected SpriteBatch spriteBatch;
        //private protected Camera camera;

        //Camera Stuff
        Camera camera;
        Vector2 parallax = new Vector2(1f);


        //Scrolling Background, Manually Setting
        private List<Layer> layers;
        // Background texture


        #region Lists
        private List<IController> controllers;
        private List<IGameObjects> objects;
        private List<IGameObjects> avatars;
        #endregion

        public Mario GetMario { get => (Mario)EntityManager.FindItem((int)AvatarID.MARIO);  }
        public GraphicsDeviceManager Graphics { get => graphics; }
        
        string xmlFileName = "../../../TileMapDefinition/HenryTestingDontEdit2.xml";
        //string xmlFileName = "../../../TileMapDefinition/JohnTestingDontEdit.xml";
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
            camera = new Camera(GraphicsDevice.Viewport) { Limits = new Rectangle(0, 0, 1100, 600) };//Should be set to level's max X and Y

            //Scrolling Background, Manually Setting
            //bg = Content.Load<Texture2D>("Background/Background");
            //bg = Content.Load<Texture2D>("Background/Mario_1_1_Background");
            //bg = Content.Load<Texture2D>("Background/Temp_BG");
            bg = Content.Load<Texture2D>("Background/small_BG");
            background = new BackgroundSprite(bg);
            EntityManager.AddCamera(camera);
            //MANUAL Stuff/ WILL BE CHANGED
            // Create 9 layers with parallax ranging from 0% to 100% (only horizontal)
            layers = new List<Layer>
            {
                new Layer(camera) { Parallax = new Vector2(2.0f, 1.0f) },
                new Layer(camera) { Parallax = new Vector2(1.5f, 1.0f) },
                new Layer(camera) { Parallax = new Vector2(1.0f, 1.0f) },
            };

            // Add a sprite to each layer
            layers[0].Sprites.Add(BackgroundFactory.GetInstance().CreateCloudsSprite());
            layers[1].Sprites.Add(BackgroundFactory.GetInstance().CreateBGMountainSprite());
            layers[2].Sprites.Add(BackgroundFactory.GetInstance().CreateRegularBackground());//Background layer

        }

        protected override void Update(GameTime gameTime)
        {
            foreach (IController controller in controllers) controller.Update();
            
            EntityManager.Update(gameTime);
            base.Update(gameTime);
            //Camera Stuff- Centered Mario
            camera.LookAt(new Vector2(GetMario.Position.X + GetMario.CollisionBox.Width/2, GraphicsDevice.Viewport.Height / 2));


        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Background/Scrolling Stuff
            foreach (Layer layer in layers)
                layer.Draw(spriteBatch, camera.Position);

            //Normal Sprites
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.GetViewMatrix(parallax));
            EntityManager.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
