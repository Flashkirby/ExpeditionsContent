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
            AddRewardItem(mod.ItemType<Items.Albums.AlbumCavern>());
        }
        public override string Description(bool complete)
        {
            return "TODO: FILLER. ";
        }
        #region Photo Bools
        public static bool Eater
        { get { return PhotoManager.PhotoOfNPC[NPCID.EaterofSouls]; } }
        public static bool Devourer
        { get { return 
                    PhotoManager.PhotoOfNPC[NPCID.DevourerHead] ||
                    PhotoManager.PhotoOfNPC[NPCID.DevourerBody] ||
                    PhotoManager.PhotoOfNPC[NPCID.DevourerTail]; } }
        public static bool EoW
        { get { return
                    PhotoManager.PhotoOfNPC[NPCID.EaterofWorldsHead] ||
                    PhotoManager.PhotoOfNPC[NPCID.EaterofWorldsBody] ||
                    PhotoManager.PhotoOfNPC[NPCID.EaterofWorldsTail]; } }
        #endregion

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return (API.FindExpedition<AlbumOmnibus1>(mod).completed)
                || expedition.conditionCounted > 0;
        }

        public override void CheckConditionCountable(Player player, ref int count, int max)
        {
            count = 0;
            if (Eater) count++;
            if (Devourer) count++;
            if (EoW) count++;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            cond1 = Eater;
            cond2 = Devourer;
            cond3 = EoW;
            return cond1 && cond2 && cond3;
        }

        public override void PreCompleteExpedition(List<Item> rewards, List<Item> deliveredItems)
        {
            PhotoManager.ConsumePhoto(NPCID.EaterofSouls);
            if (!PhotoManager.ConsumePhoto(NPCID.DevourerHead))
            {
                if (!PhotoManager.ConsumePhoto(NPCID.DevourerBody))
                { PhotoManager.ConsumePhoto(NPCID.DevourerTail); }
            }
            if (!PhotoManager.ConsumePhoto(NPCID.EaterofWorldsHead))
            {
                if (!PhotoManager.ConsumePhoto(NPCID.EaterofWorldsBody))
                { PhotoManager.ConsumePhoto(NPCID.EaterofWorldsTail); }
            }

            // Only reward the coupon once!
            if (expedition.completed)
            { rewards[0] = new Item(); }
        }
    }
}
