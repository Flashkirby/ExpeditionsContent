using System;
using Terraria;
using Terraria.ID;
using Expeditions;
using System.Collections.Generic;

namespace ExpeditionsContent.Quests.Daily
{
    class SnapPreBoneSerpent : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Super Snap! Bone Serpent";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 3;
            expedition.ctgExplore = true;
            expedition.ctgCollect = true;
            expedition.ctgImportant = true;

            expedition.conditionDescription1 = "Return with a photo of the target's head";
            expedition.conditionDescription2 = "Return with a photo of the target's body";
            expedition.conditionDescription3 = "Return with a photo of the target's tail";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(API.ItemIDExpeditionCoupon);
            AddRewardItem(mod.ItemType<Items.QuestItems.PhotoBlank>(), 3);
        }
        public override string Description(bool complete)
        {
            return "There's a mail-in photo challenge happening on right now! Want to enter? Today's target is a Boneserpent which can be found far down in the underworld, and you have until tomorrow to submit. You'll need a photo of each part to qualify. Good luck!";
        }

        public override bool IncludeAsDaily()
        {
            return NPC.FindFirstNPC(ExpeditionC.NPCIDClerk) >= 0;
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return API.IsDaily(expedition) && (NPC.downedBoss1 || NPC.downedBoss2);
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            cond1 = PhotoManager.PhotoOfNPC[NPCID.BoneSerpentHead];
            cond2 = PhotoManager.PhotoOfNPC[NPCID.BoneSerpentBody];
            cond3 = PhotoManager.PhotoOfNPC[NPCID.BoneSerpentTail];
            return cond1 && cond2 && cond3;
        }

        public override void PreCompleteExpedition(List<Item> rewards, List<Item> deliveredItems)
        {
            PhotoManager.ConsumePhoto(NPCID.BoneSerpentHead);
            PhotoManager.ConsumePhoto(NPCID.BoneSerpentBody);
            PhotoManager.ConsumePhoto(NPCID.BoneSerpentTail);
        }
    }
}
