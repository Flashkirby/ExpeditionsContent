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
            expedition.name = "Snap! Animal Album";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 0;
            expedition.ctgCollect = true;
            expedition.ctgExplore = true;

            expedition.conditionDescription1 = "Bunny, Mouse, Squirrel, Red Squirrel";
            expedition.conditionDescription2 = "Bird, Cardinal, Blue Jay, Duck, Mallard";
            expedition.conditionDescription3 = "Frog, Penguin, Goldfish";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(API.ItemIDExpeditionCoupon);
            AddRewardItem(mod.ItemType<Items.Albums.AlbumAnimals>());
        }
        public override string Description(bool complete)
        {
            return "Carrying around pictures is tough; they take up a lot of space and are fragile at best... that's why we compile them into albums! For a start, why not gather photos of different critters, like bunnies? ";
        }
        public static readonly int[] photos1 = {
            NPCID.Bunny ,
            NPCID.GoldBunny ,
            NPCID.Squirrel ,
            NPCID.SquirrelRed ,
            NPCID.SquirrelGold ,
            NPCID.Mouse ,
            NPCID.GoldMouse ,
        };
        public static readonly int[] photos2 = {
            NPCID.Bird ,
            NPCID.BirdBlue ,
            NPCID.BirdRed ,
            NPCID.GoldBird ,
            NPCID.Duck ,
            NPCID.DuckWhite ,
            NPCID.Duck2 ,
            NPCID.DuckWhite2 ,
        };
        public static readonly int[] photos3 = {
            NPCID.Frog ,
            NPCID.GoldFrog ,
            NPCID.Penguin ,
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
            return API.InInventory[mod.ItemType<Items.PhotoCamera>()];
        }

        public override void CheckConditionCountable(Player player, ref int count, int max)
        {
            count = PhotoManager.CountUniquePhotosInInventory(photosToMatch);
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            cond1 = PhotoManager.CountUniquePhotosInInventory(photos1) >= 4;
            cond2 = PhotoManager.CountUniquePhotosInInventory(photos2) >= 5;
            cond3 = PhotoManager.CountUniquePhotosInInventory(photos3) >= 3;
            return cond1 && cond2 && cond3;
        }

        public override void PreCompleteExpedition(List<Item> rewards, List<Item> deliveredItems)
        {
            PhotoManager.ConsumePhotos(photosToMatch);
        }
    }
}
