using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.MiscPre
{
    class MakingBase : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Setting up Base Camp";
            expedition.difficulty = 0;
            expedition.ctgCollect = true;
            expedition.repeatable = true;
        }
        public override void AddItemsOnLoad()
        {
            expedition.AddDeliverable(ItemID.GoldCoin, 5);

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
            return @"Rich? Impatient? Strangers asking you for residency? Well look no further, do we have a once in a lifetime limited time offer deal for you! For one time only*, all your problems** can be solved right now!
*Valid only in " + Main.worldName + @". 
** Not guaranteed to be all/any of your problems. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // LIMITED TIME OFFER™
            return !Main.expertMode && !Main.hardMode && !NPC.downedBoss2 && player.statLifeMax >= 200;
        }
    }
}
