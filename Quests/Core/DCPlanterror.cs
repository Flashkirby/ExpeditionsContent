using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class DCPlanterror : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Jungle Unchained";
            SetNPCHead(NPCID.Guide, false);
            expedition.difficulty = 7;
            expedition.ctgSlay = true;
            expedition.ctgImportant = true;

            expedition.conditionDescription1 = "Face the monstrous parasite";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardMoney(Item.buyPrice(0, 0, 10, 0));
        }
        public override string Description(bool complete)
        {
            string message = "A huge parasitic plant will ocassionally sprout a delicate pink bulb somewhere in the jungle. Breaking one should provoke the monster to investigate. ";
            if (Main.player[Main.myPlayer].statLifeMax <= 420 || // BLAZE IT
                (
                Main.player[Main.myPlayer].armor[0].rare < 7 ||
                Main.player[Main.myPlayer].armor[1].rare < 7 ||
                Main.player[Main.myPlayer].armor[2].rare < 7
                ))
            {
                message += "You will need better gear, or at least 5 golden hearts, before you attempt to fight it. ";
            }
            else
            {
                message += "It is best to fight in an open chamber, and avoid leading it to the surface as it will only get stronger.";
            }
            return message;
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (cond1)
            { expedition.conditionDescription2 = "Defeat Plantera"; }
            else
            { expedition.conditionDescription2 = ""; }

            return NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3;
        }

        public override void OnCombatWithNPC(NPC npc, bool playerGotHit)
        {
            if (!expedition.condition1Met)
                expedition.condition1Met =
                    npc.type == NPCID.Plantera ||
                    npc.type == NPCID.PlanterasHook ||
                    npc.type == NPCID.PlanterasTentacle;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (cond1 && !cond2) cond2 = NPC.downedPlantBoss;
            return cond1 && cond2;
        }
    }
}