﻿using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.TravMerch
{
    class CrimsonRod : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "(Trade) Crimson Rod";
            SetNPCHead(NPCID.TravellingMerchant);
            expedition.difficulty = 1;
            expedition.ctgCollect = true;
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverable(ItemID.GoldCoin);
            AddDeliverableAnyOf(new int[]{
                ItemID.Vilethorn,
                ItemID.Musket,
                ItemID.ShadowOrb,
            }, 1);

            AddRewardItem(ItemID.CrimsonRod);
        }
        public override string Description(bool complete)
        {
            return "A quality item for watching your own back - enemies beware. You'll be seeing silver linings in any fight with this magical rod. ";
        }
        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Must have travelling merchant present
            if (NPC.FindFirstNPC(NPCID.TravellingMerchant) == -1) return false;

            return NPC.downedBoss1;
        }
    }
}