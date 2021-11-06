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
        private LevelRestart levelRestart;
        private static Vector2 p;
        private static bool startOfGame;
        private SpriteFont fontFile;
        // DeathTimer timer;
        public Color FontColor { get; set; } = Color.DarkBlue;

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
        public List<SoundEffect> soundEffects;
        #endregion

        private State currentState;

        //Audio stuff
        private Song song;

        public GraphicsDeviceManager Graphics => graphicsDevice;

        //private readonly string xmlFileName = "./Level1.xml"; // Turn in with this line of code!
        private readonly string xmlFileName = "../../../TileMapDefinition/Level1.xml"; // ONLY to run on our machines
        //private readonly string xmlFileName = "../../../TileMapDefinition/CalebTesting.xml";
        public GameRoot()
        {
            graphicsDevice = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            levelRestart = new LevelRestart(this, 0);
        }

        protected override void Initialize()
        {
            base.Initialize();
            LoadContent();
        }
        public void Reset()
        {
            startOfGame = true;
            Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            currentState = new GameState(this, GraphicsDevice, Content);
            currentState.LoadContent();
        }

        public void Restart(Vector2 position)
        {
            startOfGame = false;
            p = position;
            Initialize();
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

            MusicHandler.GetInstance().LoadMusicIntoList(AudioFactory.GetInstance().loadList());

            #region Loading Lists
            objects = Loader.Load(this, xmlFileName, position, true);
            //soundEffects = AudioFactory.GetInstance().loadList();
            #endregion

            #region Loading Handlers
            //EntityManager.LoadList(objects);
            //MusicHandler.GetInstance().LoadMusicIntoList(soundEffects);
            HUDHandler.GetInstance().LoadContent(Content);
            TheaterHandler.GetInstance().LoadData(objects);
            #endregion

            #region Loading Controllers
            controllers = new List<IController>()
            {
                new KeyboardInput(this), new ControllerInput(this)
            };
            #endregion
            fontFile = Content.Load<SpriteFont>("font");
            //Camera Stuff
            camera = new Camera(GraphicsDevice.Viewport) { Limits = new Rectangle(0, 0, Loader.boundaryX, 2000) };//Should be set to level's max X and Y

            EntityManager.AddCamera(camera);
            CameraHandler.GetInstance().LoadCamera(camera);

            //Scrolling Background, Manually Setting
            layers = new List<Layer>
            {
                new Layer(camera, BackgroundFactory.GetInstance().CreateCloudsSprite(), new Vector2(2.0f, 1.0f)),
                new Layer(camera, BackgroundFactory.GetInstance().CreateBGMountainSprite(), new Vector2(1.5f, 1.0f)),
                new Layer(camera, BackgroundFactory.GetInstance().CreateRegularBackground(), new Vector2(1.0f, 1.0f)),
            };

            //Audio Stuff
            song = AudioFactory.GetInstance().CreateSong();
            MusicHandler.GetInstance().PlaySong(song);
        }

        public Mario GetMario => (Mario)FinderHandler.GetInstance().FindItem((int)AvatarID.MARIO);

        protected override void Update(GameTime gameTime)
        {
            currentState.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            currentState.Draw(gameTime, spriteBatch);
            base.Draw(gameTime);
        }
    }
}
