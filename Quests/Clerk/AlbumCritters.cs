﻿using System;
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
            expedition.name = "Snap! Animal Album";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 0;
            expedition.ctgCollect = true;
            expedition.ctgExplore = true;
            expedition.repeatable = true;

            expedition.conditionDescription1 = "Bunny";
            expedition.conditionDescription2 = "Bird";
            expedition.conditionDescription3 = "Goldfish";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(API.ItemIDExpeditionCoupon);
            AddRewardItem(mod.ItemType<Items.Albums.AlbumAnimalFirst>());
        }
        public override string Description(bool complete)
        {
            return "Carrying around pictures is tough; they take up a lot of space and are fragile at best... that's why we compile them into albums! For a start, why not gather photos of different critters? These ones should be quite simple. ";
        }
        public static readonly int[] photos1 = {
            NPCID.Bunny ,
            NPCID.GoldBunny ,
        };
        public static readonly int[] photos2 = {
            NPCID.Bird ,
            NPCID.GoldBird ,
        };
        public static readonly int[] photos3 = {
            NPCID.Goldfish ,
            NPCID.GoldfishWalker ,
        };
        public static int[] photosToMatch
        {
            get
            {
                int[] photosToMatch = new int[photos1.Length + photos2.Length + photos3.Length];
                photos1.CopyTo(photosToMatch, 0);
                photos2.CopyTo(photosToMatch, photos1.Length);
                photos3.CopyTo(photosToMatch, photos1.Length + photos2.Length);
                return photosToMatch;
            }
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return PlayerExplorer.HoldingCamera(mod);
        }

        public override void CheckConditionCountable(Player player, ref int count, int max)
        {
            count = PhotoManager.CountUniquePhotosInInventory(photosToMatch);
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            cond1 = PhotoManager.CountUniquePhotosInInventory(photos1) >= 1;
            cond2 = PhotoManager.CountUniquePhotosInInventory(photos2) >= 1;
            cond3 = PhotoManager.CountUniquePhotosInInventory(photos3) >= 1;
            return cond1 && cond2 && cond3;
        }

        public override void PreCompleteExpedition(List<Item> rewards, List<Item> deliveredItems)
        {
            PhotoManager.ConsumePhotos(photosToMatch);
        }
    }
}
