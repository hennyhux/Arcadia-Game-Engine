using GameSpace.Abstracts;
using GameSpace.EntityManaging;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;

namespace GameSpace.Sprites.ExtraItems
{

    public class StateWarpPipeIdle : AbstractBlockStates
    {

    }
    public class WarpPipeHead : AbstractItem
    {
        private readonly bool hasCollided;
        public int TimesCollided { get; set; }
        public WarpPipeHead(Vector2 location)
        {
            ObjectID = (int)ItemID.WARPPIPE;
            Sprite = SpriteItemFactory.GetInstance().CreateWarpPipe();
            Position = location;
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);
            hasCollided = false;
            drawBox = false;
            TimesCollided = 0;

        }

        public override void HandleCollision(IGameObjects entity)
        {
            CollisionHandler.GetInstance().ItemToMarioCollison(this);
        }
    }

    public class WarpPipeHeadWithMob : AbstractItem
    {
        private IGameObjects mob;
        private bool itemRevealed;
        public WarpPipeHeadWithMob(Vector2 location)
        {
            ObjectID = (int)ItemID.WARPPIPEGOOMBA;
            Sprite = SpriteItemFactory.GetInstance().CreateWarpPipe();
            Position = location;
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);
            ExpandedCollisionBox = new Rectangle((int)Position.X - Sprite.Texture.Width * 5, (int)Position.Y, Sprite.Texture.Width * 10, Sprite.Texture.Height * 2);
            drawBox = false;
            itemRevealed = false;

        }
        public override void Update(GameTime gametime)
        {
            base.Update(gametime);
            if (!itemRevealed)
            {
                RevealItem();
            }
        }

        public override bool RevealItem()
        {
            if (!itemRevealed)
            {
                mob = ObjectFactory.GetInstance().CreateGoombaObject(new Vector2(Position.X - 4, Position.Y - Sprite.Texture.Height * 2 - 4));
                //TheaterHandler.GetInstance().AddItemToStage(mob);
                itemRevealed = true;
                return true;
            }

            return false;
        }

        public override void HandleCollision(IGameObjects entity)
        {
            CollisionHandler.GetInstance().ItemToMarioCollison(this);
        }
    }
}
