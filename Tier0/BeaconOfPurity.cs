using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsPlus.Tier0
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
            return "When I got posted here I was expecting rolling hills and stretches of untouched woodland, but a giant tree!? How exciting! ";
        }

        public override bool CheckPrerequisites(Player player)
        {
            if (!expedition.condition1Met)
            {
                Explorer.TileCheckList.Add(TileID.LivingWood);
                int treeCount = Explorer.CountTilesInChecked(TileID.LivingWood);
                if (treeCount > 64) return true;
            }
            return expedition.condition1Met;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            cond1 = true;
            return cond1;
        }
    }
}
