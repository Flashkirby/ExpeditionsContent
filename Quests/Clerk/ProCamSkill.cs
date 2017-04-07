using System;
using Terraria;
using Terraria.ID;
using Expeditions;
using System.Collections.Generic;

namespace ExpeditionsContent.Quests.Clerk
{
    class ProCamSkill : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Professional Photographer";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 5;
            expedition.ctgCollect = true;
            expedition.ctgExplore = true;

            expedition.conditionDescription1 = "Rainbow Slime";
            expedition.conditionDescription2 = "Moth";
            expedition.conditionDescription3 = "Truffle Worm";
            expedition.conditionCountedMax = 3;
            expedition.conditionDescriptionCountable = "Take photos of listed creatures";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(mod.ItemType<Items.QuestItems.PhotoCamPro>(), 1);
            AddRewardItem(mod.ItemType<Items.QuestItems.PhotoBlank>(), 30);
        }
        public override string Description(bool complete)
        {
            return "Do you think your photography skills are a cut above the rest? Well first you'll have to prove yourself - try getting shots of these rare creatures. If you can do that for me, I will gladly provide you with more professional equipment!  ";
        }
        #region Photo Bools
        public static PhotoManager rainbowSlime = new PhotoManager(NPCID.RainbowSlime);
        public static PhotoManager moth = new PhotoManager(NPCID.Moth);
        public static PhotoManager truffleWorm = new PhotoManager(false, NPCID.TruffleWorm, NPCID.TruffleWormDigger);
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
            if (rainbowSlime.checkValid()) count++;
            if (moth.checkValid()) count++;
            if (truffleWorm.checkValid()) count++;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            cond1 = rainbowSlime.checkValid();
            cond2 = moth.checkValid();
            cond3 = truffleWorm.checkValid();
            return cond1 && cond2 && cond3;
        }

        public override void PreCompleteExpedition(List<Item> rewards, List<Item> deliveredItems)
        {
            rainbowSlime.consumePhoto();
            moth.consumePhoto();
            truffleWorm.consumePhoto();

            // Only reward the coupon once!
            if (expedition.completed)
            { rewards[0] = new Item(); }
        }
    }
}
