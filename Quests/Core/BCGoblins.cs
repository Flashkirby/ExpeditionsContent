using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class BCGoblins : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "The Goblin Hoard";
            SetNPCHead(NPCID.Guide);
            expedition.difficulty = 2;
            expedition.ctgSlay = true;
            expedition.ctgImportant = true;

            expedition.conditionDescription1 = "Face the goblin army";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardMoney(Item.buyPrice(0, 2, 0, 0));
        }
        public override string Description(bool complete)
        {
            return "Tattered cloths are carried by goblin scouts, wandering not too far from the coastlines. With enough cloth and wood at a loom, you can craft an item to prematurely declare war with the goblins before they invade of their own accord. Surely there must be peacful goblins somewhere. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (cond1)
            { expedition.conditionDescription2 = "Defeat the goblin army"; }
            else
            { expedition.conditionDescription2 = ""; }

            if (!cond3)
            {
                foreach (Item i in player.inventory) if (i.type == ItemID.TatteredCloth) cond3 = true;
            }
            return WorldGen.shadowOrbSmashed || (cond1 || cond3 || Main.invasionType == 1);
        }

        public override void OnCombatWithNPC(NPC npc, bool playerGotHit)
        {
            if(!expedition.condition1Met)
            {
                expedition.condition1Met =
                    npc.type == NPCID.GoblinPeon ||
                    npc.type == NPCID.GoblinThief ||
                    npc.type == NPCID.GoblinWarrior ||
                    npc.type == NPCID.GoblinSorcerer;
            }
        }

        public override void CheckConditionCountable(Player player, ref int count, int max)
        {
            if (Main.invasionType == 1)
            {
                expedition.conditionCounted = Main.invasionProgress;
                expedition.conditionCountedMax = Main.invasionProgressMax;
                expedition.conditionDescriptionCountable = "Slay goblins";
            }
            else
            {
                expedition.conditionCounted = 0;
                expedition.conditionCountedMax = 0;
                expedition.conditionDescriptionCountable = "";
            }
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if(cond1 && !cond2)
            {
                cond2 = NPC.downedGoblins;
            }
            return cond1 && cond2;
        }
    }
}
