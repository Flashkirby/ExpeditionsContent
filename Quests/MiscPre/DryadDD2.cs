using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.MiscPre
{
    class DryadDD2 : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Interdimensional Accident";
            SetNPCHead(NPCID.Dryad);
            expedition.difficulty = 4;
            expedition.ctgExplore = true;

            expedition.conditionDescription1 = "Find the missing person";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(ItemID.DD2ElderCrystal);
        }
        public override string Description(bool complete)
        {
            return "So, I may have accidently summoned evil creatures from another dimension. Speaking of which, have you seen a large, bald man recently? Big bushy eyebrows, serves ale, comes from Etheria? ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (NPC.FindFirstNPC(NPCID.Dryad) == -1 || !(NPC.downedBoss1 || NPC.downedBoss2)) return false;

            if (cond1)
            {
                expedition.conditionDescription2 = "Wake up the unconscious man";
            }

            // Only active whilst stylist isn't saved yet, or the stylist has been saved (not just here)
            return !NPC.savedBartender || cond1;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Cannot "save" unless player has seen the bound first - only spawns when not saved
            if (!cond1)
            {
                Rectangle viewRect = Utils.CenteredRectangle(player.Center, new Vector2(400f, 400f));
                for (int i = 0; i < 200; i++)
                {
                    if (Main.npc[i].type != NPCID.BartenderUnconscious) continue;
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
                cond2 = NPC.savedBartender;
            }
            return cond1 && cond2;
        }
    }
}
