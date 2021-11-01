using GameSpace.Factories;
using GameSpace.GameObjects.ItemObjects;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.States.ItemStates
{
    public class StateSuperShroomHidden : IItemStates
    {
        public ISprite StateSprite { get; set; }
        public bool CollidedWithMario { get; set; }
        public SuperShroom SuperShroom;

        public StateSuperShroomHidden(SuperShroom superShroom)
        {
            StateSprite = SpriteItemFactory.GetInstance().CreateSuperShroom();
            CollidedWithMario = false;
            SuperShroom = SuperShroom;
        }

        public void Draw(SpriteBatch spritebatch, Vector2 location)
        {
            //StateSprite.Draw(spritebatch, location);
        }

        public void Trigger()
        {
            // throw new NotImplementedException();
        }

        public void Update(GameTime gametime)
        {
            // StateSprite.Update(gametime);
        }
    }
}
