using System;
using Terraria;
using Terraria.ID;
using Expeditions;
using System.Collections.Generic;

namespace ExpeditionsContent.Quests.Clerk
{
    class AlbumCorruption : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Snap! Corrupted Landscape";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 2;
            expedition.ctgCollect = true;
            expedition.ctgExplore = true;
            expedition.repeatable = true;

            expedition.conditionDescription1 = "Eater Of Souls";
            expedition.conditionDescription2 = "Devourer";
            expedition.conditionDescription3 = "Eater Of Worlds";
            expedition.conditionCountedMax = 3;
            expedition.conditionDescriptionCountable = "Take photos of listed creatures";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(API.ItemIDExpeditionCoupon);
            AddRewardItem(mod.ItemType<Items.Albums.AlbumCorruption>());
        }
        public override string Description(bool complete)
        {
            return "TODO: FILLER. ";
        }
        #region Photo Bools
        public static PhotoManager eos = new PhotoManager(NPCID.EaterofSouls);
        public static PhotoManager dv = new PhotoManager(true,
            NPCID.DevourerHead, NPCID.DevourerBody, NPCID.DevourerTail);
        public static PhotoManager EoW = new PhotoManager(true,
            NPCID.EaterofWorldsHead, NPCID.EaterofWorldsBody, NPCID.EaterofWorldsTail);
        #endregion

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return (API.FindExpedition<AlbumOmnibus2>(mod).completed // Completed the second tier
                && !WorldGen.crimson)
                || expedition.conditionCounted > 0; // Already done (repeatable)
        }

        public override void CheckConditionCountable(Player player, ref int count, int max)
        {
            count = 0;
            if (eos.checkValid()) count++;
            if (dv.checkValid()) count++;
            if (EoW.checkValid()) count++;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            cond1 = eos.checkValid();
            cond2 = dv.checkValid();
            cond3 = EoW.checkValid();
            return cond1 && cond2 && cond3;
        }

        public override void PreCompleteExpedition(List<Item> rewards, List<Item> deliveredItems)
        {
            eos.consumePhoto();
            dv.consumePhoto();
            EoW.consumePhoto();

            // Only reward the coupon once!
            if (expedition.completed)
            { rewards[0] = new Item(); }
        }
    }
}