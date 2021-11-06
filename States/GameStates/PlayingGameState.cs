using GameSpace.Camera2D;
using GameSpace.EntitiesManager;
using GameSpace.EntityManaging;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.Interfaces;
using GameSpace.Level;
using GameSpace.Machines;
using GameSpace.TileMapDefinition;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSpace.States.GameStates
{
    public class PlayingGameState : State
    {
        private protected readonly GraphicsDeviceManager graphics;
        private protected SpriteBatch spriteBatch;
        private readonly LevelRestart levelRestart;
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

        private Song song;

        public GraphicsDeviceManager Graphics => graphics;

        //private readonly string xmlFileName = "./Level1.xml"; // Turn in with this line of code!
        private readonly string xmlFileName = "../../../TileMapDefinition/Level1.xml"; // ONLY to run on our machines
        //private readonly string xmlFileName = "../../../TileMapDefinition/CalebTesting.xml";

        public PlayingGameState(GameRoot game, GraphicsDevice graphicsDevice ,ContentManager content) : base(game, graphicsDevice, content)
        {
            levelRestart = new LevelRestart(game, 0);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.CornflowerBlue);

            //Background/Scrolling Stuff
            foreach (Layer layer in layers)
            {
                layer.Draw(spriteBatch, camera.Position);
            }
            //Normal Sprites
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.GetViewMatrix(parallax));
            TheaterHandler.GetInstance().Draw(spriteBatch);
            HUDHandler.GetInstance().Draw(spriteBatch);
            spriteBatch.End();
        }

        public override void Restart(Vector2 position)
        {
            startOfGame = false;
            p = position;
            Initialize();
            spriteBatch = new SpriteBatch(graphicsDevice);

            #region Loading Factories
            SpriteBlockFactory.GetInstance().LoadContent(content);
            MarioFactory.GetInstance(game).LoadContent(content);
            SpriteEnemyFactory.GetInstance().LoadContent(content);
            SpriteExtraItemsFactory.GetInstance().LoadContent(content);
            BackgroundFactory.GetInstance().LoadContent(content);
            SpriteItemFactory.GetInstance().LoadContent(content);
            BackgroundFactory.GetInstance().LoadContent(content);
            AudioFactory.GetInstance().LoadContent(content);
            #endregion

            MusicHandler.GetInstance().LoadMusicIntoList(AudioFactory.GetInstance().loadList());

            #region Loading Lists
            objects = Loader.Load(game, xmlFileName, position, true);
            //soundEffects = AudioFactory.GetInstance().loadList();
            #endregion

            #region Loading Handlers
            //EntityManager.LoadList(objects);
            //MusicHandler.GetInstance().LoadMusicIntoList(soundEffects);
            HUDHandler.GetInstance().LoadContent(content);
            TheaterHandler.GetInstance().LoadData(objects, game);
            #endregion

            #region Loading Controllers
            controllers = new List<IController>()
            {
                new KeyboardInput(game), new ControllerInput(game)
            };
            #endregion
            fontFile = content.Load<SpriteFont>("font");
            //Camera Stuff
            camera = new Camera(graphicsDevice.Viewport) { Limits = new Rectangle(0, 0, Loader.boundaryX, 2000) };//Should be set to level's max X and Y

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

        public override void LoadContent()
        {
            spriteBatch = new SpriteBatch(graphicsDevice);

            HUDHandler.GetInstance().LoadContent(content);

            #region Loading Factories
            SpriteBlockFactory.GetInstance().LoadContent(content);
            MarioFactory.GetInstance(game).LoadContent(content);
            SpriteEnemyFactory.GetInstance().LoadContent(content);
            SpriteExtraItemsFactory.GetInstance().LoadContent(content);
            BackgroundFactory.GetInstance().LoadContent(content);
            SpriteItemFactory.GetInstance().LoadContent(content);
            BackgroundFactory.GetInstance().LoadContent(content);
            AudioFactory.GetInstance().LoadContent(content);
            #endregion

            MusicHandler.GetInstance().LoadMusicIntoList(AudioFactory.GetInstance().loadList());

            #region Loading Lists
            objects = Loader.Load(game, xmlFileName, new Vector2(0, 0), false);
            #endregion

            #region Loading Handlers
            //EntityManager.LoadList(objects);
            HUDHandler.GetInstance().LoadContent(content);
            TheaterHandler.GetInstance().LoadData(objects, game);
            #endregion

            #region Loading Controllers
            controllers = new List<IController>()
            {
                new KeyboardInput(game), new ControllerInput(game)
            };
            #endregion
            fontFile = content.Load<SpriteFont>("font");
            //Camera Stuff
            camera = new Camera(graphicsDevice.Viewport) { Limits = new Rectangle(0, 0, Loader.boundaryX, 480) };//Should be set to level's max X and Y

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

        public void Initialize()
        {
            LoadContent();
        }
        public void Reset()
        {
            startOfGame = true;
            Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (IController controller in controllers)
            {
                controller.Update();
            }

            TheaterHandler.GetInstance().Update(gameTime);
            //Camera Stuff- Centered Mario
            camera.LookAt(new Vector2(GetMario.Position.X + GetMario.CollisionBox.Width / 2, graphicsDevice.Viewport.Height / 2));
            CameraHandler.GetInstance().DebugCameraFindLimits();
            levelRestart.Restart(true);
            //timer = new DeathTimer(gameTime, fontFile);
            //timer.helper();
        }

    }



    public abstract class State
    {
        #region Fields

        protected ContentManager content;

        protected GraphicsDevice graphicsDevice;

        protected GameRoot game;

        #endregion

        #region Methods
        public abstract void LoadContent();

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public State(GameRoot game, GraphicsDevice graphicsDevice, ContentManager content)
        {
            this.game = game;

            this.graphicsDevice = graphicsDevice;

            this.content = content;
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Restart(Vector2 position);

        #endregion
    }
}
