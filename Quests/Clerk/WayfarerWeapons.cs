using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Clerk
{
    class WayfarerWeapons : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Wayfarer's Armory";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 0;
            expedition.ctgCollect = true;

            expedition.partyShare = true;
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverable(ItemID.DartTrap);
            AddDeliverable(ItemID.GeyserTrap);
            AddDeliverableAnyOf(new int[] {
                ItemID.GrayPressurePlate,
                ItemID.BrownPressurePlate }, 1);

            AddRewardItem(API.ItemIDExpeditionCoupon, 1);
        }
        public override string Description(bool complete)
        {
            return "As you may already know, the underground is littered with traps! This is of peak interest to me - I really want to know how they work, and who originally put them there. If you can find a few for me, I'd be way more than happy to order in some new items for the shop! ";
        }
        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Disappear after skeletron, queen bee or WoF
            if (!expedition.completed && (NPC.downedBoss3 || NPC.downedQueenBee || Main.hardMode)) return false;

            return true;
        }
    }
}
