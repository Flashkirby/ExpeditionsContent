using System;
using Terraria;
using Terraria.ID;
using Expeditions;
using System.Collections.Generic;

namespace ExpeditionsContent.Quests.Clerk
{
    class AlbumUndead2 : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Snap! Skeletons";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 1;
            expedition.ctgCollect = true;
            expedition.ctgExplore = true;
            expedition.repeatable = true;

            expedition.conditionDescription1 = "Skeleton, Pantless Skeleton, Headache Skeleton";
            expedition.conditionDescription2 = "Misassembled Skeleton, Undead Viking, Hoplite";
            expedition.conditionDescription3 = "Skeleton Merchant, Tim, Undead Miner";
            expedition.conditionCountedMax = 9;
            expedition.conditionDescriptionCountable = "Take photos of listed creatures";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(API.ItemIDExpeditionCoupon, 1, true);
            AddRewardItem(mod.ItemType<Items.Albums.AlbumUndead2>());
        }
        public override string Description(bool complete)
        {
            return "Just like the zombies above ground, the caverns seem to host a huge variety of different skeletons. You know what to do by now, so get cracking! ";
        }
        #region Photo Bools
        public static bool S1
        { get { return PhotoManager.PhotoOfNPC[NPCID.Skeleton] || PhotoManager.PhotoOfNPC[NPCID.BoneThrowingSkeleton]; } }
        public static bool S2
        { get { return PhotoManager.PhotoOfNPC[NPCID.PantlessSkeleton] || PhotoManager.PhotoOfNPC[NPCID.BoneThrowingSkeleton4]; } }
        public static bool S3
        { get { return PhotoManager.PhotoOfNPC[NPCID.HeadacheSkeleton] || PhotoManager.PhotoOfNPC[NPCID.BoneThrowingSkeleton2]; } }
        public static bool S4
        { get { return PhotoManager.PhotoOfNPC[NPCID.MisassembledSkeleton] || PhotoManager.PhotoOfNPC[NPCID.BoneThrowingSkeleton3]; } }
        public static bool Viking
        { get { return PhotoManager.PhotoOfNPC[NPCID.UndeadViking]; } }
        public static bool Miner
        { get { return PhotoManager.PhotoOfNPC[NPCID.UndeadMiner]; } }
        public static bool Merch
        { get { return PhotoManager.PhotoOfNPC[NPCID.SkeletonMerchant]; } }
        public static bool Tim
        { get { return PhotoManager.PhotoOfNPC[NPCID.Tim]; } }
        public static bool Hoplite
        { get { return PhotoManager.PhotoOfNPC[NPCID.GreekSkeleton]; } }
        #endregion

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return (API.FindExpedition<AlbumUndead>(mod).completed)
                || expedition.conditionCounted > 0;
        }

        public override void CheckConditionCountable(Player player, ref int count, int max)
        {
            count = 0;
            if (S1) count++;
            if (S2) count++;
            if (S3) count++;
            if (S4) count++;
            if (Viking) count++;
            if (Miner) count++;
            if (Merch) count++;
            if (Tim) count++;
            if (Hoplite) count++;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            cond1 = S1 && S2 && S3;
            cond2 = S4 && Viking && Hoplite;
            cond3 = Merch && Tim && Miner;
            return cond1 && cond2 && cond3;
        }

        public override void PreCompleteExpedition(List<Item> rewards, List<Item> deliveredItems)
        {
            if (!PhotoManager.ConsumePhoto(NPCID.BoneThrowingSkeleton))
            { PhotoManager.ConsumePhoto(NPCID.Skeleton); }

            if (!PhotoManager.ConsumePhoto(NPCID.BoneThrowingSkeleton4))
            { PhotoManager.ConsumePhoto(NPCID.PantlessSkeleton); }

            if (!PhotoManager.ConsumePhoto(NPCID.BoneThrowingSkeleton2))
            { PhotoManager.ConsumePhoto(NPCID.HeadacheSkeleton); }

            if (!PhotoManager.ConsumePhoto(NPCID.BoneThrowingSkeleton3))
            { PhotoManager.ConsumePhoto(NPCID.MisassembledSkeleton); }

            PhotoManager.ConsumePhoto(NPCID.UndeadViking);
            PhotoManager.ConsumePhoto(NPCID.UndeadMiner);
            PhotoManager.ConsumePhoto(NPCID.SkeletonMerchant);
            PhotoManager.ConsumePhoto(NPCID.Tim);
            PhotoManager.ConsumePhoto(NPCID.GreekSkeleton);

            // Only reward the coupon once!
            if (expedition.completed)
            { rewards[0] = new Item(); }
        }
    }
}