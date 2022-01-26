using GameSpace.Abstracts;
using GameSpace.EntityManaging;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.Sprites.ExtraItems
{

    public class StateWarpVineIdle : BlockState
    {
        public StateWarpVineIdle()
        {

        }
    }

    public class StateWarpVineActivated : BlockState
    {
        public StateWarpVineActivated()
        {

        }
    }

    public class StateWarpVineBodyIdle : BlockState
    {
        public StateWarpVineBodyIdle()
        {

        }
    }

    public class StateWarpVineDeactiveated : BlockState
    {
        public StateWarpVineDeactiveated()
        {

        }
    }

    public class WarpVine : Blocks
    {
        private readonly bool hasCollided;
        public int TimesCollided { get; set; }
        public WarpVine(Vector2 location)
        {
            ObjectID = (int)ItemID.VINE;
            Sprite = SpriteItemFactory.GetInstance().CreateVine();
            Position = location;
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);
            hasCollided = false;
            drawBox = false;
            TimesCollided = 0;
            state = new StateWarpVineIdle();
        }

        public override void HandleCollision(IGameObjects entity)
        {

            state = new StateWarpPipeDeactiveated();
            //CollisionHandler.GetInstance().ItemToMarioCollison(this);
        }

        public override bool RevealItem()
        {
            return false;
        }
    }

    public class WarpVineBlock : Blocks
    {
        private readonly IGameObjects mob;
        private readonly bool itemRevealed;
        public Rectangle ExpandedCollisionBox;
        public Rectangle InRangeCollisionBox;
        public WarpVineBlock(Vector2 location)
        {
            ObjectID = (int)ItemID.VINE;
            Sprite = SpriteItemFactory.GetInstance().CreateVine();
            Position = location;
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);
            ExpandedCollisionBox = new Rectangle((int)Position.X - Sprite.Texture.Width * 3, (int)Position.Y, Sprite.Texture.Width * 8, Sprite.Texture.Height * 2);
            InRangeCollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);
            drawBox = false;
            state = new StateWarpVineIdle();
            hasCollided = false;
            itemRevealed = false;
            mob = ObjectFactory.GetInstance().CreateVineObject(new Vector2(Position.X - 10, Position.Y - Sprite.Texture.Height * 3 + 2));
            TheaterHandler.GetInstance().QueueItemAddToStage(mob);
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            state.Draw(spritebatch, Position); //this shouldnt be hardcoded anymore 
            if (drawBox)
            {
                Sprite.DrawBoundary(spritebatch, CollisionBox);
                Sprite.DrawBoundary(spritebatch, ExpandedCollisionBox);
                Sprite.DrawBoundary(spritebatch, InRangeCollisionBox);
            }
        }

        public override void Update(GameTime gametime)
        {
            base.Update(gametime);
            if (FinderHandler.GetInstance().FindMarioPosition().X <= ExpandedCollisionBox.X ||
                FinderHandler.GetInstance().FindMarioPosition().X >= ExpandedCollisionBox.X + ExpandedCollisionBox.Width)
            {
                if (state is StateWarpPipeIdle)
                {
                    ShowItem();
                }
            }

            else
            {
                if (state is StateWarpPipeActivated)
                {
                    HideItem();
                }
            }

        }

        public override bool RevealItem()
        {
            //AnimationHandler.GetInstance().AddAnimation(new PlantComingOutOfPipe((AbstractEnemy)mob)); //later use 
            return false;
        }

        public void HideItem()
        {

        }

        public void ShowItem()
        {

        }

        /* public override void HandleCollision(IGameObjects entity)
         {
             switch (entity.ObjectID)
             {
                 case (int)AvatarID.MARIO:
                     CollisionHandler.GetInstance().ItemToMarioCollison(this);
                     break;
             }
         }
        */
    }


    public class WarpVineHeadRoom : Blocks
    {
        public int TimesCollided { get; set; }

        public WarpVineHeadRoom(Vector2 location)
        {

        }

        public override bool RevealItem()
        {
            return false;
        }

        /* public override void HandleCollision(IGameObjects entity)
         {
             switch (entity.ObjectID)
             {
                 case (int)AvatarID.MARIO:
                     CollisionHandler.GetInstance().ItemToMarioCollison(this);
                     break;
             }
         }
        */
    }

    public class WarpVineBody : Blocks
    {
        public int TimesCollided { get; set; }

        public WarpVineBody(Vector2 location)
        {

        }

        public override bool RevealItem()
        {
            return false;
        }

        public override void HandleCollision(IGameObjects entity)
        {
            switch (entity.ObjectID)
            {

            }
        }
    }

    public class WarpVineHeadBack : Blocks
    {
        private readonly bool hasCollided;
        public int TimesCollided { get; set; }
        public WarpVineHeadBack(Vector2 location)
        {
            ObjectID = (int)ItemID.WARPPIPEBACK;
            Sprite = SpriteItemFactory.GetInstance().CreateWarpPipe();
            Position = location;
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);
            hasCollided = false;
            drawBox = false;
            TimesCollided = 0;
            state = new StateWarpPipeIdle();

        }

        /* public override void HandleCollision(IGameObjects entity)
         {
             CollisionHandler.GetInstance().ItemToMarioCollison(this);
         } */

        public override bool RevealItem()
        {
            return false;
        }
    }

}

