﻿using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.TravMerch
{
    class BandOfStarpower : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Trading Band of Starpower";
            SetNPCHead(NPCID.TravellingMerchant);
            expedition.difficulty = 1;
            expedition.ctgCollect = true;
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverable(ItemID.GoldCoin);
            AddDeliverableAnyOf(new int[]{
                ItemID.TheRottedFork,
                ItemID.CrimsonHeart,
                ItemID.PanicNecklace,
            }, 1);

            AddRewardItem(ItemID.BandofStarpower);
        }
        public override string Description(bool complete)
        {
            return "For aspiring mages, this item is probably a must have. Practical, and stylish. Become the star of the show! ";
        }

        public override void OnNewDay()
        {
            expedition.ResetProgress(true); //Reset after trade use
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Must have travelling merchant present
            if (NPC.FindFirstNPC(NPCID.TravellingMerchant) == -1) return false;

            return NPC.downedBoss1;
        }
    }
}
