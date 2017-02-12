using System;
using Terraria;
using Terraria.ID;
using Expeditions;
using System.Collections.Generic;

namespace ExpeditionsContent.Quests.Daily
{
    class DyeForCyan : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Colors to Dye For";
            SetNPCHead(NPCID.DyeTrader);
            expedition.difficulty = 1;
            expedition.ctgCollect = true;
            expedition.ctgImportant = true;
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverable(ItemID.CyanGradientDye, 1);
            AddRewardItem(API.ItemIDExpeditionCoupon, 1);
        }
        public override string Description(bool complete)
        {
            return "I only sell high quality dyes, but I do love seeing the variety of wonderful colours this place has to offer once in a while! Show off your mixing skills, and present me a beautiful hue of cool cyans! ";
        }

        public override bool IncludeAsDaily()
        {
            return NPC.FindFirstNPC(NPCID.DyeTrader) >= 0;
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return API.IsDaily(expedition);
        }
    }
}