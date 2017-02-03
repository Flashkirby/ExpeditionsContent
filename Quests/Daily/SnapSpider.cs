using System;
using Terraria;
using Terraria.ID;
using Expeditions;
using System.Collections.Generic;

namespace ExpeditionsContent.Quests.Daily
{
    class SnapSpider : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Super Snap! Wall Creeper";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 1;
            expedition.ctgExplore = true;
            expedition.ctgCollect = true;
            expedition.ctgImportant = true;

            expedition.conditionDescription1 = "Return with a photo of the target";
        }
        public override void AddItemsOnLoad()
        {
            if (Main.hardMode)
            { expedition.name = "Super Snap! Black Recluse"; }
            else
            { expedition.name = "Super Snap! Wall Creeper"; }

            AddRewardItem(API.ItemIDExpeditionCoupon);
        }
        public override string Description(bool complete)
        {
            if(Main.hardMode)
            {
                return "There's a mail-in photo challenge happening on right now! Want to enter? Today's target is a Black Recluse which can be found creeping about in spider caves, and you have until tomorrow to submit. Good luck!";
            }
            return "There's a mail-in photo challenge happening on right now! Want to enter? Today's target is a Wall Creeper which can be found skittering about in spider caves, and you have until tomorrow to submit. Good luck!";
        }

        public override bool IncludeAsDaily()
        {
            return NPC.FindFirstNPC(ExpeditionC.NPCIDClerk) >= 0;
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return API.IsDaily(expedition);
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            cond1 =
                PhotoManager.PhotoOfNPC[NPCID.WallCreeper] ||
                PhotoManager.PhotoOfNPC[NPCID.WallCreeperWall] ||
                PhotoManager.PhotoOfNPC[NPCID.BlackRecluse] ||
                PhotoManager.PhotoOfNPC[NPCID.BlackRecluseWall];
            return cond1;
        }

        public override void PreCompleteExpedition(List<Item> rewards, List<Item> deliveredItems)
        {
            if (!PhotoManager.ConsumePhoto(NPCID.BlackRecluseWall))
            {
                if (!PhotoManager.ConsumePhoto(NPCID.BlackRecluse))
                {
                    if (!PhotoManager.ConsumePhoto(NPCID.WallCreeperWall))
                    {
                        PhotoManager.ConsumePhoto(NPCID.WallCreeper);
                    }
                }
            }
        }
    }
}
