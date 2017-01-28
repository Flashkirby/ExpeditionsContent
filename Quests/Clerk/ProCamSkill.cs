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

            expedition.conditionDescription1 = "Tim";
            expedition.conditionDescription2 = "Moth";
            expedition.conditionDescription3 = "Truffle Worm";
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

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return PlayerExplorer.HoldingCamera(mod) && Main.hardMode;
        }
        public readonly int[] photosToMatch = new int[]
        {
            NPCID.Tim,
            NPCID.Moth,
            NPCID.TruffleWorm,
            NPCID.TruffleWormDigger
        };

        public override void CheckConditionCountable(Player player, ref int count, int max)
        {
            count = PhotoManager.CountUniquePhotosInInventory(photosToMatch);
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            cond1 = PhotoManager.PhotoOfNPC[NPCID.Tim];
            cond2 = PhotoManager.PhotoOfNPC[NPCID.Moth];
            cond3 = PhotoManager.PhotoOfNPC[NPCID.TruffleWorm]
                || PhotoManager.PhotoOfNPC[NPCID.TruffleWormDigger];
            return cond1 && cond2 && cond3;
        }

        public override void PreCompleteExpedition(List<Item> rewards, List<Item> deliveredItems)
        {
            PhotoManager.ConsumePhotos(photosToMatch);
        }
    }
}
