using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class DAMechaPrime : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Armed and Dangerous";
            SetNPCHead(NPCID.Guide);
            expedition.difficulty = 6;
            expedition.ctgSlay = true;
            expedition.ctgImportant = true;

            expedition.conditionDescription1 = "Face the hallowed defilers";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardMoney(Item.buyPrice(0, 3, 0, 0));
        }
        public override string Description(bool complete)
        {
            return "Of the three challenges, this one is the most difficult and should be attempted last. Mobility is key for success in this fight, as many attacks will be constant and deadly. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond1)
            {
                expedition.conditionDescription2 = "";
                cond1 = API.LastHitNPC.type == NPCID.SkeletronPrime
                    || API.LastHitNPC.type == NPCID.PrimeCannon
                    || API.LastHitNPC.type == NPCID.PrimeLaser
                    || API.LastHitNPC.type == NPCID.PrimeSaw
                    || API.LastHitNPC.type == NPCID.PrimeVice;
            }
            else
            { expedition.conditionDescription2 = "Defeat Skeletron Prime"; }

            return API.FindExpedition<CCTracingSteps>(mod).completed || cond1 || cond3;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {

            if (cond1 && !cond2) cond2 = NPC.downedMechBoss3;
            return cond1 && cond2;
        }
    }
}
