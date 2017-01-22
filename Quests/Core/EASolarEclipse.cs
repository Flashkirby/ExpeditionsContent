using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class EASolarEclipse : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Darker Days";
            SetNPCHead(NPCID.Guide, false);
            expedition.difficulty = 8;
            expedition.ctgExplore = true;
            expedition.ctgSlay = true;

            expedition.conditionDescription1 = "Experience a solar eclipse";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardMoney(Item.buyPrice(0, 0, 10, 0));
        }
        public override string Description(bool complete)
        {
            string message = "";
            if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
            { message = "A peculiar moth only comes out during the eclipse, and if you're lucky it may even have a sword, left by an old hero who attempted to sly one. "; }
            else
            { message = "The presence of the mechanical bosses appears to be surpressing some creatures from emerging though. "; }
            return "Very rarely, the moon might end up in front of the sun, blocking out daylight. During this period, strange creatures come out to hunt. " + message;
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (cond1)
            { expedition.conditionDescription2 = "Encounter a super-sized moth"; }
            else
            { expedition.conditionDescription2 = ""; }

            if (cond2)
            { expedition.conditionDescription3 = "Defeat a Mothron"; }
            else
            { expedition.conditionDescription3 = ""; }

            // Only appears until moonlord, or is done already
            if (!expedition.completed && NPC.downedMoonlord) return false;

            if (!cond1) cond1 = Main.eclipse;

            // Appears once any mech boss defeated (when fruit spawns)
            return cond1 || NPC.downedPlantBoss;
        }

        public override void OnCombatWithNPC(NPC npc, bool playerGotHit)
        {
            if(expedition.condition1Met)
            {
                if(!expedition.condition2Met)
                {
                    expedition.condition2Met = npc.type == NPCID.Mothron;
                }
            }
        }

        public override void OnAnyNPCDeath(NPC npc)
        {
            if (expedition.condition2Met)
            {
                if (!expedition.condition3Met)
                {
                    expedition.condition3Met = npc.type == NPCID.Mothron;
                }
            }
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return cond1 && cond2 && cond3;
        }
    }
}
