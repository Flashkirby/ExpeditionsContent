using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Clerk
{
    class WayfererGuns : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Lock, Stock and Barrel";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 1;
            expedition.ctgExplore = true;
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverable(ItemID.GoldCoin, 5);

            AddRewardItem(API.ItemIDExpeditionCoupon, 1);
            AddRewardItem(ItemID.MusketBall, 100);
        }
        public override string Description(bool complete)
        {
            return "Ooh! I've just had a thought! Are you much into firearms? Because I figured I could try stocking some in the store. I'll need help with paying for shipment though, since I don't exactly have much budget for this sort of thing. ";
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
