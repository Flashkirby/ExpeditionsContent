using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class AAWelcomeQuest : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Welcome To Terraria";
            SetNPCHead(NPCID.Guide);
            expedition.difficulty = 0;
            expedition.ctgCollect = true;
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverable(ItemID.Torch);

            AddRewardMoney(Item.buyPrice(0, 0, 5, 0));
        }
        public override string Description(bool complete)
        {
            return "Need some direction? The first thing you'll need to do is start collecting wood, and building yourself some shelter. Slimes drop gel when killed, which can be combined with wood to make torches. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (Main.hardMode) return false;

            // Only appears until first boss is beaten, or is done already
            return expedition.completed || !NPC.downedBoss1;
        }
    }
}
