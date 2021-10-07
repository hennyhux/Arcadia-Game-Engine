using GameSpace.Factories;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using GameSpace.EntitiesManager;
using System.Diagnostics;

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
        #endregion

        private Mario mario;

        public Mario GetMario { get => mario; }
        public GraphicsDeviceManager Graphics { get => graphics; }

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

            mario = marioFactory.ReturnMario();

        }

        protected override void Update(GameTime gameTime)
        {
            mario.Update(gameTime);

            foreach (IController controller in controllers) controller.Update();

            EntityManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(blendState: BlendState.AlphaBlend);

            EntityManager.Draw(spriteBatch);

            mario.Draw(spriteBatch, new Vector2(500, 400));
            
            //mario.sprite.Update(gameTime);
            //mario.sprite.Draw(spriteBatch, new Vector2(500, 400));
            //Debug.WriteLine("mario sprite address \n", *mario.sprite);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
