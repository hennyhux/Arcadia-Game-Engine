using GameSpace.Machines;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameSpace.States.GameStates
{
    public abstract class Component
    {

    }

    public class StartGameState : State
    {
        private readonly List<Component> components;

        private readonly Texture2D menuBackGroundTexture;
        private List<IController> controllers;



        public StartGameState(GameRoot game, GraphicsDevice graphicsDevice, ContentManager content, int lives) : base(game, graphicsDevice, content, lives)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            HUDHandler.GetInstance().DrawStartingPanel(spriteBatch);
            spriteBatch.End();
        }

        public override void LoadContent()
        {
            HUDHandler.GetInstance().LoadContent(content, marioLives, game);
            controllers = new List<IController>()
            {
                new KeyboardInput(game), new ControllerInput(game)
            };
        }

        public override void Restart()
        {

        }
        public override void Reset()
        {

        }

        public override void Update(GameTime gameTime)
        {
            foreach (IController controller in controllers)
            {
                controller.Update();
            }
        }
    }
}
