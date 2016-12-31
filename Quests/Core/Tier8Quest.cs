﻿using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class Tier8Quest : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Temple Raid";
            SetNPCHead(NPCID.Guide);
            expedition.difficulty = 8;
            expedition.ctgSlay = true;
            expedition.ctgExplore = true;
            expedition.ctgImportant = true;

            expedition.conditionDescription1 = "Gain access to the Jungle Temple";
            expedition.conditionDescription1 = "Enter the Jungle Temple";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardMoney(Item.buyPrice(0, 14, 0, 0));
            AddRewardItem(API.ItemIDRelicBox, 1);

            // Boss summons!
            AddRewardItem(ItemID.LihzahrdPowerCell, 2);

            // Dungeon prep
            AddRewardItem(ItemID.InfernoPotion, 2);

            // Always need chests
            AddRewardItem(ItemID.LihzahrdChest);
        }
        public override string Description(bool complete)
        {
            if (complete) return "You had to fight a huge man-eating plant to recover the keys to the Temple? That sounds so cool! Like one of those action movies, y'know? Anyway, while you were down there, I posted another urgent expedition for you to look at.";
            return "I WANT YOU!... to investigate that jungle temple. But how can we get in I wonder. Knowing this place you'll probably need to defeat a bigger, scarier jungle boss, like the dungeon's Skeletron hijinks.";
        }

        public override bool CheckPrerequisites(Player player)
        {
            return API.FindExpedition(mod, "Tier7Quest").completed;
        }
        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond1) cond1 = NPC.downedPlantBoss;
            if (cond1 && !cond2)
            {
                try
                {
                    cond2 = Main.tile[
                        (int)(player.Center.X / 16f),
                        (int)(player.Center.Y / 16f)
                        ].wall == WallID.LihzahrdBrickUnsafe;
                }
                catch { }
            }
            return cond1 && cond2;
        }
    }
}
