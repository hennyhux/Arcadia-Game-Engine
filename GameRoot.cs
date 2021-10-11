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

        #region Object Factories
        private ObjectFactory objectFactory;
        private MarioFactory marioFactory;
        #endregion

        #region Lists
        private List<IController> controllers;
        private List<IGameObjects> objects;
        private List<IGameObjects> obj;
        private List<IGameObjects> avatars;
        #endregion

        private Mario mario;

        //public Mario GetMario { get => mario; }
        public Mario GetMario { get => (Mario)EntityManager.FindItem((int)AvatarID.MARIO); }
        public GraphicsDeviceManager Graphics { get => graphics; }

        //IF RUNNING IT WITH TILE MAP DEFINITION, copy the full path of Level.xml and paste it in the next line
        //YOU MIGHT HAVE TO SWITCH THE '\' CHARACHTERS TO  '/'
        string xmlFileName = "C:/Users/Henry/Source/Repos/Team_Arcadia/TileMapDefinition/Level.xml";

        //HENRY'S PATH: "C:/Users/Henry/Source/Repos/Team_Arcadia/TileMapDefinition/Level.xml"
        //BETO'S PATH: "C:/Users/alber/Source/Repos/Team_Arcadia/TileMapDefinition/Level.xml"
        //JOHN's PATH: "C:/Users/18473/source/repos/Team_Arcadia_Sprint1 - Copy/TileMapDefinition/Level.xml"
        //CALEB'S PATH: "C:/Users/CalebHernandez/source/repos/Team_Arcadia/TileMapDefinition/Level.xml"
        public GameRoot()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            marioFactory = MarioFactory.GetInstance(this);
            objectFactory = ObjectFactory.GetInstance();
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
            objects = new List<IGameObjects>()
            {
                objectFactory.CreateBrickBlockObject(new Vector2(100, 100)), objectFactory.CreateStairBlockObject(new Vector2(200, 100)),
                objectFactory.CreateFloorBlockObject(new Vector2(300, 100)), objectFactory.CreateQuestionBlockObject(new Vector2(400, 100)),
                objectFactory.CreateUsedBlockObject(new Vector2(500, 100)), objectFactory.CreateHiddenBlockObject(new Vector2(600, 100)),
             
                objectFactory.CreateGoombaObject(new Vector2(200, 300)), objectFactory.CreateGreenKoopaObject(new Vector2(300, 300)),
                objectFactory.CreateRedKoopaObject(new Vector2(400, 300)),

                objectFactory.CreateCoinObject(new Vector2(100, 200)), objectFactory.CreateStarObject(new Vector2(100, 250)),
                objectFactory.CreateFireFlowerObject(new Vector2(100, 300)), objectFactory.CreateSuperShroomObject(new Vector2(100, 350)),
                objectFactory.CreateOneUpShroomObject(new Vector2(100, 400))

            };


            obj = Loader.Load(xmlFileName);

            #endregion

            #region Load EntityManager
            //EntityManager.LoadList(objects);
            //IF RUNNING IT WITH TILE MAP DEFINITION, UNCOMMENT THE NEXT LINE
            EntityManager.LoadList(obj);
            


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
