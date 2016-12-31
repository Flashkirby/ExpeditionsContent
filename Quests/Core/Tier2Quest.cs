﻿using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class Tier2Quest : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Tier 2 Quest";
            SetNPCHead(NPCID.Guide);
            expedition.difficulty = 2;
            expedition.ctgCollect = true;
            expedition.ctgImportant = true;
        }
        public override void AddItemsOnLoad()
        {
            if (!WorldGen.crimson)
            {
                expedition.name = "A Demon's Metal";
                // Enough Ore for 6 or so bars
                expedition.AddDeliverable(ItemID.DemoniteOre, 20);

                AddRewardMoney(Item.buyPrice(0, 0, 50, 0));

                // Helpful biome-styled prefixes and enough bars for a bow
                AddRewardItem(API.ItemIDRustedBox, 1);
                AddRewardItem(ItemID.DemoniteBar, 8);
            }
            else
            {
                expedition.name = "Streaks of Crimson";
                // Enough Ore for 6 or so bars
                expedition.AddDeliverable(ItemID.CrimtaneOre, 20);

                // Helpful biome-styled prefixes and enough bars for a bow
                AddRewardMoney(Item.buyPrice(0, 1, 0, 0));
                AddRewardItem(ItemID.CrimtaneBar, 8);
            }
            // Other useful items
            AddRewardItem(WorldGen.ironBar, 5);
            AddRewardItem(ItemID.Gel, 15);

            // Always need chests
            AddRewardItem(ItemID.Chest);
        }
        public override string Description(bool complete)
        {
            if (complete) return "Woah, did not expect this " + (WorldGen.crimson ? "'crimtane'" : "'demonite'") + " to come from a giant boss... well that's one for the books! ";
            return "Hey, do you think the " + (WorldGen.crimson ? "crimson" : "corruption") + " hides some ore that could be made into workable metal? I mean it's a possibility considering how it transforms things. Please investigate it! ";
        }

        public override bool CheckPrerequisites(Player player)
        {
            return API.FindExpedition(mod, "Tier1Quest").completed;
        }
    }
}
