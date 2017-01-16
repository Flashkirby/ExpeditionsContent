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

                bool tatteredCloth = false;
            foreach (Item i in player.inventory) if (i.type == ItemID.TatteredCloth) tatteredCloth = true;
            return WorldGen.shadowOrbSmashed && (cond1 || tatteredCloth || Main.invasionType == 1);
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond1)
            {
                int type = API.LastHitNPC.type;
                cond1 = (
                    type == NPCID.GoblinPeon ||
                    type == NPCID.GoblinThief ||
                    type == NPCID.GoblinWarrior ||
                    type == NPCID.GoblinSorcerer
                    );
            }
            if(cond1 && !cond2)
            {
                cond2 = NPC.downedGoblins;
            }
            return cond1 && cond2;
        }
    }
}
