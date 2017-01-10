using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Tier0
{
    class BeaconOfPurity : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Beacon of Purity";
            expedition.difficulty = 0;
            expedition.ctgCollect = true;

            expedition.conditionDescription1 = "Discover a Living Tree";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(ItemID.WandofSparking);
            AddRewardItem(ItemID.LivingWoodChest);
        }
        public override string Description(bool complete)
        {
            return "You found a living tree. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond1)
            {
                API.TileCheckList.Add(TileID.LivingWood);
                int treeCount = API.CountTilesInCheckedOnScreen(TileID.LivingWood);
                if (treeCount > 64) cond1 = true;
            }
            return cond1;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return cond1;
        }
    }
}
