using System;
using Terraria;
using Terraria.ID;
using Expeditions;
using System.Collections.Generic;

namespace ExpeditionsContent.Quests.Daily
{
    class SnapPreBee : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Super Snap! Bee";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 3;
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
            return "There's a mail-in photo challenge happening on right now! Want to enter? Today's target is a Bee which can be found deep in the jungle, and you have until tomorrow to submit. Good luck!";
        }

        public override bool IncludeAsDaily()
        {
            return NPC.FindFirstNPC(ExpeditionC.NPCIDClerk) >= 0;
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return API.IsDaily(expedition) && NPC.downedBoss1 || NPC.downedBoss2;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            cond1 = 
                PhotoManager.PhotoOfNPC[NPCID.Bee] ||
                PhotoManager.PhotoOfNPC[NPCID.BeeSmall] ||
                PhotoManager.PhotoOfNPC[NPCID.QueenBee];
            return cond1;
        }

        public override void PreCompleteExpedition(List<Item> rewards, List<Item> deliveredItems)
        {
            if (!PhotoManager.ConsumePhoto(NPCID.Bee))
            {
                if (!PhotoManager.ConsumePhoto(NPCID.BeeSmall))
                {
                    PhotoManager.ConsumePhoto(NPCID.QueenBee);
                }
            }
        }
    }
}
