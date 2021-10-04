using GameSpace.Factories;
using GameSpace.GameObjects;
using GameSpace.GameObjects.BlockObjects;
using GameSpace.GameObjects.ItemObjects;
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

        #region Object Factories
        private BlockObjectFactory blockObjectFactory;
        private MarioFactory marioFactory;
        private ItemObjectFactory itemObjectFactory;
        private EnemyObjectFactory enemyObjectFactory;
        #endregion

        #region Lists
        private List<IBlockObjects> blocks;
        private List<IEnemyObjects> enemies;
        private List<IController> controllers;
        private List<IItemObjects> items;
        private List<IGameObjects> objects;
        #endregion

        private Mario mario;

        public Mario GetMario { get => mario; }
        public GraphicsDeviceManager Graphics { get => graphics; }
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

            marioFactory = MarioFactory.GetInstance(this);
            enemyObjectFactory = EnemyObjectFactory.GetInstance();
            blockObjectFactory = BlockObjectFactory.GetInstance(this);
            itemObjectFactory = ItemObjectFactory.GetInstance(this);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            #region Loading Factories
            BlockSpriteFactory.GetInstance().LoadContent(Content);
            MarioFactory.GetInstance(this).LoadContent(Content);
            EnemySpriteFactory.GetInstance().LoadContent(Content);
            BackgroundFactory.GetInstance().LoadContent(Content);
            ItemSpriteFactory.GetInstance().LoadContent(Content);
            BackgroundFactory.GetInstance().LoadContent(Content);
            #endregion

            #region Loading Lists
            blocks = new List<IBlockObjects>()
            {
                blockObjectFactory.ReturnBrickBlockObject(), blockObjectFactory.ReturnStairBlockObject(),
                blockObjectFactory.ReturnFloorBlockObject(), blockObjectFactory.ReturnQuestionBlockObject(),
                blockObjectFactory.ReturnUsedBlockObject(), blockObjectFactory.ReturnHiddenBlockObject()
            };

            enemies = new List<IEnemyObjects>()
            {
                enemyObjectFactory.ReturnGoombaObject(), enemyObjectFactory.ReturnGreenKoopaObject(),
                enemyObjectFactory.ReturnRedKoopaObject()
            };

            items = new List<IItemObjects>()
            {
                itemObjectFactory.ReturnFireFlowerObject(), itemObjectFactory.ReturnOneUpShroomObject(), 
                itemObjectFactory.ReturnStarObject(), itemObjectFactory.ReturnSuperShroomObject(),
                itemObjectFactory.ReturnCoinObject()
            };

            objects = new List<IGameObjects>()
            {

            }
            #endregion

            mario = marioFactory.ReturnMario();
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

            foreach (IItemObjects item in items)
            {
                item.Update(gameTime);
            }

            mario.Update(gameTime);
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
                enemy.Draw(spriteBatch, new Vector2(0, 0));
            }

            foreach (IItemObjects item in items)
            {
                item.Draw(spriteBatch, new Vector2(0, 0));
            }

            mario.Draw(spriteBatch, new Vector2(500, 400));

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
