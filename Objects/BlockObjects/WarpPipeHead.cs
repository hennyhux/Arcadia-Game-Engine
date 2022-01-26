using GameSpace.Abstracts;
using GameSpace.EntityManaging;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using GameSpace.Objects.EnemyObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.Sprites.ExtraItems
{

    public class StateWarpPipeIdle : BlockState
    {
        public StateWarpPipeIdle()
        {
            StateSprite = SpriteItemFactory.GetInstance().CreateWarpPipe();
        }
    }

    public class StateWarpPipeActivated : BlockState
    {
        public StateWarpPipeActivated()
        {
            StateSprite = SpriteItemFactory.GetInstance().CreateWarpPipe();
        }
    }

    public class StateWarpPipeBodyIdle : BlockState
    {
        public StateWarpPipeBodyIdle()
        {
            StateSprite = SpriteBlockFactory.GetInstance().CreateWarpPipeBody();
        }
    }

    public class StateWarpPipeDeactiveated : BlockState
    {
        public StateWarpPipeDeactiveated()
        {
            StateSprite = SpriteItemFactory.GetInstance().CreateWarpPipe();
        }
    }

    public class WarpPipeHead : Blocks
    {
        private readonly bool hasCollided;
        public int TimesCollided { get; set; }
        public WarpPipeHead(Vector2 location)
        {
            ObjectID = (int)ItemID.WARPPIPEHEAD;
            Sprite = SpriteItemFactory.GetInstance().CreateWarpPipe();
            Position = location;
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);
            hasCollided = false;
            drawBox = false;
            TimesCollided = 0;
            state = new StateWarpPipeIdle();
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

    public class WarpPipeHeadMob : Blocks
    {
        private readonly IGameObjects mob;
        private readonly bool itemRevealed;
        public Rectangle ExpandedCollisionBox;
        public Rectangle InRangeCollisionBox;
        public WarpPipeHeadMob(Vector2 location)
        {
            ObjectID = (int)ItemID.WARPPIPEHEADWITHMOB;
            Sprite = SpriteItemFactory.GetInstance().CreateWarpPipe();
            Position = location;
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);
            ExpandedCollisionBox = new Rectangle((int)Position.X - Sprite.Texture.Width * 3, (int)Position.Y, Sprite.Texture.Width * 8, Sprite.Texture.Height * 2);
            InRangeCollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);
            drawBox = false;
            state = new StateWarpPipeIdle();
            hasCollided = false;
            itemRevealed = false;
            mob = ObjectFactory.GetInstance().CreatePlantObject(new Vector2(Position.X - 10, Position.Y - Sprite.Texture.Height * 3 + 2));
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
            Plant castedMob = (Plant)mob;
            castedMob.Hide();
            state = new StateWarpPipeIdle();
        }

        public void ShowItem()
        {
            Plant castedMob = (Plant)mob;
            castedMob.Show();
            state = new StateWarpPipeActivated();
        }

        public override void HandleCollision(IGameObjects entity)
        {
            switch (entity.ObjectID)
            {
                case (int)AvatarID.MARIO:
                    CollisionHandler.GetInstance().ItemToMarioCollison(this);
                    break;
            }
        }
    }


    public class WarpPipeHeadRoom : Blocks
    {
        public int TimesCollided { get; set; }

        public WarpPipeHeadRoom(Vector2 location)
        {
            ObjectID = (int)ItemID.WARPPIPEROOM;
            Sprite = SpriteItemFactory.GetInstance().CreateWarpPipe();
            Position = location;
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);
            drawBox = false;
            state = new StateWarpPipeIdle();
        }

        public override bool RevealItem()
        {
            return false;
        }

        public override void HandleCollision(IGameObjects entity)
        {
            switch (entity.ObjectID)
            {
                case (int)AvatarID.MARIO:
                    CollisionHandler.GetInstance().ItemToMarioCollison(this);
                    break;
            }
        }
    }

    public class WarpPipeBody : Blocks
    {
        public int TimesCollided { get; set; }

        public WarpPipeBody(Vector2 location)
        {
            ObjectID = (int)ItemID.WARPPIPEBODY;
            Sprite = SpriteBlockFactory.GetInstance().CreateWarpPipeBody();
            Position = location;
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);
            drawBox = false;
            state = new StateWarpPipeBodyIdle();
        }

        public override bool RevealItem()
        {
            return false;
        }

        public override void HandleCollision(IGameObjects entity)
        {
            switch (entity.ObjectID)
            {
                case (int)AvatarID.MARIO:
                    CollisionHandler.GetInstance().BlockToMarioCollision(this);
                    break;
            }
        }
    }

    public class WarpPipeHeadBack : Blocks
    {
        private readonly bool hasCollided;
        public int TimesCollided { get; set; }
        public WarpPipeHeadBack(Vector2 location)
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

        public override void HandleCollision(IGameObjects entity)
        {
            CollisionHandler.GetInstance().ItemToMarioCollison(this);
        }

        public override bool RevealItem()
        {
            return false;
        }
    }

}
