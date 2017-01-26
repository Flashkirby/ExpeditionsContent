using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.TravMerch
{
    class PrePair3CrimsonRod : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Trading Crimson Rod";
            SetNPCHead(NPCID.TravellingMerchant);
            expedition.difficulty = 1;
            expedition.ctgCollect = true;
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverable(ItemID.GoldCoin);
            AddDeliverableAnyOf(new int[]{
                ItemID.Vilethorn,
                ItemID.BallOHurt,
                ItemID.BandofStarpower,
            }, 1);

            AddRewardItem(ItemID.CrimsonRod);
        }
        public override string Description(bool complete)
        {
            return "A quality item for watching your own back - enemies beware. You'll be seeing silver linings in any fight with this magical rod, or your money back! You won't get your money back. ";
        }

        public override void OnNewDay(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            expedition.ResetProgress(true); //Reset after trade use
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Must have travelling merchant present
            if (NPC.FindFirstNPC(NPCID.TravellingMerchant) == -1) return false;

            return NPC.downedBoss1 && !WorldGen.crimson;
        }
    }
}
