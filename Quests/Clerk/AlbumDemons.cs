using System;
using Terraria;
using Terraria.ID;
using Expeditions;
using System.Collections.Generic;

namespace ExpeditionsContent.Quests.Clerk
{
    class AlbumDemons : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Snap! Watching Eyes";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 0;
            expedition.ctgCollect = true;
            expedition.ctgExplore = true;
            expedition.repeatable = true;

            expedition.conditionDescription1 = "Demon Eye, Cataract Eye";
            expedition.conditionDescription2 = "Green Eye, Purple Eye";
            expedition.conditionDescription3 = "Dilated Eye, Sleepy Eye";
            expedition.conditionCountedMax = 6;
            expedition.conditionDescriptionCountable = "Take photos of listed creatures";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(API.ItemIDExpeditionCoupon, 1, true);
            AddRewardItem(mod.ItemType<Items.Albums.AlbumDemons>());
        }
        public override string Description(bool complete)
        {
            return "You even wondered what those strange floating eye balls are, or why they are constantly watching use every night? Well I think it's our turn to watch them back - flash that camera at them! ";
        }
        #region Photo Bools
        public static bool E1
        { get { return PhotoManager.PhotoOfNPC[NPCID.DemonEye]; } }
        public static bool E2
        { get { return PhotoManager.PhotoOfNPC[NPCID.CataractEye]; } }
        public static bool E3
        { get { return PhotoManager.PhotoOfNPC[NPCID.GreenEye]; } }
        public static bool E4
        { get { return PhotoManager.PhotoOfNPC[NPCID.PurpleEye]; } }
        public static bool E5
        { get { return PhotoManager.PhotoOfNPC[NPCID.DialatedEye]; } }
        public static bool E6
        { get { return PhotoManager.PhotoOfNPC[NPCID.SleepyEye]; } }
        #endregion

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return PlayerExplorer.HoldingCamera(mod)
                || expedition.conditionCounted > 0;
        }

        public override void CheckConditionCountable(Player player, ref int count, int max)
        {
            count = 0;
            if (E1) count++;
            if (E2) count++;
            if (E3) count++;
            if (E4) count++;
            if (E5) count++;
            if (E6) count++;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            cond1 = E1 && E2;
            cond2 = E3 && E4;
            cond3 = E5 && E6;
            return cond1 && cond2 && cond3;
        }

        public override void PreCompleteExpedition(List<Item> rewards, List<Item> deliveredItems)
        {
            PhotoManager.ConsumePhoto(NPCID.DemonEye);
            PhotoManager.ConsumePhoto(NPCID.CataractEye);
            PhotoManager.ConsumePhoto(NPCID.GreenEye);
            PhotoManager.ConsumePhoto(NPCID.PurpleEye);
            PhotoManager.ConsumePhoto(NPCID.DialatedEye);
            PhotoManager.ConsumePhoto(NPCID.SleepyEye);

            // Only reward the coupon once!
            if (expedition.completed)
            { rewards[0] = new Item(); }
        }
    }
}