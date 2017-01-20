using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class EAGhostBusters : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Restless Warriors";
            SetNPCHead(NPCID.Guide);
            expedition.difficulty = 8;
            expedition.ctgCollect = true;

            expedition.conditionDescription1 = "Craft a spectre bar";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardMoney(Item.buyPrice(0, 2, 0, 0));
        }
        public override string Description(bool complete)
        {
            return "With Plantera's curse lifted, the dungeon's original inhabitants have been let loose. The spirits can be vanquished for their ectoplasm, which can be crafted with chlorophyte bars to make a truly magical metal. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {

            // Only appears until moonlord, or is done already
            if (!expedition.completed && NPC.downedMoonlord) return false;

            // Appears once plantera's curse is lifted
            return API.FindExpedition<DCPlanterror>(mod).completed;
        }

        public override void OnCraftItem(Item item, Recipe recipe)
        {
            if (!expedition.condition1Met)
            {
                expedition.condition1Met = API.InInventory[ItemID.SpectreBar];
            }
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return cond1;
        }
    }
}
