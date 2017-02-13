using System;
using Terraria;
using Terraria.ID;
using Expeditions;
using System.Collections.Generic;

namespace ExpeditionsContent.Quests.Clerk
{
    class AlbumBEES : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Snap! Photo Buzz";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 1;
            expedition.ctgCollect = true;
            expedition.ctgExplore = true;
            expedition.repeatable = true;

            expedition.conditionDescription1 = "Small Bee";
            expedition.conditionDescription2 = "Large Bee";
            expedition.conditionDescription3 = "Hornet";
            expedition.conditionCountedMax = 3;
            expedition.conditionDescriptionCountable = "Take photos of listed creatures";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(API.ItemIDExpeditionCoupon);
            AddRewardItem(mod.ItemType < Items.Albums.AlbumBee>());
        }
        public override string Description(bool complete)
        {
            return "The jungle has a healthy population of bees and... big bees? They're called hornets but I'm not too sure. You may need to disturb a couple of beehives to get the photos we're looking for... You'll be fine, trust me! ";
        }
        #region Photo Bools
        public static bool Bee1
        { get { return PhotoManager.PhotoOfNPC[NPCID.BeeSmall]; } }
        public static bool Bee2
        { get { return PhotoManager.PhotoOfNPC[NPCID.Bee]; } }
        public static bool Hornet
        { get { return PhotoManager.PhotoOfNPC[NPCID.Hornet]; } }
        #endregion

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return PlayerExplorer.HoldingCamera(mod)
                || expedition.conditionCounted > 0;
        }

        public override void CheckConditionCountable(Player player, ref int count, int max)
        {
            count = 0;
            if (Bee1) count++;
            if (Bee2) count++;
            if (Hornet) count++;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            cond1 = Bee1;
            cond2 = Bee2;
            cond3 = Hornet;
            return cond1 && cond2 && cond3;
        }

        public override void PreCompleteExpedition(List<Item> rewards, List<Item> deliveredItems)
        {
            PhotoManager.ConsumePhoto(NPCID.BeeSmall);
            PhotoManager.ConsumePhoto(NPCID.Bee);
            PhotoManager.ConsumePhoto(NPCID.Hornet);

            // Only reward the coupon once!
            if (expedition.completed)
            { rewards[0] = new Item(); }
        }
    }
}
