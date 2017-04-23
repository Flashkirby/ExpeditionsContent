using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.MiscPre
{
    class NurseLocket : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Just What The Nurse Ordered";
            SetNPCHead(NPCID.Nurse);
            expedition.difficulty = 1;
            expedition.ctgCollect = true;
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverable(ItemID.GoldCoin);
            AddDeliverable(ItemID.Chain, 2);

            AddRewardItem(mod.ItemType<Items.QuestItems.HeartLocket>(), 1);
        }
        public override string Description(bool complete)
        {
            return "It's in my professional interest to prevent you from dying. I think it's safe to say we would both much prefer to see you come back almost dead than actually dead. If you have the coin and some spare pieces, I can fix up this old locket which should keep you alive just a little longer.";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Doesn't appear after reaching maxlife
            if (!expedition.completed && player.statLifeMax >= 400) return false;
            // After using 3 crystals
            if (!cond1) cond1 = player.statLifeMax >= 160;
            return cond1;
        }
    }
}
