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
            expedition.repeatable = true;

            expedition.conditionDescription1 = "Bunny";
            expedition.conditionDescription2 = "Bird";
            expedition.conditionDescription3 = "Goldfish";
            expedition.conditionCountedMax = 3;
            expedition.conditionDescriptionCountable = "Take photos of listed creatures";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(API.ItemIDExpeditionCoupon, 1, true);
            AddRewardItem(mod.ItemType<Items.Albums.AlbumAnimalFirst>());
        }
        public override string Description(bool complete)
        {
            return "Carrying around pictures is tough; they take up a lot of space and are fragile at best... that's why we compile them into albums! For a start, why not gather photos of different critters? These ones should be quite simple. ";
        }
        #region Photo Bools
        public static bool Bunny
        { get { return PhotoManager.PhotoOfNPC[NPCID.Bunny] || PhotoManager.PhotoOfNPC[NPCID.GoldBunny]; } }
        public static bool Bird
        { get { return PhotoManager.PhotoOfNPC[NPCID.Bird] || PhotoManager.PhotoOfNPC[NPCID.GoldBird]; } }
        public static bool Goldfish
        { get { return PhotoManager.PhotoOfNPC[NPCID.Goldfish] || PhotoManager.PhotoOfNPC[NPCID.GoldfishWalker]; } }
        #endregion

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return PlayerExplorer.HoldingCamera(mod)
                || expedition.conditionCounted > 0;
        }

        public override void CheckConditionCountable(Player player, ref int count, int max)
        {
            count = 0;
            if (Bunny) count++;
            if (Bird) count++;
            if (Goldfish) count++;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            cond1 = Bunny;
            cond2 = Bird;
            cond3 = Goldfish;
            return cond1 && cond2 && cond3;
        }

        public override void PreCompleteExpedition(List<Item> rewards, List<Item> deliveredItems)
        {

            if (!PhotoManager.ConsumePhoto(NPCID.Bunny))
            { PhotoManager.ConsumePhoto(NPCID.GoldBunny); }

            if (!PhotoManager.ConsumePhoto(NPCID.Bird))
            { PhotoManager.ConsumePhoto(NPCID.GoldBird); }

            if (!PhotoManager.ConsumePhoto(NPCID.Goldfish))
            { PhotoManager.ConsumePhoto(NPCID.GoldfishWalker); }
        }
    }
}
