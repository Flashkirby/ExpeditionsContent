using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.TravMerch
{
    class PrePair3Vilethorn : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Trading Vilethorn";
            SetNPCHead(NPCID.TravellingMerchant);
            expedition.difficulty = 1;
            expedition.ctgCollect = true;
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverable(ItemID.GoldCoin);
            AddDeliverableAnyOf(new int[]{
                ItemID.CrimsonRod,
                ItemID.TheRottedFork,
                ItemID.PanicNecklace,
            }, 1);

            AddRewardItem(ItemID.Vilethorn);
        }
        public override string Description(bool complete)
        {
            return "For when you can't afford to take on your enemies directly, these nefarious plants creep even through walls! Take back control and fight your foes safely. ";
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
