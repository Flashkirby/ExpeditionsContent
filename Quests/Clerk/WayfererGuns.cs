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
            expedition.ctgCollect = true;
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverable(ItemID.GoldCoin, 10);
        }
        public override string Description(bool complete)
        {
            if (complete) return "I'm now stocking a new gun and some bullets to go with it, just for you! If you have any coupons lying around you can redeem them to get one right now. ";
            return "Ooh! Are you much into firearms? There's a shipping opportunity available, but I'll need some investment if you want me to order them in. It's a one-off payment to set it up, so don't worry about paying extra. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond1) cond1 = NPC.FindFirstNPC(NPCID.ArmsDealer) >= 0 && API.TimeMorning;
            return cond1;
        }

        public override void PostCompleteExpedition()
        {
            if (!WorldGen.crimson)
            {
                WayfarerWeapons.ShowItemUnlock("Wayfarer's Carbine", expedition.difficulty);
            }
            else
            {
                WayfarerWeapons.ShowItemUnlock("Wayfarer's Repeater", expedition.difficulty);
            }
        }
    }
}
