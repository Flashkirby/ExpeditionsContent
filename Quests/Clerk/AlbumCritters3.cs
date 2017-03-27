using System;
using Terraria;
using Terraria.ID;
using Expeditions;
using System.Collections.Generic;

namespace ExpeditionsContent.Quests.Clerk
{
    class AlbumCritters3 : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Snap! Exotic Critters";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 1;
            expedition.ctgCollect = true;
            expedition.ctgExplore = true;
            expedition.repeatable = true;

            expedition.conditionDescription1 = "Penguin";
            expedition.conditionDescription2 = "Frog";
            expedition.conditionDescription3 = "Mouse";
            expedition.conditionCountedMax = 3;
            expedition.conditionDescriptionCountable = "Take photos of listed creatures";
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverable(mod.ItemType<Items.Albums.AlbumAnimals>());

            AddRewardItem(API.ItemIDExpeditionCoupon);
            AddRewardItem(mod.ItemType<Items.Albums.AlbumAnimals3>());
        }
        public override string Description(bool complete)
        {
            return "There are still more animals out there for you to find - these ones live beyond the forests. Frogs can be found in jungles, penguins in snowy areas, and mice underground. Good luck! ";
        }
        #region Photo Bools
        public static bool Mouse
        { get { return PhotoManager.PhotoOfNPC[NPCID.Mouse] || PhotoManager.PhotoOfNPC[NPCID.GoldMouse]; } }
        public static bool Penguin
        { get { return PhotoManager.PhotoOfNPC[NPCID.Penguin] || PhotoManager.PhotoOfNPC[NPCID.PenguinBlack]; } }
        public static bool Frog
        { get { return PhotoManager.PhotoOfNPC[NPCID.Frog] || PhotoManager.PhotoOfNPC[NPCID.GoldFrog]; } }
        #endregion

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return (API.FindExpedition<AlbumCritters2>(mod).completed)
                || expedition.conditionCounted > 0;
        }

        public override void CheckConditionCountable(Player player, ref int count, int max)
        {
            count = 0;
            if (Mouse) count++;
            if (Penguin) count++;
            if (Frog) count++;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            cond1 = Penguin;
            cond2 = Frog;
            cond3 = Mouse;
            return cond1 && cond2 && cond3;
        }

        public override void PreCompleteExpedition(List<Item> rewards, List<Item> deliveredItems)
        {
            if (!PhotoManager.ConsumePhoto(NPCID.Penguin))
            { PhotoManager.ConsumePhoto(NPCID.PenguinBlack); }

            if (!PhotoManager.ConsumePhoto(NPCID.Mouse))
            { PhotoManager.ConsumePhoto(NPCID.GoldMouse); }

            if (!PhotoManager.ConsumePhoto(NPCID.Frog))
            { PhotoManager.ConsumePhoto(NPCID.GoldFrog); }

            // Only reward the coupon once!
            if (expedition.completed)
            { rewards[0] = new Item(); }
        }
    }
}
