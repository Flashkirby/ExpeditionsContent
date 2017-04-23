using System;
using Terraria;
using Terraria.ID;
using Expeditions;
using System.Collections.Generic;

namespace ExpeditionsContent.Quests.Clerk
{
    class AlbumRare : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Snap! Rare Sights";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 3;
            expedition.ctgCollect = true;
            expedition.ctgExplore = true;
            expedition.repeatable = true;

            expedition.conditionDescription1 = "Goblin Scout, Nymph";
            expedition.conditionDescription2 = "Doctor Bones";
            expedition.conditionDescription3 = "The Bride, The Groom";
            expedition.conditionCountedMax = 5;
            expedition.conditionDescriptionCountable = "Take photos of listed creatures";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(API.ItemIDExpeditionCoupon, 1, true);
            AddRewardItem(mod.ItemType<Items.Albums.AlbumRare>());
        }
        public override string Description(bool complete)
        {
            return "So there are a couple of creatures out there that are much rarer than normal, either because of the biome they appear in or that they're just hard to come by. It's your job to go find them! ";
        }
        #region Photo Bools
        public static PhotoManager gb = new PhotoManager(NPCID.GoblinScout);
        public static PhotoManager ny = new PhotoManager(false, NPCID.Nymph, NPCID.LostGirl);
        public static PhotoManager db = new PhotoManager(NPCID.DoctorBones);
        public static PhotoManager br = new PhotoManager(NPCID.TheBride);
        public static PhotoManager gr = new PhotoManager(NPCID.TheGroom);
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
            if (gb.checkValid()) count++;
            if (ny.checkValid()) count++;
            if (db.checkValid()) count++;
            if (br.checkValid()) count++;
            if (gr.checkValid()) count++;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            cond1 = gb.checkValid() && ny.checkValid();
            cond2 = db.checkValid();
            cond3 = br.checkValid() && gr.checkValid();
            return cond1 && cond2 && cond3;
        }

        public override void PreCompleteExpedition(List<Item> rewards, List<Item> deliveredItems)
        {
            gb.consumePhoto();
            ny.consumePhoto();
            db.consumePhoto();
            br.consumePhoto();
            gr.consumePhoto();

            // Only reward the coupon once!
            if (expedition.completed)
            {
                rewards[0] = new Item();
                rewards[1] = new Item();
            }
        }
    }
}
