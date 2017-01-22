using System;
using Terraria;
using Terraria.ID;
using Expeditions;
using System.Collections.Generic;

namespace ExpeditionsContent.Quests.Clerk
{
    class AlbumCritters : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Critter Album";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 0;
            expedition.ctgCollect = true;

            expedition.repeatable = true;

            expedition.conditionDescriptionCountable = "Collect photos";
            expedition.conditionCountedMax = 2;
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(ItemID.Keybrand);
        }
        public override string Description(bool complete)
        {
            return "I need pictures. Pictures of: Bunny, Bird";
        }
        public static readonly int[] photosToMatch = {
            NPCID.Bunny,
            NPCID.Bird
        };

        public override void CheckConditionCountable(Player player, ref int count, int max)
        {
            count =  PhotoManager.CountUniquePhotosInInventory(photosToMatch);
        }
        public override void PreCompleteExpedition(List<Item> rewards, List<Item> deliveredItems)
        {
            PhotoManager.ConsumePhotos(photosToMatch);
        }
    }
}
