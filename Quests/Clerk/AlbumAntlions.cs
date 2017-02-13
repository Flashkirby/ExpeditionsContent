using System;
using Terraria;
using Terraria.ID;
using Expeditions;
using System.Collections.Generic;

namespace ExpeditionsContent.Quests.Clerk
{
    class AlbumAntlions : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Snap! An Antlion's Life";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 1;
            expedition.ctgCollect = true;
            expedition.ctgExplore = true;
            expedition.repeatable = true;

            expedition.conditionDescription1 = "Antlion";
            expedition.conditionDescription2 = "Antlion Charger";
            expedition.conditionDescription3 = "Antlion Swarmer";
            expedition.conditionCountedMax = 3;
            expedition.conditionDescriptionCountable = "Take photos of listed creatures";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(API.ItemIDExpeditionCoupon);
            AddRewardItem(mod.ItemType<Items.Albums.AlbumAntlion>());
        }
        public override string Description(bool complete)
        {
            return "Here's a task for you - have you looked at the various insects residing in that giant pit in the desert? They're all antlions of some kind, but perhaps if you take some photos I can do some homework into their lifecycle. ";
        }
        #region Photo Bools
        public static bool Antlion
        { get { return PhotoManager.PhotoOfNPC[NPCID.Antlion]; } }
        public static bool Charger
        { get { return PhotoManager.PhotoOfNPC[NPCID.WalkingAntlion]; } }
        public static bool Swarmer
        { get { return PhotoManager.PhotoOfNPC[NPCID.FlyingAntlion]; } }
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
            if (Antlion) count++;
            if (Charger) count++;
            if (Swarmer) count++;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            cond1 = Antlion;
            cond2 = Charger;
            cond3 = Swarmer;
            return cond1 && cond2 && cond3;
        }

        public override void PreCompleteExpedition(List<Item> rewards, List<Item> deliveredItems)
        {
            PhotoManager.ConsumePhoto(NPCID.Antlion);
            PhotoManager.ConsumePhoto(NPCID.WalkingAntlion);
            PhotoManager.ConsumePhoto(NPCID.FlyingAntlion);

            // Only reward the coupon once!
            if (expedition.completed)
            { rewards[0] = new Item(); }
        }
    }
}
