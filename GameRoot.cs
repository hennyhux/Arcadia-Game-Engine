using GameSpace.Camera2D;
using GameSpace.EntitiesManager;
using GameSpace.EntityManaging;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.Interfaces;
using GameSpace.Machines;
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
        private protected readonly GraphicsDeviceManager graphics;
        private protected SpriteBatch spriteBatch;

        //private protected Camera camera;

        //Camera Stuff
        private Camera camera;
        private Vector2 parallax = new Vector2(1f);


        //Scrolling Background, Manually Setting
        private List<Layer> layers;
        // Background texture


        #region Lists
        private List<IController> controllers;
        private List<IGameObjects> objects;
        private readonly List<IGameObjects> avatars;
        public List<SoundEffect> soundEffects;
        #endregion

        //Audio stuff
        private Song song;

        public GraphicsDeviceManager Graphics => graphics;

        //private readonly string xmlFileName = "./Level1.xml"; // Turn in with this line of code!
        //private readonly string xmlFileName = "../../../TileMapDefinition/Level1.xml"; // ONLY to run on our machines
        private readonly string xmlFileName = "../../../TileMapDefinition/HenryTesting.xml";
        public GameRoot()
        {
            graphics = new GraphicsDeviceManager(this);
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

            #region Loading Factories
            SpriteBlockFactory.GetInstance().LoadContent(Content);
            MarioFactory.GetInstance(this).LoadContent(Content);
            SpriteEnemyFactory.GetInstance().LoadContent(Content);
            SpriteExtraItemsFactory.GetInstance().LoadContent(Content);
            BackgroundFactory.GetInstance().LoadContent(Content);
            SpriteItemFactory.GetInstance().LoadContent(Content);
            BackgroundFactory.GetInstance().LoadContent(Content);
            AudioFactory.GetInstance().LoadContent(Content);
            #endregion

            #region Loading Lists
            objects = Loader.Load(xmlFileName);
            //objects = Loader.LoadEverything(xmlFileName);
            soundEffects = AudioFactory.GetInstance().loadList();
            MusicHandler.GetInstance().LoadMusicIntoList(soundEffects);
            #endregion

            #region Load EntityManager
            //EntityManager.LoadList(objects);
            TheaterHandler.GetInstance().LoadData(objects);
            #endregion

            #region Loading Controllers
            controllers = new List<IController>()
            {
                new KeyboardInput(this), new ControllerInput(this)
            };
            #endregion

            //Camera Stuff
            camera = new Camera(GraphicsDevice.Viewport) { Limits = new Rectangle(0, 0, Loader.boundaryX, 480) };//Should be set to level's max X and Y

            EntityManager.AddCamera(camera);
            CameraAgency.GetInstance().LoadCamera(camera);

            //Scrolling Background, Manually Setting
            layers = new List<Layer>
            {
                new Layer(camera, BackgroundFactory.GetInstance().CreateCloudsSprite(), new Vector2(2.0f, 1.0f)),
                new Layer(camera, BackgroundFactory.GetInstance().CreateBGMountainSprite(), new Vector2(1.5f, 1.0f)),
                new Layer(camera, BackgroundFactory.GetInstance().CreateRegularBackground(), new Vector2(1.0f, 1.0f)),
            };

            //Audio Stuff
            this.song = AudioFactory.GetInstance().CreateSong();
            MusicHandler.GetInstance().PlaySong(this.song);
        }

        public Mario GetMario => (Mario)FinderHandler.GetInstance().FindItem((int)AvatarID.MARIO);

        protected override void Update(GameTime gameTime)
        {
            foreach (IController controller in controllers)
            {
                controller.Update();
            }

            TheaterHandler.GetInstance().Update(gameTime);
            base.Update(gameTime);
            //Camera Stuff- Centered Mario
            camera.LookAt(new Vector2(GetMario.Position.X + GetMario.CollisionBox.Width / 2, GraphicsDevice.Viewport.Height / 2));
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Background/Scrolling Stuff
            foreach (Layer layer in layers)
            {
                layer.Draw(spriteBatch, camera.Position);
            }

            //Normal Sprites
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.GetViewMatrix(parallax));
            TheaterHandler.GetInstance().Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
