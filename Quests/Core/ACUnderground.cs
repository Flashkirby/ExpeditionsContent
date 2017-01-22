using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class ACUnderground : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Going Underground";
            SetNPCHead(NPCID.Guide, false);
            expedition.difficulty = 0;
            expedition.ctgExplore = true;

            expedition.conditionDescription1 = "Enter the Underground";
            expedition.conditionDescription2 = "Reach the Caverns";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardMoney(Item.buyPrice(0, 0, 10, 0));
        }
        public override string Description(bool complete)
        {
            return "To find more powerful items as well as tougher foes, you'll need to start heading underground, and into the caverns. You can find cave entrances across the surface. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return API.FindExpedition<ABSmeltOres>(mod).completed;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond1) cond1 = (player.position.Y + player.height) * 2f / 16f - Main.worldSurface * 2.0 > 0;
            if (!cond2) cond2 = player.position.Y > Main.rockLayer * 16.0 + (double)(1080 / 2) + 16.0;
            return cond1 && cond2;
        }
    }
}
