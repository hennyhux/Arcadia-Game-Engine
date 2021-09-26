using GameSpace.Factories;
using GameSpace.GameObjects;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameSpace
{
    public class Game1 : Game
    {
        private protected readonly GraphicsDeviceManager graphics;
        private protected SpriteBatch spriteBatch;
        private List<IController> controllers;
        private BlockSpriteFactory blockFactory;
        private MarioFactory marioFactory;
        private EnemySpriteFactory enemyFactory;
        private BackgroundFactory backgroundFactory;
        private BlockObjectFactory blockObjectFactory;
        private EnemyObjectFactory enemyObjectFactory;
        private List<IBlockObjects> blocks;
        private List<IEnemyObjects> enemies;

        private ISprite MarioSprite;
        private ISprite Background;

        public ISprite GetMarioSprite { get => MarioSprite; }
        public GraphicsDeviceManager Graphics { get => graphics; }
        public BlockSpriteFactory BlockFactory { get => blockFactory; }
        public EnemySpriteFactory EnemySpriteFactory { get => enemyFactory; }
        public List<IBlockObjects> Blocks { get => blocks; }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            controllers = new List<IController>()
            {
                new KeyboardInput(this), new ControllerInput(this)
            };

            blockFactory = new BlockSpriteFactory();
            marioFactory = new MarioFactory();
            enemyFactory = new EnemySpriteFactory();
            enemyObjectFactory = new EnemyObjectFactory(this);
            backgroundFactory = new BackgroundFactory();
            blockObjectFactory = new BlockObjectFactory(this);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            blockFactory.LoadContent(Content);
            marioFactory.LoadContent(Content);
            enemyFactory.LoadContent(Content);
            backgroundFactory.LoadContent(Content);

            blocks = new List<IBlockObjects>()
            {
                blockObjectFactory.ReturnBrickBlockObject(), blockObjectFactory.ReturnStairBlockObject(),
                blockObjectFactory.ReturnFloorBlockObject(), blockObjectFactory.ReturnQuestionBlockObject(),
                blockObjectFactory.ReturnUsedBlockObject(), blockObjectFactory.ReturnHiddenBlockObject()
            };

            enemies = new List<IEnemyObjects>()
            {
                enemyObjectFactory.ReturnGoombaObject(this)
            };

            MarioSprite = marioFactory.ReturnMarioStandingLeftSprite();
            Background = backgroundFactory.ReturnRegularBackground();

        }

        protected override void Update(GameTime gameTime)
        {
            foreach (IController controller in controllers)
            {
                controller.Update();
            }

            foreach (IBlockObjects block in blocks)
            {
                block.Update(gameTime);
            }

            foreach (IEnemyObjects enemy in enemies)
            {
                enemy.Update(gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(blendState: BlendState.AlphaBlend);
            
            foreach (IBlockObjects block in blocks)
            {
                block.Draw(spriteBatch, new Vector2(0, 0));
            }

            foreach (IEnemyObjects enemy in enemies)
            {
                enemy.Draw(spriteBatch, new Vector2(300, 150));
            }

            MarioSprite.Draw(spriteBatch, new Vector2(500, 200));
              
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
