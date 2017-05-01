using System;
using Terraria;
using Terraria.ID;
using Expeditions;
using System.Collections.Generic;

namespace ExpeditionsContent.Quests.Clerk
{
    class AlbumOmnibus1 : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Snap! Surface Compilation";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 1;
            expedition.ctgCollect = true;
            expedition.ctgExplore = true;
            expedition.repeatable = true;

            expedition.conditionDescription1 = "Vulture";
            expedition.conditionDescription2 = "Jungle Bat";
            expedition.conditionCountedMax = 2;
            expedition.conditionDescriptionCountable = "Take photos of listed creatures";
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverable(mod.ItemType<Items.Albums.AlbumSlimes>());
            AddDeliverable(mod.ItemType<Items.Albums.AlbumWater>());
            AddDeliverable(mod.ItemType<Items.Albums.AlbumUndead>());
            AddDeliverable(mod.ItemType<Items.Albums.AlbumDemons>());

            AddRewardItem(API.ItemIDExpeditionCoupon, 1, true);
            AddRewardItem(mod.ItemType<Items.Albums.AlbumPredators>());
        }
        public override string Description(bool complete)
        {
            return "You have photos of quite a few creatures now, perhaps you should compile them together? I can help you build an almanac of sorts, but first you'll need a couple more photos to complete the set. ";
        }
        #region Photo Bools
        public static PhotoManager vulture = new PhotoManager(NPCID.Vulture);
        public static PhotoManager jungleBat = new PhotoManager(NPCID.JungleBat);
        #endregion

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return (API.FindExpedition<AlbumSlimes>(mod).completed
                || API.FindExpedition<AlbumWaters>(mod).completed
                || API.FindExpedition<AlbumUndead>(mod).completed
                || API.FindExpedition<AlbumDemons>(mod).completed)
                || expedition.conditionCounted > 0;
        }

        public override void CheckConditionCountable(Player player, ref int count, int max)
        {
            count = 0;
            if (vulture.checkValid()) count++;
            if (jungleBat.checkValid()) count++;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            cond1 = vulture.checkValid();
            cond2 = jungleBat.checkValid();
            return cond1 && cond2;
        }

        public override void PreCompleteExpedition(List<Item> rewards, List<Item> deliveredItems)
        {
            vulture.consumePhoto();
            jungleBat.consumePhoto();

            // Only reward the coupon once!
            if (expedition.completed)
            { rewards[0] = new Item(); }
        }
    }
}