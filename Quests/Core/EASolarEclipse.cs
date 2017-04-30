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
            expedition.name = "Total Eclipse of The Sun";
            SetNPCHead(NPCID.Guide, false);
            expedition.difficulty = 8;
            expedition.ctgExplore = true;
            expedition.ctgSlay = true;

            expedition.conditionDescription1 = "Experience a solar eclipse";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardMoney(Item.buyPrice(0, 3, 0, 0));
        }
        public override string Description(bool complete)
        {
            string message = "";
            if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
            { message = "A peculiar moth only comes out during the eclipse, and if you're lucky it may even carry on it a sword from a hero who once tried to slay it. "; }
            else
            { message = "Perhaps if you best the mechanical bosses, a powerful foe may emerge to challenge you during this eclipse. "; }
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

            // Appears during an eclipse, or just generally after plantera
            return cond1 || NPC.downedPlantBoss;
        }

        public override void OnCombatWithNPC(NPC npc, bool playerGotHit, Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if(cond1)
            {
                if(!cond2)
                {
                    cond2 = npc.type == NPCID.Mothron;
                }
            }
        }

        public override void OnAnyNPCDeath(NPC npc, Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
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
