using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Daily
{
    class MerchFishbowl : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Fish for Thought";
            SetNPCHead(NPCID.Merchant);
            expedition.difficulty = 0;
            expedition.ctgCollect = true;
            expedition.ctgImportant = true;
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverable(ItemID.FishBowl, 3);
            AddRewardItem(API.ItemIDExpeditionCoupon, 1);
        }
        public override string Description(bool complete)
        {
            string clerk = NPC.GetFirstNPCNameOrNull(ExpeditionC.NPCIDClerk);
            if (clerk == "") clerk = "the clerk";
            return "Hey, you know what sells really well right now? Pet goldfish. Overseas that is, I'm not paying you any extra to get it for me. But I'll put in a word with " + clerk + ", how's that? Now get me a 3.";
        }

        public override bool IncludeAsDaily()
        {
            return NPC.FindFirstNPC(NPCID.Merchant) >= 0;
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return API.IsDaily(expedition);
        }
    }
}
