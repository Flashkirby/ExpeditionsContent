using System;
using Terraria;
using Terraria.ID;
using Expeditions;
using System.Collections.Generic;

namespace ExpeditionsContent.Quests.Clerk
{
    class AlbumOmnibus2 : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Snap! Cavern Compilation";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 2;
            expedition.ctgCollect = true;
            expedition.repeatable = true;
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverable(mod.ItemType<Items.Albums.AlbumPredators>());
            AddDeliverable(mod.ItemType<Items.Albums.AlbumCavern>());
            AddDeliverable(mod.ItemType<Items.Albums.AlbumUndead2>());
            AddDeliverable(mod.ItemType<Items.Albums.AlbumSnow>());
            AddDeliverable(mod.ItemType<Items.Albums.AlbumAntlion>());
            AddDeliverable(mod.ItemType<Items.Albums.AlbumBee>());
            AddDeliverable(mod.ItemType<Items.Albums.AlbumFlora>());

            AddRewardItem(API.ItemIDExpeditionCoupon);
            AddRewardItem(mod.ItemType<Items.Albums.AlbumPredators2>());
        }
        public override string Description(bool complete)
        {
            return "You've built up quite the selection, so let's keep going! For the next big one let's go for all of the cool stuff you can find underground. Now there's a ton of things to find out there so this one may take a little longer than the last - but that's part of the fun, right? ";
        }
        #region Photo Bools
        public static bool Vulture
        { get { return PhotoManager.PhotoOfNPC[NPCID.Vulture]; } }
        public static bool JBat
        { get { return PhotoManager.PhotoOfNPC[NPCID.JungleBat]; } }
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
            if (Vulture) count++;
            if (JBat) count++;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            cond1 = Vulture;
            cond2 = JBat;
            return cond1 && cond2;
        }

        public override void PreCompleteExpedition(List<Item> rewards, List<Item> deliveredItems)
        {
            // Only reward the coupon once!
            if (expedition.completed)
            { rewards[0] = new Item(); }
        }
    }
}
