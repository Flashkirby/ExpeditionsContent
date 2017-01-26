using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.TravMerch
{
    class PrePair2BallOHurt : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Trading Ball O' Hurt";
            SetNPCHead(NPCID.TravellingMerchant);
            expedition.difficulty = 1;
            expedition.ctgCollect = true;
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverable(ItemID.GoldCoin);
            AddDeliverableAnyOf(new int[]{
                ItemID.TheRottedFork,
                ItemID.PanicNecklace,
                ItemID.CrimsonHeart,
            }, 1);

            AddRewardItem(ItemID.BallOHurt);
        }
        public override string Description(bool complete)
        {
            return "The ultimate man-handled wrecking ball! Smash your way through every obstacle, or wall off your foes from even touching you. Give it a whirl! ";
        }

        public override void OnNewDay(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            expedition.ResetProgress(true); //Reset after trade use
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Must have travelling merchant present
            if (NPC.FindFirstNPC(NPCID.TravellingMerchant) == -1) return false;

            return NPC.downedBoss1 && WorldGen.crimson;
        }
    }
}
