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
            expedition.name = "Snap! AAAAAAAAAAAAAAAAAAAAAA";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 2;
            expedition.ctgCollect = true;
            expedition.ctgExplore = true;
            expedition.repeatable = true;

            expedition.conditionDescription1 = "Giant Bat";
            expedition.conditionDescription2 = "Digger";
            expedition.conditionDescription3 = "Black Recluse";
            expedition.conditionCountedMax = 3;
            expedition.conditionDescriptionCountable = "Take photos of listed creatures";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(API.ItemIDExpeditionCoupon);
            AddRewardItem(mod.ItemType<Items.Albums.AlbumCavern2>());
        }
        public override string Description(bool complete)
        {
            return "TODO: FILLER. ";
        }
        #region Photo Bools
        public static bool CBat
        { get { return PhotoManager.PhotoOfNPC[NPCID.GiantBat]; } }
        public static bool Worm
        { get { return 
                    PhotoManager.PhotoOfNPC[NPCID.DiggerHead] ||
                    PhotoManager.PhotoOfNPC[NPCID.DiggerBody] ||
                    PhotoManager.PhotoOfNPC[NPCID.DiggerTail]; } }
        public static bool WallCreeper
        { get { return PhotoManager.PhotoOfNPC[NPCID.BlackRecluse] || PhotoManager.PhotoOfNPC[NPCID.BlackRecluseWall]; } }
        #endregion

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return (API.FindExpedition<AlbumOmnibus2>(mod).completed // Completed the second tier
                && Main.hardMode) // In hard mode
                || expedition.conditionCounted > 0; // Already done (repeatable)
        }

        public override void CheckConditionCountable(Player player, ref int count, int max)
        {
            count = 0;
            if (CBat) count++;
            if (Worm) count++;
            if (WallCreeper) count++;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            cond1 = CBat;
            cond2 = Worm;
            cond3 = WallCreeper;
            return cond1 && cond2 && cond3;
        }

        public override void PreCompleteExpedition(List<Item> rewards, List<Item> deliveredItems)
        {
            PhotoManager.ConsumePhoto(NPCID.GiantBat);
            if (!PhotoManager.ConsumePhoto(NPCID.DiggerHead))
            {
                if (!PhotoManager.ConsumePhoto(NPCID.DiggerBody))
                { PhotoManager.ConsumePhoto(NPCID.DiggerTail); }
            }
            if (!PhotoManager.ConsumePhoto(NPCID.BlackRecluseWall))
            { PhotoManager.ConsumePhoto(NPCID.BlackRecluse); }

            // Only reward the coupon once!
            if (expedition.completed)
            { rewards[0] = new Item(); }
        }
    }
}
