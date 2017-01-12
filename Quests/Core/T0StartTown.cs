using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests
{
    class T0StartTown : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Starting A Town";
            expedition.difficulty = 0;
            expedition.ctgExplore = true;

            expedition.conditionDescription1 = "Attract someone to the town";
        }
        public override void AddItemsOnLoad()
        {
        }
        public override string Description(bool complete)
        {
            return "For people to move into our town, they will of course need a home. A room needs walls, a door, chair, table, and a light source. You can craft all of these at a workbench. ";
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Check if an NPC has a house every second
            if (!cond1 && Main.time % 60 == 0)
            {
                for (int i = 0; i < 200; i++)
                {
                    if (!Main.npc[i].active || Main.npc[i].type == NPCID.OldMan) continue;
                    if (Main.npc[i].townNPC && !Main.npc[i].homeless) cond1 = true;
                }
            }
            return cond1;
        }
    }
}
