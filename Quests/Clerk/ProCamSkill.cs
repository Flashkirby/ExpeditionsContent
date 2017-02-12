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

            expedition.conditionDescription1 = "Ice Golem";
            expedition.conditionDescription2 = "Moth";
            expedition.conditionDescription3 = "Truffle Worm";
            expedition.conditionCountedMax = 3;
            expedition.conditionDescriptionCountable = "Take photos of listed creatures";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(mod.ItemType<Items.PhotoCamPro>(), 1);
            AddRewardItem(mod.ItemType<Items.PhotoBlank>(), 30);
        }
        public override string Description(bool complete)
        {
            return "Do you think your photography skills are a cut above the rest? Well first you'll have to prove yourself - try getting shots of these rare creatures. If you can do that for me, I will gladly provide you with more professional equipment!  ";
        }
        #region Photo Bools
        public static bool IceGolem
        { get { return PhotoManager.PhotoOfNPC[NPCID.IceGolem]; } }
        public static bool Moth
        { get { return PhotoManager.PhotoOfNPC[NPCID.Moth]; } }
        public static bool TruffleWorm
        { get { return PhotoManager.PhotoOfNPC[NPCID.TruffleWorm] || PhotoManager.PhotoOfNPC[NPCID.TruffleWormDigger]; } }
        #endregion

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return (PlayerExplorer.HoldingCamera(mod) && Main.hardMode)
                 || expedition.conditionCounted > 0;
        }

        public override void CheckConditionCountable(Player player, ref int count, int max)
        {
            count = 0;
            if (IceGolem) count++;
            if (Moth) count++;
            if (TruffleWorm) count++;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            cond1 = IceGolem;
            cond2 = Moth;
            cond3 = TruffleWorm;
            return cond1 && cond2 && cond3;
        }

        public override void PreCompleteExpedition(List<Item> rewards, List<Item> deliveredItems)
        {
            PhotoManager.ConsumePhoto(NPCID.IceGolem);
            PhotoManager.ConsumePhoto(NPCID.Moth);

            if (!PhotoManager.ConsumePhoto(NPCID.TruffleWormDigger))
            { PhotoManager.ConsumePhoto(NPCID.TruffleWorm); }
        }
    }
}
