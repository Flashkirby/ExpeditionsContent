using System;
using Terraria;
using Terraria.ID;
using Expeditions;
using System.Collections.Generic;

namespace ExpeditionsContent.Quests.Clerk
{
    class AlbumUndead3 : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Snap! Dungeon Denizens";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 2;
            expedition.ctgCollect = true;
            expedition.ctgExplore = true;
            expedition.repeatable = true;

            expedition.conditionDescription1 = "Angry Bones, Spiked Angry Bones";
            expedition.conditionDescription2 = "Tough Angry Bones, Armored Angry Bones";
            expedition.conditionDescription3 = "Dark Caster, Cursed Skull";
            expedition.conditionCountedMax = 6;
            expedition.conditionDescriptionCountable = "Take photos of listed creatures";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(API.ItemIDExpeditionCoupon, 1, true);
            AddRewardItem(mod.ItemType<Items.Albums.AlbumUndead3>());
        }
        public override string Description(bool complete)
        {
            return "The dungeon looks to be chock full of skeletons of all kinds, so it's your job to take pictures of them. It seems there are also some magic users in there - suppose there are some cool magic artifacts to discover? ";
        }
        #region Photo Bools
        public static PhotoManager ab1 = new PhotoManager(NPCID.AngryBones);
        public static PhotoManager ab2 = new PhotoManager(NPCID.AngryBonesBig);
        public static PhotoManager ab3 = new PhotoManager(NPCID.AngryBonesBigMuscle);
        public static PhotoManager ab4 = new PhotoManager(NPCID.AngryBonesBigHelmet);
        public static PhotoManager dc = new PhotoManager(NPCID.DarkCaster);
        public static PhotoManager cs = new PhotoManager(NPCID.CursedSkull);
        #endregion

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return (API.FindExpedition<AlbumOmnibus2>(mod).completed // Completed the second tier
                && NPC.downedBoss3) // After skeletron
                || expedition.conditionCounted > 0; // Already done (repeatable)
        }

        public override void CheckConditionCountable(Player player, ref int count, int max)
        {
            count = 0;
            if (ab1.checkValid()) count++;
            if (ab2.checkValid()) count++;
            if (ab3.checkValid()) count++;
            if (ab4.checkValid()) count++;
            if (dc.checkValid()) count++;
            if (cs.checkValid()) count++;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            cond1 = ab1.checkValid() && ab3.checkValid();
            cond2 = ab2.checkValid() && ab4.checkValid();
            cond3 = dc.checkValid() && cs.checkValid();
            return cond1 && cond2 && cond3;
        }

        public override void PreCompleteExpedition(List<Item> rewards, List<Item> deliveredItems)
        {
            ab1.consumePhoto();
            ab2.consumePhoto();
            ab3.consumePhoto();
            ab4.consumePhoto();
            dc.consumePhoto();
            cs.consumePhoto();

            // Only reward the coupon once!
            if (expedition.completed)
            { rewards[0] = new Item(); }
        }
    }
}
