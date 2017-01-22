using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.TravMerch
{
    class PrePair2TheRottedFork : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Trading The Rotted Fork";
            SetNPCHead(NPCID.TravellingMerchant);
            expedition.difficulty = 1;
            expedition.ctgCollect = true;
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverable(ItemID.GoldCoin);
            AddDeliverableAnyOf(new int[]{
                ItemID.BallOHurt,
                ItemID.BandofStarpower,
                ItemID.ShadowOrb,
            }, 1);

            AddRewardItem(ItemID.TheRottedFork);
        }
        public override string Description(bool complete)
        {
            return "For anyone that prefers precision pointed piercing pikes - this is your calling! Safely poke your foes from a distance, and push back even the largest crowd of unruly zombies. ";
        }

        public override void OnNewDay()
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
