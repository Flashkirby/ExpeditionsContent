using System;
using Terraria;
using Terraria.ID;
using Expeditions;
using System.Collections.Generic;

namespace ExpeditionsContent.Quests.Clerk
{
    class AlbumMushi2 : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Snap! Freakish Fungus";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 2;
            expedition.ctgCollect = true;
            expedition.ctgExplore = true;
            expedition.repeatable = true;

            expedition.conditionDescription1 = "Anomura Fungus";
            expedition.conditionDescription2 = "Giant Fungi Bulb";
            expedition.conditionDescription3 = "Fungo Fish";
            expedition.conditionCountedMax = 3;
            expedition.conditionDescriptionCountable = "Take photos of listed creatures";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(API.ItemIDExpeditionCoupon);
            AddRewardItem(mod.ItemType<Items.Albums.AlbumMushroom2>());
        }
        public override string Description(bool complete)
        {
            return "TODO: FILLER. ";
        }
        #region Photo Bools
        public static PhotoManager af = new PhotoManager(NPCID.AnomuraFungus);
        public static PhotoManager gfb = new PhotoManager(NPCID.GiantFungiBulb);
        public static PhotoManager ff = new PhotoManager(NPCID.FungoFish);
        #endregion

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return (API.FindExpedition<AlbumOmnibus3>(mod).completed // Completed the second tier
                && Main.hardMode) // In hard mode
                || expedition.conditionCounted > 0; // Already done (repeatable)
        }

        public override void CheckConditionCountable(Player player, ref int count, int max)
        {
            count = 0;
            if (af.checkValid()) count++;
            if (gfb.checkValid()) count++;
            if (ff.checkValid()) count++;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            cond1 = af.checkValid();
            cond2 = gfb.checkValid();
            cond3 = ff.checkValid();
            return cond1 && cond2 && cond3;
        }

        public override void PreCompleteExpedition(List<Item> rewards, List<Item> deliveredItems)
        {
            af.consumePhoto();
            gfb.consumePhoto();
            ff.consumePhoto();

            // Only reward the coupon once!
            if (expedition.completed)
            { rewards[0] = new Item(); }
        }
    }
}
