using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class DBFruitsOfLabour : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Walk on the Wild Side";
            SetNPCHead(NPCID.Guide, false);
            expedition.difficulty = 6;
            expedition.ctgCollect = true;
            expedition.ctgImportant = true;

            expedition.conditionDescription1 = "Obtain a golden heart";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardMoney(Item.buyPrice(0, 0, 10, 0));
        }
        public override string Description(bool complete)
        {
            return "Have you checked out the jungle recently? You might find some peculiar fruit growing there that can upgrade your hearts. If you are having trouble finding where they grow, try drinking a spelunker potion. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Only appears whilst player needs life, or is done already
            if (!expedition.completed && player.statLifeMax >= 500) return false;
            
            // Appears once any mech boss defeated (when fruit spawns)
            return NPC.downedMechBossAny;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond1)
            {
                cond1 = player.statLifeMax > 400;
            }
            return cond1;
        }
    }
}
