using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class CCMonsterLoot : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Amazing Chest Ahead";
            SetNPCHead(NPCID.Guide, false);
            expedition.difficulty = 5;
            expedition.ctgSlay = true;

            expedition.partyShare = true;

            expedition.conditionDescription1 = "Encounter a conspicious chest";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardMoney(Item.buyPrice(0, 2, 0, 0));
        }
        public override string Description(bool complete)
        {
            return "If you leave a key of light or night in an empty chest, it will turn into a giant mimic based off the biome the key originated from. These mimics are much more powerful as a result, and should you defeat one you will be rewarded with a rare item. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Only appears until plantera is defeated, or is done already
            if (!expedition.completed && NPC.downedPlantBoss) return false;

            if (cond1)
            { expedition.conditionDescription2 = "Defeat a Greater Mimic"; }
            else
            { expedition.conditionDescription2 = ""; }

            // Appears once altar smashing turned in chain starts
            return API.FindExpedition<CBLivingLoot>(mod).completed || cond1;
        }

        public override void OnCombatWithNPC(NPC npc, bool playerGotHit, Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!expedition.condition1Met) expedition.condition1Met =
                    npc.type == NPCID.BigMimicHallow ||
                    npc.type == NPCID.BigMimicCorruption ||
                    npc.type == NPCID.BigMimicCrimson ||
                    npc.type == NPCID.BigMimicJungle;
        }

        public override void OnAnyNPCDeath(NPC npc, Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!expedition.condition2Met) expedition.condition2Met =
                    npc.type == NPCID.BigMimicHallow ||
                    npc.type == NPCID.BigMimicCorruption ||
                    npc.type == NPCID.BigMimicCrimson ||
                    npc.type == NPCID.BigMimicJungle;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return cond1 && cond2;
        }
    }
}
