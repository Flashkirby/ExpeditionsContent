using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.TravMerch
{
    class PinkGel : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Trading Pink Gel";
            SetNPCHead(NPCID.TravellingMerchant);
            expedition.difficulty = 0;
            expedition.ctgCollect = true;
            expedition.repeatable = true;
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverable(ItemID.Gel, 100);
            AddDeliverable(ItemID.FallenStar, 5);
            AddDeliverable(ItemID.MushroomGrassSeeds, 3);

            AddRewardItem(ItemID.PinkGel, 30);
        }
        public override string Description(bool complete)
        {
            return "Pink gel! The new miracle cure. Unlike the gel from normal slimes, the product of a mutant pink slime has physics defying bounciness and supreme sweetness that would even make a bee blush! Yours for only a few meagre offerings! ";
        }

        public override void OnNewDay(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            expedition.ResetProgress(true); //Reset after trade use
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Must have travelling merchant present
            if (NPC.FindFirstNPC(NPCID.TravellingMerchant) == -1) return false;

            if (!cond1) cond1 = player.statDefense > 5;
            return cond1;
        }
    }
}
