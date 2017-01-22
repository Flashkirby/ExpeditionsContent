using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Clerk
{
    class SOSStylist : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Search and Rescue: Spiders";
            SetNPCHead(ExpeditionC.NPCIDClerk);
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
            return "Hope you're not too busy: I've received word from a concerned individual that an explorer hasn't been heard from in quite a while. Reports seem to suggest their route involved passing through a spider-infested tunnel. Assuming the worst, would you please investigate any web-laden caves you find. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!WorldExplorer.savedClerk) return false;

            if (cond1)
            {
                expedition.conditionDescription2 = "Free the webbed stylist";
            }

            if(!cond3)
            {
                cond3 = player.ZoneRockLayerHeight;
            }

            // Only active whilst stylist isn't saved yet, or the stylist has been saved (not just here)
            return (!NPC.savedStylist || cond1) && cond3;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Cannot "save" unless player has seen the bound first - only spawns when not saved
            if (!cond1)
            {
                Rectangle viewRect = Utils.CenteredRectangle(player.Center, new Vector2(400f, 400f));
                for (int i = 0; i < 200; i++)
                {
                    if (Main.npc[i].type != NPCID.WebbedStylist) continue;
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
                cond2 = NPC.savedStylist;
            }
            return cond1 && cond2;
        }
    }
}
