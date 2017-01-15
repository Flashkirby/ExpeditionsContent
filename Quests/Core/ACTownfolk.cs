using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class ACTownfolk : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Townsfolk and You";
            SetNPCHead(NPCID.Guide);
            expedition.difficulty = 0;
            expedition.ctgExplore = true;

            expedition.conditionDescription1 = "House a Guide (me!)";
            expedition.conditionDescription2 = "House a Nurse";
            expedition.conditionDescription3 = "House a Merchant";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardMoney(Item.buyPrice(0, 0, 15, 0));
        }
        public override string Description(bool complete)
        {
            return "Well done! You've got your first townsfolk. If you want to expand, be sure to build more homes; people will not share rooms with each other. Keep in mind, townsfolk will usually look for something before moving in. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Only appears until first boss is beaten, or is done already
            if (!expedition.completed && !NPC.downedBoss1) return false;

            return API.FindExpedition<ABStartTown>(mod).completed;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Check if an NPC has a house every second
            if (!(cond1 && cond2 && cond3) // only satisfied if all 3 conditions are met at the same time
                && Main.time % 60 == 0)
            {
				bool guide = false;
				bool nurse = false;
				bool merch = false;
                for (int i = 0; i < 200; i++)
                {
                    if (!Main.npc[i].active || Main.npc[i].type == NPCID.OldMan) continue;
                    if (Main.npc[i].townNPC && !Main.npc[i].homeless)
                    {
						if (Main.npc[i].type == NPCID.Guide) guide = true;
                        if (Main.npc[i].type == NPCID.Nurse) nurse = true;
                        if (Main.npc[i].type == NPCID.Merchant) merch = true;
                    }
                }
                cond1 = guide;
                cond2 = nurse;
                cond3 = merch;
            }
            return cond1 && cond2 && cond3;
        }
    }
}
