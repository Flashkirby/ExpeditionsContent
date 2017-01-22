using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Clerk
{
    class BeaconOfPurity : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Beacon of Purity";
            SetNPCHead(ExpeditionC.NPCIDClerk);
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
            return "You found a what? A giant tree? That's awesome! I can tell you know I didn't expect something like that when I came here, no siree. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond1)
            {
                cond1 = (Main.screenTileCounts[TileID.LivingWood] > 128);
            }
            return cond1 && WorldExplorer.savedClerk;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return cond1;
        }
    }
}
