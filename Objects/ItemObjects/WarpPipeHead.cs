using GameSpace.Abstracts;
using GameSpace.Animations;
using GameSpace.EntityManaging;
using GameSpace.Enums;
using GameSpace.Factories;
using GameSpace.Interfaces;
using GameSpace.Machines;
using GameSpace.Objects.EnemyObjects;
using GameSpace.States;
using GameSpace.States.BlockStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSpace.Sprites.ExtraItems
{

    public class StateWarpPipeIdle : AbstractBlockStates
    {
        public StateWarpPipeIdle()
        {
            sprite = SpriteItemFactory.GetInstance().CreateWarpPipe();
        }
    }

    public class StateWarpPipeActivated : AbstractBlockStates
    {
        public StateWarpPipeActivated()
        {
            sprite = SpriteItemFactory.GetInstance().CreateWarpPipe();
        }
    }
    public class WarpPipeHead : AbstractItem
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

        }

        public override void HandleCollision(IGameObjects entity)
        {
            CollisionHandler.GetInstance().ItemToMarioCollison(this);
        }
    }

    public class WarpPipeHeadWithMob : AbstractBlock
    {
        private IGameObjects mob;
        private bool itemRevealed;
        public Rectangle ExpandedCollisionBox;
        public Rectangle InRangeCollisionBox;
        public WarpPipeHeadWithMob(Vector2 location)
        {
            ObjectID = (int)ItemID.WARPPIPEHEADWITHMOB;
            Sprite = SpriteItemFactory.GetInstance().CreateWarpPipe();
            Position = location;
            CollisionBox = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Texture.Width * 2, Sprite.Texture.Height * 2);
            ExpandedCollisionBox = new Rectangle((int)Position.X - Sprite.Texture.Width * 6, (int)Position.Y, Sprite.Texture.Width * 5, Sprite.Texture.Height * 2);
            InRangeCollisionBox = new Rectangle((int)Position.X - Sprite.Texture.Width * 2, (int)Position.Y, Sprite.Texture.Width * 5, Sprite.Texture.Height * 2);
            drawBox = false;
            state = new StateWarpPipeIdle();
            hasCollided = false;
            itemRevealed = false;
            mob = ObjectFactory.GetInstance().CreatePlantObject(new Vector2(Position.X - 10, Position.Y - Sprite.Texture.Height * 3 + 2));
            TheaterHandler.GetInstance().QueueItemAddToStage(mob);
            HideItem();

        }

        public override void Draw(SpriteBatch spritebatch)
        {
            state.Draw(spritebatch, Position); //this shouldnt be hardcoded anymore 
            if (drawBox)
            {
                Sprite.DrawBoundary(spritebatch, CollisionBox);
                Sprite.DrawBoundary(spritebatch, ExpandedCollisionBox);
               // Sprite.DrawBoundary(spritebatch, InRangeCollisionBox);
            }
        }

        public override void Update(GameTime gametime)
        {
            base.Update(gametime);
            if (FinderHandler.GetInstance().FindMarioPosition().X >= ExpandedCollisionBox.X)
            {
                if (state is StateWarpPipeIdle)ShowItem();
            }

            else
            {
                if (state is StateWarpPipeActivated) HideItem();
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
            RevealItem();
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

    public class WarpPipeHeadRoom : AbstractBlock
    {
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
}
