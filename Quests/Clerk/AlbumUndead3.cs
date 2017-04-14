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

            expedition.conditionDescription1 = "Angry Bones";
            expedition.conditionDescription2 = "Dark Caster";
            expedition.conditionDescription3 = "Cursed Skull";
            expedition.conditionCountedMax = 3;
            expedition.conditionDescriptionCountable = "Take photos of listed creatures";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(API.ItemIDExpeditionCoupon, 1, true);
            AddRewardItem(mod.ItemType<Items.Albums.AlbumUndead3>());
        }
        public override string Description(bool complete)
        {
            return "TODO: FILLER. ";
        }
        #region Photo Bools
        public static PhotoManager ab = new PhotoManager(false,
            NPCID.AngryBones, NPCID.AngryBonesBigMuscle,
            NPCID.AngryBonesBig, NPCID.AngryBonesBigHelmet);
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
            if (ab.checkValid()) count++;
            if (dc.checkValid()) count++;
            if (cs.checkValid()) count++;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            cond1 = ab.checkValid();
            cond2 = dc.checkValid();
            cond3 = cs.checkValid();
            return cond1 && cond2 && cond3;
        }

        public override void PreCompleteExpedition(List<Item> rewards, List<Item> deliveredItems)
        {
            ab.consumePhoto();
            dc.consumePhoto();
            cs.consumePhoto();

            // Only reward the coupon once!
            if (expedition.completed)
            { rewards[0] = new Item(); }
        }
    }
}
