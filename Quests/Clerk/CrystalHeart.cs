using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Clerk
{
    class CrystalHeart : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "A Hearty Quest";
            SetNPCHead(ExpeditionC.npcClerk);
            expedition.difficulty = 0;
            expedition.ctgExplore = true;
            expedition.partyShare = true;
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverable(ItemID.LifeCrystal, 2);
            AddRewardItem(API.ItemIDExpeditionCoupon, 2);
            AddRewardMoney(Item.buyPrice(0, 0, 10, 0));
        }
        public override string Description(bool complete)
        {
            return "I want to experiment with these crystal hearts, apparently they do something that toughens your endurance? Maybe even make you look younger! Just think of the possibilities! And if this experiment works, I might have even have something in store for your troubles! ";
        }


    }
}
