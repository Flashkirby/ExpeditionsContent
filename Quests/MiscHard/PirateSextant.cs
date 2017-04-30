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
            // expedition.conditionDescription3 = "pirate invasion start";
            // expedition.conditionDescriptionCountable = "Have at least 5 other people in town";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(ItemID.Sextant, 1);
            AddRewardItem(ItemID.Keg, 1);
            AddRewardItem(ItemID.Mug, 4);
        }
        public override string Description(bool complete)
        {
            if(complete)
            {
                return "Arr, ye be a fine captain indeed. It's an honour to work with ye. ";
            }
            return "Only the best captains have the brawn to sail with a mighty crew. Prove to me that ye be a great captain against me old crew, and I'll share with you a little old treasure of mine, aharr!";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (expedition.conditionCounted <= 5 && Main.time % 120 == 0)
            {
                // check occasionally to see if there are enough townspeople for the quest to show
                int townieCount = 0;
                for (int i = 0; i < 200; i++)
                {
                    if (!Main.npc[i].active || Main.npc[i].type == NPCID.OldMan) continue;
                    if (Main.npc[i].townNPC && !Main.npc[i].homeless) townieCount++;
                }
            }
            return Main.hardMode && expedition.conditionCounted > 5;
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
