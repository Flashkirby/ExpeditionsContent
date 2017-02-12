using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class CBSavingGrace : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Saving Grace";
            SetNPCHead(NPCID.Guide, false);
            expedition.difficulty = 4;
            expedition.ctgExplore = true;

            expedition.partyShare = true;

            expedition.conditionDescription2 = "Find the Tortured Soul";
            expedition.conditionDescription1 = "Purify the Tortured Soul";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardMoney(Item.buyPrice(0, 1, 0, 0));
        }
        public override string Description(bool complete)
        {
            return "There is an unfortunate soul trapped in the underworld, cursed even in undeath. Perhaps something will happen if you scatter purification powder on them. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Appears once hardmode quest chain starts and tax man not saved yet
            return (!NPC.savedTaxCollector || cond1) && API.FindExpedition<CAHardMode>(mod).completed;
        }

        private const float viewRangeX = 1984f;
        private const float viewRangeY = 1120f;
        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Cannot "save" unless player has seen the bound first - only spawns when not saved
            if (!cond1)
            {
                Rectangle viewRect = Utils.CenteredRectangle(player.Center, new Vector2(viewRangeX, viewRangeY));
                for (int i = 0; i < 200; i++)
                {
                    if (Main.npc[i].type != NPCID.DemonTaxCollector) continue;
                    if (viewRect.Intersects(Main.npc[i].getRect()))
                    {
                        cond1 = true;
                        break;
                    }
                }
            }
            // Ensure it is only fulfilled when player is nearby when NPC is saved
            if (cond1 && !cond2)
            {
                cond2 = NPC.savedTaxCollector;
            }
            return cond1;
        }
    }
}
