using System;
using Terraria;
using Terraria.ID;
using Expeditions;
using System.Collections.Generic;

namespace ExpeditionsContent.Quests.Clerk
{
    class AlbumOmnibus2 : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Snap! Cavern Compilation";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 2;
            expedition.ctgCollect = true;
            expedition.repeatable = true;
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverable(mod.ItemType<Items.Albums.AlbumPredators>());
            AddDeliverable(mod.ItemType<Items.Albums.AlbumCavern>());
            AddDeliverable(mod.ItemType<Items.Albums.AlbumUndead2>());
            AddDeliverable(mod.ItemType<Items.Albums.AlbumSnow>());
            AddDeliverable(mod.ItemType<Items.Albums.AlbumAntlion>());
            AddDeliverable(mod.ItemType<Items.Albums.AlbumBee>());
            AddDeliverable(mod.ItemType<Items.Albums.AlbumFlora>());

            AddRewardItem(API.ItemIDExpeditionCoupon);
            AddRewardItem(mod.ItemType<Items.Albums.AlbumPredators2>());
        }
        public override string Description(bool complete)
        {
            return "You've built up quite the selection, so let's keep going! For the next big one let's go for all of the cool stuff you can find underground. Now there's a ton of things to find out there so this one may take a little longer than the last - but that's part of the fun, right? ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return (API.FindExpedition<AlbumOmnibus1>(mod).completed)
                || expedition.conditionCounted > 0;
        }

        public override void PreCompleteExpedition(List<Item> rewards, List<Item> deliveredItems)
        {
            // Only reward the coupon once!
            if (expedition.completed)
            { rewards[0] = new Item(); }
        }
    }
}
