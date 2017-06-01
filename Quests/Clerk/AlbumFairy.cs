using System;
using Terraria;
using Terraria.ID;
using Expeditions;
using System.Collections.Generic;

namespace ExpeditionsContent.Quests.Clerk
{
    class AlbumFairy : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Snap! Magical Folk";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 2;
            expedition.ctgCollect = true;
            expedition.ctgExplore = true;
            expedition.repeatable = true;

            expedition.conditionDescription1 = "Granite Elemental";
            expedition.conditionDescription2 = "Granite Golem";
            expedition.conditionDescription3 = "Meteor Head";
            expedition.conditionCountedMax = 3;
            expedition.conditionDescriptionCountable = "Take photos of listed creatures";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(API.ItemIDExpeditionCoupon, 1, true);
            AddRewardItem(mod.ItemType<Items.Albums.AlbumFairy>());
        }
        public override string Description(bool complete)
        {
            return "Can you imagine creatures powered purely by unknown magical forces? Well I can, and I believe it is your duty to take pictures of them!  ";
        }
        #region Photo Bools
        public static PhotoManager gf = new PhotoManager(NPCID.GraniteFlyer);
        public static PhotoManager gg = new PhotoManager(NPCID.GraniteGolem);
        public static PhotoManager mh = new PhotoManager(NPCID.MeteorHead);
        #endregion

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return (API.FindExpedition<AlbumOmnibus2>(mod).completed // Completed the second tier
                )
                || expedition.conditionCounted > 0; // Already done (repeatable)
        }

        public override void CheckConditionCountable(Player player, ref int count, int max)
        {
            count = 0;
            if (gf.checkValid()) count++;
            if (gg.checkValid()) count++;
            if (mh.checkValid()) count++;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            cond1 = gf.checkValid();
            cond2 = gg.checkValid();
            cond3 = mh.checkValid();
            return cond1 && cond2 && cond3;
        }

        public override void PreCompleteExpedition(List<Item> rewards, List<Item> deliveredItems)
        {
            gf.consumePhoto();
            gg.consumePhoto();
            mh.consumePhoto();

            // Only reward the coupon once!
            if (expedition.completed)
            { rewards[0] = new Item(); }
        }
    }
}
