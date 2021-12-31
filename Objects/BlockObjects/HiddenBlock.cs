using GameSpace.Abstracts;
using GameSpace.EntityManaging;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace GameSpace.GameObjects.BlockObjects
{
    public class HiddenBlock : Block
    {
        public HiddenBlock(Vector2 initalPosition)
        {
            ObjectID = (int)BlockID.HIDDENBLOCK;
            state = new StateHiddenBlockIdleRedux(this);
            Position = initalPosition;
        }

        public override void Trigger()
        {
            state.Trigger();
        }
    }

    public class HiddenBlockWithVine : Block
    {
        private bool hasRevealedItem;
        public HiddenBlockWithVine(Vector2 initalPosition)
        {
            ObjectID = (int)BlockID.VINEHIDDENBLOCK;
            state = new HiddenVineBlockIdle(this);
            Position = initalPosition;
            hasRevealedItem = false;
            item = (Item)ObjectFactory.GetInstance().CreateVineObject(new Vector2(Position.X, Position.Y - 64));
        }

        public override void Trigger()
        {
            state.Trigger();
        }

        public override bool RevealItem()
        {
            if (!hasRevealedItem)
            {
                hasRevealedItem = true;
                TheaterHandler.GetInstance().AddItemToStage(item);
                int dist = (int)item.Position.Y;
                for (int i = 0; i < dist + 50; i = i + 50)
                {
                    //Debug.WriteLine("Y cord: {0}", item.Position.Y - i);
                    TheaterHandler.GetInstance().AddItemToStage((Item)ObjectFactory.GetInstance().CreateVineObject(new Vector2(item.Position.X, item.Position.Y - i)));
                }
            }
            return hasRevealedItem;
        }
    }

    public class HiddenVineBlockIdle : VineBlockStates
    {
        public HiddenVineBlockIdle(HiddenBlockWithVine block) : base(block)
        {
            StateSprite = SpriteBlockFactory.GetInstance().ReturnHiddenBlock();
        }

        public override void Trigger()
        {
            block.state = new HiddenVineBlockBumped(block);
            block.RevealItem();
        }
    }

    public class HiddenVineBlockBumped : VineBlockStates
    {
        public HiddenVineBlockBumped(HiddenBlockWithVine block) : base(block)
        {
            StateSprite = new BumpAnimation(SpriteBlockFactory.GetInstance().ReturnUsedBlock().Texture, (int)block.Position.X, (int)block.Position.Y, 24);
        }

        public override void Trigger()
        {

        }
    }

    public abstract class VineBlockStates : IBlockState
    {
        public ISprite StateSprite { get; set; }
        protected internal HiddenBlockWithVine block;

        protected VineBlockStates(HiddenBlockWithVine block)
        {
            this.block = block;
        }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            StateSprite.Draw(spriteBatch, location);
        }

        public virtual void DrawBounds(SpriteBatch spriteBatch, Rectangle CollisionBox)
        {
            StateSprite.DrawBoundary(spriteBatch, CollisionBox);
        }

        public virtual void Update(GameTime gameTime)
        {
            StateSprite.Update(gameTime);
            UpdateCollisionBox();
        }

        public abstract void Trigger();
        public virtual void RevealItem()
        {

        }

        protected internal virtual void UpdateCollisionBox()
        {
            block.CollisionBox = new Rectangle((int)block.Position.X, (int)block.Position.Y,
                StateSprite.Texture.Width * 2, StateSprite.Texture.Height * 2);
        }
    }
}
