﻿using System;
using Terraria;
using Terraria.ID;
using Expeditions;
using System.Collections.Generic;

namespace ExpeditionsContent.Quests.Daily
{
    class SnapHardWyvern : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Super Snap! Wyvern";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 5;
            expedition.ctgExplore = true;
            expedition.ctgCollect = true;
            expedition.ctgImportant = true;

            expedition.conditionDescription1 = "Return with a photo of the target's head";
            expedition.conditionDescription2 = "Return with a photo of the target's body";
            expedition.conditionDescription3 = "Return with a photo of the target's tail";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(API.ItemIDExpeditionCoupon);
            AddRewardItem(mod.ItemType<Items.PhotoBlank>(), 3);
        }
        public override string Description(bool complete)
        {
            return "There's a mail-in photo challenge happening on right now! Want to enter? Today's target is a Wyvern which can be found flying high in the sky, and you have until tomorrow to submit. You'll need a photo of each part to qualify. Good luck!";
        }

        public override bool IncludeAsDaily()
        {
            return NPC.FindFirstNPC(ExpeditionC.NPCIDClerk) >= 0 && Main.hardMode;
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return API.IsDaily(expedition);
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            cond1 = PhotoManager.PhotoOfNPC[NPCID.WyvernHead];
            cond2 =
                PhotoManager.PhotoOfNPC[NPCID.WyvernBody] ||
                PhotoManager.PhotoOfNPC[NPCID.WyvernBody2] ||
                PhotoManager.PhotoOfNPC[NPCID.WyvernBody3] ||
                PhotoManager.PhotoOfNPC[NPCID.WyvernLegs];
            cond3 = PhotoManager.PhotoOfNPC[NPCID.WyvernTail];
            return cond1 && cond2 && cond3;
        }

        public override void PreCompleteExpedition(List<Item> rewards, List<Item> deliveredItems)
        {
            PhotoManager.ConsumePhoto(NPCID.WyvernHead);
            if (!PhotoManager.ConsumePhoto(NPCID.WyvernBody))
            {
                if (!PhotoManager.ConsumePhoto(NPCID.WyvernBody3))
                {
                    PhotoManager.ConsumePhoto(NPCID.WyvernLegs);
                }
            }
            PhotoManager.ConsumePhoto(NPCID.WyvernTail);
        }
    }
}
