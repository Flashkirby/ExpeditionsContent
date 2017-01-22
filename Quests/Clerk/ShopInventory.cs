using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Clerk
{
    class ShopInventory : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "The Expeditions Shop";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 0;
            expedition.ctgExplore = true;
            expedition.ctgImportant = true;
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(API.ItemIDExpeditionCoupon, 1);
        }
        public override string Description(bool complete)
        {
            return "Hey, thanks for stopping by. Now, you're probably wondering what an expedition coupon is? Well, they're coupons we hand out in recognition of all kinds of services. You can redeem them at my shop for all kinds of things; take one, on the house! ";
        }
        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return true;
        }
    }
}
