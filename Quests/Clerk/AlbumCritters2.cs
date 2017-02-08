using System;
using Terraria;
using Terraria.ID;
using Expeditions;
using System.Collections.Generic;

namespace ExpeditionsContent.Quests.Clerk
{
    class AlbumCritters2 : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Snap! Birds and Beasts";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 1;
            expedition.ctgCollect = true;
            expedition.ctgExplore = true;
            expedition.repeatable = true;

            expedition.conditionDescription1 = "Mouse, Squirrel, Red Squirrel";
            expedition.conditionDescription2 = "Penguin, Cardinal, Blue Jay";
            expedition.conditionDescription3 = "Frog, Duck, Mallard Duck";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(API.ItemIDExpeditionCoupon);
            AddRewardItem(mod.ItemType<Items.Albums.AlbumAnimals>());
        }
        public override string Description(bool complete)
        {
            return "Interested in compiling more albums? Well if you are looking for more critters, here's a list for you. Most of these are creatures are pretty common, but mice, penguins, and frogs live in certain biomes. ";
        }
        public static readonly int[] photos1 = {
            NPCID.Squirrel ,
            NPCID.SquirrelRed ,
            NPCID.SquirrelGold ,
            NPCID.Mouse ,
            NPCID.GoldMouse ,
        };
        public static readonly int[] photos2 = {
            NPCID.Penguin ,
            NPCID.PenguinBlack ,
            NPCID.BirdBlue ,
            NPCID.BirdRed ,
        };
        public static readonly int[] photos3 = {
            NPCID.Frog ,
            NPCID.GoldFrog ,
            NPCID.Duck2 ,
            NPCID.DuckWhite2 ,
            NPCID.Duck ,
            NPCID.DuckWhite ,
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
