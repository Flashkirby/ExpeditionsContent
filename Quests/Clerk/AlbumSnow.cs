using System;
using Terraria;
using Terraria.ID;
using Expeditions;
using System.Collections.Generic;

namespace ExpeditionsContent.Quests.Clerk
{
    class AlbumSnow : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Snap! Cold Snap";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 1;
            expedition.ctgCollect = true;
            expedition.ctgExplore = true;
            expedition.repeatable = true;

            expedition.conditionDescription1 = "Snow Flinx";
            expedition.conditionDescription2 = "Ice Bat";
            expedition.conditionCountedMax = 2;
            expedition.conditionDescriptionCountable = "Take photos of listed creatures";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(API.ItemIDExpeditionCoupon);
            AddRewardItem(mod.ItemType<Items.Albums.AlbumSnow>());
        }
        public override string Description(bool complete)
        {
            return "Some beasts live in the snow caverns that you may want to take pictures of. Especially a snow flinx! With a name like that they must look cute! ";
        }
        #region Photo Bools
        public static bool Flinx
        { get { return PhotoManager.PhotoOfNPC[NPCID.SnowFlinx]; } }
        public static bool IBat
        { get { return PhotoManager.PhotoOfNPC[NPCID.IceBat]; } }
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
            if (Flinx) count++;
            if (IBat) count++;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            cond1 = Flinx;
            cond2 = IBat;
            return cond1 && cond2;
        }

        public override void PreCompleteExpedition(List<Item> rewards, List<Item> deliveredItems)
        {
            PhotoManager.ConsumePhoto(NPCID.SnowFlinx);
            PhotoManager.ConsumePhoto(NPCID.IceBat);

            // Only reward the coupon once!
            if (expedition.completed)
            { rewards[0] = new Item(); }
        }
    }
}
