using System;
using Terraria;
using Terraria.ID;
using Expeditions;
using System.Collections.Generic;

namespace ExpeditionsContent.Quests.Daily
{
    class SnapHardDesertSpirit : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Super Snap! Desert Sprit";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 5;
            expedition.ctgExplore = true;
            expedition.ctgCollect = true;
            expedition.ctgImportant = true;

            expedition.conditionDescription1 = "Return with a photo of the target";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(API.ItemIDExpeditionCoupon);
        }
        public override string Description(bool complete)
        {
            return "There's a mail-in photo challenge happening on right now! Want to enter? Today's target is... a Medusa. Yeah, I know. You can find them in marble caves, and you have until tomorrow to submit. Good luck!";
        }

        public override bool IncludeAsDaily()
        {
            return NPC.FindFirstNPC(ExpeditionC.NPCIDClerk) >= 0 && Main.hardMode && !WorldGen.crimson;
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return API.IsDaily(expedition);
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            cond1 = PhotoManager.PhotoOfNPC[NPCID.Medusa];
            return cond1;
        }

        public override void PreCompleteExpedition(List<Item> rewards, List<Item> deliveredItems)
        {
            PhotoManager.ConsumePhoto(NPCID.Medusa);
        }
    }
}
