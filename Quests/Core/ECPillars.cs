using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class ECPillars : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Towers of Power";
            SetNPCHead(NPCID.Guide, false);
            expedition.difficulty = 9;
            expedition.ctgSlay = true;

            expedition.conditionDescriptionCountable = "Defeat the Celestial Pillars";
            expedition.conditionCountedMax = 4;
        }
        public override void AddItemsOnLoad()
        {
            AddRewardMoney(Item.buyPrice(0, 3, 0, 0));
        }
        public override string Description(bool complete)
        {
            return "The four celestial pillars are each associated with a theme, with the appropriate enemies to back them up. You need to break their shields before you can attack them directly, by defeating enemies surrounding the pillars. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return NPC.downedAncientCultist;
        }

        public override void CheckConditionCountable(Player player, ref int count, int max)
        {
            if(count < 4)
            {
                count = 0;
                if (NPC.downedTowerSolar) count++;
                if (NPC.downedTowerVortex) count++;
                if (NPC.downedTowerNebula) count++;
                if (NPC.downedTowerStardust) count++;
            }
        }
    }
}
