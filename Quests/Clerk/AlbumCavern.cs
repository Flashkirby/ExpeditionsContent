using System;
using Terraria;
using Terraria.ID;
using Expeditions;
using System.Collections.Generic;

namespace ExpeditionsContent.Quests.Clerk
{
    class AlbumCavern : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Snap! Cave Life";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 1;
            expedition.ctgCollect = true;
            expedition.ctgExplore = true;
            expedition.repeatable = true;

            expedition.conditionDescription1 = "Cave Bat";
            expedition.conditionDescription2 = "Giant Worm";
            expedition.conditionDescription3 = "Wall Creeper";
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
            return "All kinds of monsters, from skeletons to golems and demons. However for this album we'll just concern ourselves with the frequent creatures that you may encounter on any caving expedition. ";
        }
        #region Photo Bools
        public static bool CBat
        { get { return PhotoManager.PhotoOfNPC[NPCID.CaveBat]; } }
        public static bool Worm
        { get { return 
                    PhotoManager.PhotoOfNPC[NPCID.GiantWormHead] ||
                    PhotoManager.PhotoOfNPC[NPCID.GiantWormBody] ||
                    PhotoManager.PhotoOfNPC[NPCID.GiantWormTail]; } }
        public static bool WallCreeper
        { get { return PhotoManager.PhotoOfNPC[NPCID.FlyingAntlion]; } }
        #endregion

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return PlayerExplorer.HoldingCamera(mod)
                || expedition.conditionCounted > 0;
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
            PhotoManager.ConsumePhoto(NPCID.CaveBat);
            if (!PhotoManager.ConsumePhoto(NPCID.GiantWormHead))
            {
                if (!PhotoManager.ConsumePhoto(NPCID.GiantWormBody))
                { PhotoManager.ConsumePhoto(NPCID.GiantWormTail); }
            }
            PhotoManager.ConsumePhoto(NPCID.FlyingAntlion);

            // Only reward the coupon once!
            if (expedition.completed)
            { rewards[0] = new Item(); }
        }
    }
}
