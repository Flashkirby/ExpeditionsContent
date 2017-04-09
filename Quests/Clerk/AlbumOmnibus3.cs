using System;
using Terraria;
using Terraria.ID;
using Expeditions;
using System.Collections.Generic;

namespace ExpeditionsContent.Quests.Clerk
{
    class AlbumOmnibus3 : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Snap! World Compilation";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 2;
            expedition.ctgCollect = true;
            expedition.ctgExplore = true;
            expedition.repeatable = true;

            expedition.conditionDescription1 = "Harpy";
            expedition.conditionDescription2 = "Bone Serpent";
            expedition.conditionCountedMax = 2;
            expedition.conditionDescriptionCountable = "Take photos of listed creatures";
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverableAnyOf(new int[] {
                mod.ItemType<Items.Albums.AlbumCorruption>(),
                mod.ItemType<Items.Albums.AlbumCrimson>()});
            AddDeliverable(mod.ItemType<Items.Albums.AlbumSlimes2>());
            AddDeliverable(mod.ItemType<Items.Albums.AlbumFairy>());
            AddDeliverable(mod.ItemType<Items.Albums.AlbumMushroom>());
            AddDeliverable(mod.ItemType<Items.Albums.AlbumUndead3>());
            AddDeliverable(mod.ItemType<Items.Albums.AlbumDemons2>());
            AddDeliverable(mod.ItemType<Items.Albums.AlbumRare>());

            AddRewardItem(API.ItemIDExpeditionCoupon, 2);
            AddRewardItem(mod.ItemType<Items.Albums.AlbumPredators3>());
        }
        public override string Description(bool complete)
        {
            return "TODO: FILLER. ";
        }
        #region Photo Bools
        public static PhotoManager harpy = new PhotoManager(NPCID.Harpy);
        public static PhotoManager boneSerpent = new PhotoManager(false,
            NPCID.BoneSerpentHead, NPCID.BoneSerpentBody, NPCID.BoneSerpentTail);
        #endregion

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return (API.FindExpedition<AlbumOmnibus2>(mod).completed // Completed the second tier
                )
                || expedition.conditionCounted > 0; // Already done (repeatable)
        }

        public override void CheckConditionCountable(Player player, ref int count, int max)
        {
            count = 0;
            if (harpy.checkValid()) count++;
            if (boneSerpent.checkValid()) count++;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            cond1 = harpy.checkValid();
            cond2 = boneSerpent.checkValid();
            return cond1 && cond2 && cond3;
        }

        public override void PreCompleteExpedition(List<Item> rewards, List<Item> deliveredItems)
        {
            harpy.consumePhoto();
            boneSerpent.consumePhoto();

            // Only reward the coupon once!
            if (expedition.completed)
            { rewards[0] = new Item(); }
        }
    }
}