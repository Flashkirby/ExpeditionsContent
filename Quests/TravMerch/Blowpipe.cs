using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.TravMerch
{
    class Blowpipe : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Trading Blowpipe";
            SetNPCHead(NPCID.TravellingMerchant);
            expedition.difficulty = 0;
            expedition.ctgCollect = true;
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverable(ItemID.BreathingReed);

            AddRewardItem(ItemID.Blowpipe);
        }
        public override string Description(bool complete)
        {
            return "Searched as hard as you can, but can't find seeds anywhere? Maybe because you need a blowpipe! Simply having this wondrous item will make you gawp at all the details you've been missing. ";
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
