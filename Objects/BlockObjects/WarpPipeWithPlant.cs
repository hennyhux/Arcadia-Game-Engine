using GameSpace.Abstracts;
using GameSpace.Enums;
using GameSpace.Objects.EnemyObjects;
using Microsoft.Xna.Framework;

namespace GameSpace.Objects.BlockObjects
{
    public class WarpPipeWithPlant : Blocks
    {
        private readonly Plant plant;
        public WarpPipeWithPlant(Vector2 location)
        {
            ObjectID = (int)ItemID.WARPPIPEHEADWITHMOB;


        }

        public override bool RevealItem()
        {
            return true;
        }
    }


}
