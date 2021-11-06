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

        public PlayingGameState(GameRoot game, GraphicsDevice graphicsDevice ,ContentManager content, int marioLives, bool startOfGame) : base(game, graphicsDevice, content, marioLives, startOfGame)
        {
            levelRestart = new LevelRestart(game, 0);
        }

        public Mario GetMario => (Mario)FinderHandler.GetInstance().FindItem((int)AvatarID.MARIO);

        public override void LoadContent()
        {
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
            if (this.start)
            {
                objects = Loader.Load(game, xmlFileName, new Vector2(0, 0), true);
            }
            else
            {
                objects = Loader.Load(game, xmlFileName, levelRestart.GetPosition(), false);
            }
            #endregion

            #region Loading Handlers
            HUDHandler.GetInstance().LoadContent(content, this.lives, game);
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

        public override void Update(GameTime gameTime)
        {
            this.start = false;
            HUDHandler.GetInstance().LoadGameTime(gameTime);
            HUDHandler.GetInstance().UpdateTimer();
            foreach (IController controller in controllers)
            {
                controller.Update();
            }
            TheaterHandler.GetInstance().Update(gameTime);
            //Camera Stuff- Centered Mario
            camera.LookAt(new Vector2(GetMario.Position.X + GetMario.CollisionBox.Width / 2, graphicsDevice.Viewport.Height / 2));
            CameraHandler.GetInstance().DebugCameraFindLimits();
            levelRestart.Restart();
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
    }



    public abstract class State
    {
        #region Fields

        protected ContentManager content;

        protected GraphicsDevice graphicsDevice;

        protected GameRoot game;

        protected int lives;

        protected bool start;

        #endregion

        #region Methods
        public abstract void LoadContent();

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public State(GameRoot game, GraphicsDevice graphicsDevice, ContentManager content, int marioLives, bool startOfGame)
        {
            this.game = game;

            this.graphicsDevice = graphicsDevice;

            this.content = content;

            this.lives = marioLives;

            this.start = startOfGame;
        }

        public abstract void Update(GameTime gameTime);


        #endregion
    }
}
