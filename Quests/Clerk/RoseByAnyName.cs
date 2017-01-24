using System;
using Terraria;
using Terraria.ID;
using Expeditions;
using System.Collections.Generic;

namespace ExpeditionsContent.Quests.Clerk
{
    class RoseByAnyName : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Rose-tinted Rare";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 2;
            expedition.ctgCollect = true;
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverable(ItemID.NaturesGift, 1);

            AddRewardItem(API.ItemIDExpeditionCoupon, 1);
        }
        public override string Description(bool complete)
        {
            if (complete) return "Wow, this flower is positively flowing with magic! I've sent it off for further study; imagine how popular it might be with botanists and magicians. Speaking of magicians, I've receieved a fancy magical staff as a 'payment bonus', so feel free take a look in store. ";
            return "Apparently, a rare flower known as 'nature's gift' blooms in the jungle. Why does it warrant attention, you may ask? Well, that's for you to find out! After all, if something's rare it's probably worth looking for, right? ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Disappear after WoF
            if (!expedition.completed && Main.hardMode) return false;

            if (!cond1) cond1 = NPC.FindFirstNPC(NPCID.Demolitionist) >= 0 && API.TimeWitchingHour;
            return cond1;
        }
    }
}
