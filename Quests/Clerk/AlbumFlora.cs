using System;
using Terraria;
using Terraria.ID;
using Expeditions;
using System.Collections.Generic;

namespace ExpeditionsContent.Quests.Clerk
{
    class AlbumFlora : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Snap! Carnivorous Plants";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 1;
            expedition.ctgCollect = true;
            expedition.ctgExplore = true;
            expedition.repeatable = true;

            expedition.conditionDescription1 = "Snatcher";
            expedition.conditionDescription2 = "Man Eater";
            expedition.conditionCountedMax = 2;
            expedition.conditionDescriptionCountable = "Take photos of listed creatures";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(API.ItemIDExpeditionCoupon);
            AddRewardItem(mod.ItemType<Items.Albums.AlbumFlora>());
        }
        public override string Description(bool complete)
        {
            return "Here's another album theme for you. There are some hostile creatures in " + Main.worldName + " that are actually plants! Crazy huh? Well anyway see what you can get, and don't get eaten. ";
        }
        #region Photo Bools
        public static bool Snatcher
        { get { return PhotoManager.PhotoOfNPC[NPCID.Snatcher]; } }
        public static bool ManEater
        { get { return PhotoManager.PhotoOfNPC[NPCID.ManEater]; } }
        #endregion

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return (PlayerExplorer.HoldingCamera(mod)
                && API.FindExpedition<AlbumOmnibus1>(mod).completed)
                || expedition.conditionCounted > 0;
        }

        public override void CheckConditionCountable(Player player, ref int count, int max)
        {
            count = 0;
            if (Snatcher) count++;
            if (ManEater) count++;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            cond1 = Snatcher;
            cond2 = ManEater;
            return cond1 && cond2;
        }

        public override void PreCompleteExpedition(List<Item> rewards, List<Item> deliveredItems)
        {
            PhotoManager.ConsumePhoto(NPCID.Snatcher);
            PhotoManager.ConsumePhoto(NPCID.ManEater);
            PhotoManager.ConsumePhoto(NPCID.FungiBulb);

            // Only reward the coupon once!
            if (expedition.completed)
            { rewards[0] = new Item(); }
        }
    }
}
