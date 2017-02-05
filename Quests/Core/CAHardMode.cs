using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class CAHardMode : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Welcome to Hardmode";
            SetNPCHead(NPCID.Guide, false);
            expedition.difficulty = 4;
            expedition.ctgExplore = true;

            expedition.conditionDescription1 = "Enter Hardmode";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardMoney(Item.buyPrice(0, 4, 0, 0));
            AddRewardItem(ItemID.PeaceCandle, 1);
            AddRewardItem(ItemID.WaterCandle, 1);
        }
        public override string Description(bool complete)
        {
            return "You have proven yourself powerful enough to take on the true enemies of Terraria. With the spirits released, you will encounter dangerous new enemies and amazing treasures. Be cautious, be brave, and good luck! ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Only appears in hardmode
            return Main.hardMode;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return cond1;
        }
    }
}
