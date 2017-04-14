using System;
using Terraria;
using Terraria.ID;
using Expeditions;
using System.Collections.Generic;

namespace ExpeditionsContent.Quests.Clerk
{
    class AlbumWaters : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Snap! Lakes and Oceans";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 0;
            expedition.ctgCollect = true;
            expedition.ctgExplore = true;
            expedition.repeatable = true;

            expedition.conditionDescription1 = "Pirahna, Shark";
            expedition.conditionDescription2 = "Squid, Pink Jellyfish";
            expedition.conditionDescription3 = "Crab, Sea Snail";
            expedition.conditionCountedMax = 6;
            expedition.conditionDescriptionCountable = "Take photos of listed creatures";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(API.ItemIDExpeditionCoupon, 1, true);
            AddRewardItem(mod.ItemType<Items.Albums.AlbumWater>());
        }
        public override string Description(bool complete)
        {
            return "Whilst most bodies of water are safe, as you move further out it seems you'll start to encounter dangerous aquatic enemies ready to rip you a new one! Well, your job is to get photos of them. ";
        }
        #region Photo Bools
        public static bool Pirahna
        { get { return PhotoManager.PhotoOfNPC[NPCID.Piranha]; } }
        public static bool Shark
        { get { return PhotoManager.PhotoOfNPC[NPCID.Shark]; } }
        public static bool Squid
        { get { return PhotoManager.PhotoOfNPC[NPCID.Squid]; } }
        public static bool PinkJelly
        { get { return PhotoManager.PhotoOfNPC[NPCID.PinkJellyfish]; } }
        public static bool Crab
        { get { return PhotoManager.PhotoOfNPC[NPCID.Crab]; } }
        public static bool Snail
        { get { return PhotoManager.PhotoOfNPC[NPCID.SeaSnail]; } }
        #endregion

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return PlayerExplorer.HoldingCamera(mod)
                || expedition.conditionCounted > 0;
        }

        public override void CheckConditionCountable(Player player, ref int count, int max)
        {
            count = 0;
            if (Pirahna) count++;
            if (Shark) count++;
            if (Squid) count++;
            if (PinkJelly) count++;
            if (Crab) count++;
            if (Snail) count++;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            cond1 = Pirahna && Shark;
            cond2 = Squid && PinkJelly;
            cond3 = Crab && Snail;
            return cond1 && cond2 && cond3;
        }

        public override void PreCompleteExpedition(List<Item> rewards, List<Item> deliveredItems)
        {
            PhotoManager.ConsumePhoto(NPCID.Piranha);
            PhotoManager.ConsumePhoto(NPCID.Shark);
            PhotoManager.ConsumePhoto(NPCID.Squid);
            PhotoManager.ConsumePhoto(NPCID.PinkJellyfish);
            PhotoManager.ConsumePhoto(NPCID.Crab);
            PhotoManager.ConsumePhoto(NPCID.SeaSnail);

            // Only reward the coupon once!
            if (expedition.completed)
            { rewards[0] = new Item(); }
        }
    }
}