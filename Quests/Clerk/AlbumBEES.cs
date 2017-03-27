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

            expedition.conditionDescription1 = "Small Bee, Large Bee";
            expedition.conditionDescription2 = "Hornet, Fatty Hornet, Stingy Hornet";
            expedition.conditionDescription3 = "Honey Hornet, Leafy Hornet, Spikey Hornet";
            expedition.conditionCountedMax = 8;
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
        public static bool H1
        { get { return PhotoManager.PhotoOfNPC[NPCID.Hornet]; } }
        public static bool H2
        { get { return PhotoManager.PhotoOfNPC[NPCID.HornetFatty]; } }
        public static bool H3
        { get { return PhotoManager.PhotoOfNPC[NPCID.HornetStingy]; } }
        public static bool H4
        { get { return PhotoManager.PhotoOfNPC[NPCID.HornetHoney]; } }
        public static bool H5
        { get { return PhotoManager.PhotoOfNPC[NPCID.HornetLeafy]; } }
        public static bool H6
        { get { return PhotoManager.PhotoOfNPC[NPCID.HornetSpikey]; } }
        #endregion

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return (API.FindExpedition<AlbumOmnibus1>(mod).completed)
                || expedition.conditionCounted > 0;
        }

        public override void CheckConditionCountable(Player player, ref int count, int max)
        {
            count = 0;
            if (Bee1) count++;
            if (Bee2) count++;
            if (H1) count++;
            if (H2) count++;
            if (H3) count++;
            if (H4) count++;
            if (H5) count++;
            if (H6) count++;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            cond1 = Bee1 && Bee2;
            cond2 = H1 && H2 && H3;
            cond3 = H4 && H5 && H6;
            return cond1 && cond2 && cond3;
        }

        public override void PreCompleteExpedition(List<Item> rewards, List<Item> deliveredItems)
        {
            PhotoManager.ConsumePhoto(NPCID.BeeSmall);
            PhotoManager.ConsumePhoto(NPCID.Bee);
            PhotoManager.ConsumePhoto(NPCID.Hornet);
            PhotoManager.ConsumePhoto(NPCID.HornetFatty);
            PhotoManager.ConsumePhoto(NPCID.HornetStingy);
            PhotoManager.ConsumePhoto(NPCID.HornetHoney);
            PhotoManager.ConsumePhoto(NPCID.HornetLeafy);
            PhotoManager.ConsumePhoto(NPCID.HornetSpikey);

            // Only reward the coupon once!
            if (expedition.completed)
            { rewards[0] = new Item(); }
        }
    }
}
