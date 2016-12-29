using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsPlus.Tier0
{
    class MakingBase : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Setting up Base Camp";
            expedition.difficulty = 0;
            expedition.ctgCollect = true;
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(ItemID.Wood, 128);
            AddRewardItem(ItemID.GrayBrick, 64);
            AddRewardItem(ItemID.WorkBench);
            AddRewardItem(ItemID.Bottle);
            AddRewardItem(ItemID.Candle);
            AddRewardItem(ItemID.WoodenChair);
            AddRewardItem(ItemID.WoodenTable);
            AddRewardItem(ItemID.WoodenDoor, 2);
            AddRewardItem(ItemID.Furnace);
            AddRewardItem(ItemID.IronAnvil);
            AddRewardItem(ItemID.Sawmill);
            AddRewardItem(ItemID.Loom);
            AddRewardItem(ItemID.Bed);
            AddRewardItem(ItemID.GrandfatherClock);
            AddRewardItem(ItemID.Chest, 4);
            AddRewardItem(ItemID.Fireplace);
        }
        public override string Description(bool complete)
        {
            return "Someone's just left all these materials lying about, likely with intentions to set up a base camp. ";
        }


    }
}
