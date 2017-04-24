using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.MiscHard
{
    class PirateSextant : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Captain and Crew";
            SetNPCHead(NPCID.Pirate);
            expedition.difficulty = 6;
            expedition.ctgSlay = true;
            expedition.partyShare = true;

            expedition.conditionDescription1 = "Survive a Pirate Invasion without losing anyone";
            expedition.conditionDescription2 = "Prevent any town deaths";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(ItemID.Sextant, 1);
        }
        public override string Description(bool complete)
        {
            if(complete)
            {
                return "Arr, ye be a fine captain. It's an honour to work with ye. ";
            }
            return "Good captains commandeer a mighty crew. A great captain has the powers to look after their own. Prove to me that ye be a great captain against me old crew, and I'll share with you a little old treasure of mine, ahar!";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return Main.hardMode;
        }

        public override void OnAnyNPCDeath(NPC npc, Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Oh no, a friend died. Oh well, better luck next time.
            if (cond3)
            {
                if (npc.townNPC && npc.friendly)
                {
                    expedition.condition2Met = false;
                }
            }
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // On a blood moon, when you have 6 ore more NPCs, being quest
            if (Main.invasionType == InvasionID.PirateInvasion
                && !expedition.condition3Met
                && !expedition.condition1Met)
            {
                int townieCount = 0;
                for (int i = 0; i < 200; i++)
                {
                    if (!Main.npc[i].active || Main.npc[i].type == NPCID.OldMan) continue;
                    if (Main.npc[i].townNPC && !Main.npc[i].homeless) townieCount++;
                }
                if (townieCount > 5) // 5 NPCs + Pirate. Not too hard to get.
                {
                    expedition.condition2Met = true;
                    expedition.condition3Met = true;
                }
            }

            if(Main.invasionProgress >= Main.invasionProgressMax - 1)
            {
                expedition.condition3Met = false;
                // If player prevented any town deaths, huzzah!
                if (!expedition.condition1Met && expedition.condition2Met)
                {
                    expedition.condition1Met = true;
                }
            }

            return cond1;
        }
    }
}
