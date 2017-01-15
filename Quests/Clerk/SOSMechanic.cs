using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Clerk
{
    class SOSMechanic : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Search and Rescue: Dungeon";
            SetNPCHead(ExpeditionC.npcClerk);
            expedition.difficulty = 2;
            expedition.ctgExplore = true;

            expedition.conditionDescription1 = "Find the missing person";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(API.ItemIDExpeditionCoupon, 1);
            AddRewardMoney(Item.buyPrice(0, 1, 0, 0));
        }
        public override string Description(bool complete)
        {
            return "You know the old guy who used to be under a curse? Well he says he has vague memories of leaving a girl tied up in the dungeons. It's not much, but it's at least a lead you can start from, right? If she's still alive down there. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!WorldExplorer.savedClerk) return false;

            if (cond1)
            {
                expedition.conditionDescription2 = "Free the bound mechanic";
            }

            if(!cond3)
            {
                cond3 = NPC.FindFirstNPC(NPCID.Clothier) != -1 && NPC.downedBoss3;
            }

            // Only active whilst mechanic isn't saved yet, or the mechanic has been saved (not just here)
            return (!NPC.savedMech || cond1) && cond3;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Cannot "save" unless player has seen the bound first - only spawns when not saved
            if (!cond1)
            {
                Rectangle viewRect = Utils.CenteredRectangle(player.Center, new Vector2(400f, 400f));
                for (int i = 0; i < 200; i++)
                {
                    if (Main.npc[i].type != NPCID.BoundMechanic) continue;
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
                cond2 = NPC.savedMech;
            }
            return cond1 && cond2;
        }
    }
}
