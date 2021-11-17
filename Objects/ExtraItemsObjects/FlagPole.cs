using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using GameSpace.Machines;
using GameSpace.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameSpace.GameObjects.ExtraItemsObjects
{
    public class FlagPole : IGameObjects
    {
        public ISprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Rectangle CollisionBox { get; set; }
        public Vector2 Acceleration { get; set; }

        public Vector2 Location => throw new NotImplementedException();

        public int ObjectID { get; set; }
        private bool hasCollided;
        private bool drawBox;

        public FlagPole(Vector2 initalPosition)
        {
            ObjectID = (int)ItemID.FLAGPOLE;
            Sprite = SpriteExtraItemsFactory.GetInstance().ReturnFlagPole();
            Position = initalPosition;
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2 / 5, Sprite.Texture.Height * 2);
            //Debug.WriteLine("FlagPoleX: {0}, Y:{1}", Sprite.Texture.Width * 2 / 5, Sprite.Texture.Height * 2);
            drawBox = false;
            //Debug.WriteLine("EXTRA ITEM AT " + "(" + Position.X + ", " + Position.Y + ")");
            hasCollided = false;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            if (hasCollided)//Drawing the flag getting take down
            {
                Sprite.Draw(spritebatch, Position); //this shouldnt be hardcoded anymore
            }
            else//Flag stays at top of the pole, not animated
            {
                ((FlagPoleSprite)Sprite).Draw(spritebatch, Position, false);
            }

            if (drawBox)
            {
                Sprite.DrawBoundary(spritebatch, CollisionBox);
            }
        }

        public void Update(GameTime gametime)
        {
            if (hasCollided && Acceleration.X > 0) // Flag lowering once mario touches flag
            {
                ((FlagPoleSprite)Sprite).Update(gametime, hasCollided);
                Acceleration -= new Vector2(1 * (float)gametime.ElapsedGameTime.TotalSeconds, 0);
            }
            else if (hasCollided)//The Flag Animation has played so, go to victory
            {
                MarioHandler.GetInstance().EnterVictoryPanel();
            }

        }

        public void Trigger()
        {
            Acceleration = new Vector2((float)2.5, 0);
            //Flag does nothing.
        }

        public void UpdateCollisionBox(Vector2 location, GameTime gameTime)
        {
            //Pipe doesn't move.
        }

        public void HandleCollision(IGameObjects entity)
        {
            switch (entity.ObjectID)
            {
                case (int)AvatarID.MARIO:
                    hasCollided = true;
                    CollisionBox = new Rectangle(0, 0, 0, 0);
                    Trigger();
                    break;
            }

        }

        public void ToggleCollisionBoxes()
        {
            drawBox = !drawBox;
        }

        public bool IsCurrentlyColliding()
        {
            throw new NotImplementedException();
        }

        public bool RevealItem()
        {
            throw new NotImplementedException();
        }

    }
}
