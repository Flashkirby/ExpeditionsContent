using System;
using Terraria;
using Terraria.ID;
using Expeditions;
using System.Collections.Generic;

namespace ExpeditionsContent.Quests.Clerk
{
    class AlbumDemons2 : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Snap! Demonic Presence";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 2;
            expedition.ctgCollect = true;
            expedition.ctgExplore = true;
            expedition.repeatable = true;

            expedition.conditionDescription1 = "Eye of Cthulu";
            expedition.conditionDescription2 = "Blood Zombie, Drippler";
            expedition.conditionDescription3 = "Evil Bunny, Goldfish, & Penguin";
            expedition.conditionCountedMax = 6;
            expedition.conditionDescriptionCountable = "Take photos of listed creatures";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(API.ItemIDExpeditionCoupon, 1, true);
            AddRewardItem(mod.ItemType<Items.Albums.AlbumDemons2>());

            if (WorldGen.crimson)
            {
                expedition.conditionDescription3 = "Crimson Bunny, Goldfish, & Penguin";
            }
            else
            {
                expedition.conditionDescription3 = "Corrupt Bunny, Goldfish, & Penguin";
            }
        }
        public override string Description(bool complete)
        {
            string evilBiome = "corruption";
            if (WorldGen.crimson) evilBiome = "crimson";
            return "There are a lot of strange things happening as of late. The " + evilBiome
                + " and blood moons seem to have an evil influence on some of the critters. Could you take some pictures to help investigate? ";
        }
        #region Photo Bools
        public static PhotoManager eoc = new PhotoManager(NPCID.EyeofCthulhu);
        public static PhotoManager bz = new PhotoManager(NPCID.BloodZombie);
        public static PhotoManager dr = new PhotoManager(NPCID.Drippler);
        public static PhotoManager cs = new PhotoManager(false, 
            NPCID.CorruptBunny, NPCID.CrimsonBunny);
        public static PhotoManager gf = new PhotoManager(false,
            NPCID.CorruptGoldfish, NPCID.CrimsonGoldfish);
        public static PhotoManager pg = new PhotoManager(false,
            NPCID.CorruptPenguin, NPCID.CrimsonPenguin);
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
            if (eoc.checkValid()) count++;
            if (bz.checkValid()) count++;
            if (dr.checkValid()) count++;
            if (cs.checkValid()) count++;
            if (gf.checkValid()) count++;
            if (pg.checkValid()) count++;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            cond1 = eoc.checkValid();
            cond2 = bz.checkValid() && dr.checkValid();
            cond3 = cs.checkValid() && gf.checkValid() && pg.checkValid();
            return cond1 && cond2 && cond3;
        }

        public override void PreCompleteExpedition(List<Item> rewards, List<Item> deliveredItems)
        {
            eoc.consumePhoto();
            bz.consumePhoto();
            dr.consumePhoto();
            cs.consumePhoto();
            gf.consumePhoto();
            pg.consumePhoto();

            // Only reward the coupon once!
            if (expedition.completed)
            { rewards[0] = new Item(); }
        }
    }
}
