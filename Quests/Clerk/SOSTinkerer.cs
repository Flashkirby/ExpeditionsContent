using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Clerk
{
    class SOSTinkerer : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Search and Rescue: Goblin?";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 2;
            expedition.ctgExplore = true;

            expedition.conditionDescription1 = "Find the missing goblin";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(API.ItemIDExpeditionCoupon, 1);
            AddRewardMoney(Item.buyPrice(0, 1, 0, 0));
        }
        public override string Description(bool complete)
        {
            string npc = NPC.GetFirstNPCNameOrNull(NPCID.Guide);
            if (npc == "") npc = "the guide";
            return "Would you believe " + npc + " asked me if I've seen any friendly looking goblins follwing the goblin army? It was a strange question, but given his apparent knowledge of " + Main.worldName + ", I guess it's something worth following up. Well, Let me know if you do find anything. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!WorldExplorer.savedClerk) return false;

            if (cond1)
            {
                expedition.conditionDescription2 = "Free the bound goblin";
            }

            if(!cond3)
            {
                cond3 = NPC.FindFirstNPC(NPCID.Guide) != -1 && NPC.downedGoblins;
            }

            // Only active whilst npc isn't saved yet, or the npc has been saved (not just here)
            return (!NPC.savedGoblin || cond1) && cond3;
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
                    if (Main.npc[i].type != NPCID.BoundGoblin) continue;
                    if(viewRect.Intersects(Main.npc[i].getRect()))
                    {
                        cond1 = true;
                        break;
                    }
                }
            }
            // Ensure it is only fulfilled when player is nearby when NPC is saved
            if(cond1 && !cond2)
            {
                cond2 = NPC.savedGoblin;
            }
            return cond1 && cond2;
        }
    }
}
