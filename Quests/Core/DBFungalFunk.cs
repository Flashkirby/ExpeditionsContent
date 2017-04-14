using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class DBFungalFunk : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Fun Guy";
            SetNPCHead(NPCID.Guide, false);
            expedition.difficulty = 7;
            expedition.ctgExplore = true;

            expedition.conditionDescription1 = "Discover a surface mushroom biome";
            expedition.conditionDescription2 = "House a truffle";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardMoney(Item.buyPrice(0, 3, 0, 0));
        }
        public override string Description(bool complete)
        {
            return "Did you know mushroom grass can grow on the surface? The easiest way would be to convert a section of the jungle. This will help protect it from the spread of other biomes, and may even a attract a Truffle should you build a house there. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // If not done by moonlord, we'll just hide it
            if (!expedition.completed && NPC.downedMoonlord) return false;

            if (!cond1)
            {
                cond3 = Main.hardMode && (player.ZoneGlowshroom || NPC.downedMechBossAny);
            }
            else
            {
                cond3 = true;
            }

            // Becomes available when a mushroom biome is visited during hardmode but really should be after a mech boss
            return cond3;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond1) cond1 = player.ZoneGlowshroom && player.ZoneOverworldHeight;
            // Check if an truffle has a house every second
            if (!cond2 && Main.time % 60 == 0)
            {
                for (int i = 0; i < 200; i++)
                {
                    if (!Main.npc[i].active || Main.npc[i].type == NPCID.OldMan) continue;
                    if (Main.npc[i].type == NPCID.Truffle && !Main.npc[i].homeless) cond2 = true;
                }
            }
            return cond1 && cond2;
        }
    }
}
