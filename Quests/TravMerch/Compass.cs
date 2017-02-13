using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.TravMerch
{
    class Compass : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Trading Compass";
            SetNPCHead(NPCID.TravellingMerchant);
            expedition.difficulty = 1;
            expedition.ctgCollect = true;
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverable(ItemID.Hook, 1);

            AddRewardItem(ItemID.Compass);
        }
        public override string Description(bool complete)
        {
            return "For all you precision fanatics, do I have a deal for you! Unlike a measly old map this tool will give you HARD NUMBERS and RELIABLE FACTS to tell you exactly where you are. ";
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
