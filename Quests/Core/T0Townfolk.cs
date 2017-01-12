using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests
{
    class T0Townfolk : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Townsfolk and You";
            expedition.difficulty = 0;
            expedition.ctgExplore = true;

            expedition.conditionDescription1 = "House a Guide (me!)";
            expedition.conditionDescription2 = "House a Nurse";
            expedition.conditionDescription3 = "House a Merchant";
        }
        public override void AddItemsOnLoad()
        {
        }
        public override string Description(bool complete)
        {
            return "Well done! You've got your first townsfolk. If you want to expand, be sure to build more homes; people will not share rooms with each other. Keep in mind, townsfolk will usually look for something before moving in. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return API.FindExpedition(mod, "T0StartTown").completed;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Check if an NPC has a house every second
            if (!(cond1 && cond2 && cond3)
                && Main.time % 60 == 0)
            {
                for (int i = 0; i < 200; i++)
                {
                    if (!Main.npc[i].active || Main.npc[i].type == NPCID.OldMan) continue;
                    if (Main.npc[i].townNPC && !Main.npc[i].homeless)
                    {
                        if (Main.npc[i].type == NPCID.Guide) cond1 = true;
                        if (Main.npc[i].type == NPCID.Nurse) cond2 = true;
                        if (Main.npc[i].type == NPCID.Merchant) cond3 = true;
                    }
                }
            }
            return cond1;
        }
    }
}
