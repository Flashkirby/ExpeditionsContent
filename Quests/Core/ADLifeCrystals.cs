using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class ADLifeCrystals : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "The Strongest Muscle";
            SetNPCHead(NPCID.Guide, false);
            expedition.difficulty = 1;
            expedition.ctgCollect = true;
            expedition.ctgImportant = true;

            expedition.conditionDescription1 = "Reach 200 maximum life";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardMoney(Item.buyPrice(0, 0, 10, 0));
        }
        public override string Description(bool complete)
        {
            return "Your main focus should be to gather life crystals to increase your maximum life. You can find crystal hearts throughout the underground and caverns. 200 life should be enough for your first challenge. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return API.FindExpedition<ACUnderground>(mod).completed;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond1) cond1 = player.statLifeMax >= 200;
            return cond1;
        }
    }
}