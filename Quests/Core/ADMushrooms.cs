using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class ADMushrooms : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Mushroom Trip";
            SetNPCHead(NPCID.Guide, false);
            expedition.difficulty = 1;
            expedition.ctgExplore = true;

            expedition.conditionDescription1 = "Find a mushroom biome";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(ItemID.LesserHealingPotion, 6);
            AddRewardItem(ItemID.GlowingMushroom, 3);
        }
        public override string Description(bool complete)
        {
            return "Normal healing potions can be crafted by combining lesser healing potions and glowing mushrooms at a placed bottle. You can find these mushrooms growing in distinct underground biomes. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return API.FindExpedition<ACUnderground>(mod).completed;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond1) cond1 = player.ZoneGlowshroom;
            return cond1;
        }
    }
}
