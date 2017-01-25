using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Clerk
{
    class SkysTheLimit : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Sky's the Limit";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 2;
            expedition.ctgExplore = true;

            expedition.conditionDescription1 = "Discover a Floating Island";
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverable(ItemID.Cloud, 10);
            AddDeliverable(ItemID.RainCloud, 2);

            AddRewardItem(API.ItemIDExpeditionCoupon, 1);
        }
        public override string Description(bool complete)
        {
            if (complete) return "So it turns out those clouds don't actually keep the islands afloat - it's probably just magic or something, but they have some very interesting properties nevertheless! Check it out in my shop! ";
            return "What? Floating islands!? Great, now you've tempted me to go up there and see for myself. Tell you what, bring me some of that cloud - I wanna see what solid clouds are like. I wonder if they're super fluffy? ";
        }
        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Islands have 1600+ cloud blocks, or about 800 from the edge
            // Sky Lakes have 400-900
            if(!cond1)
            {
                cond1 = (Main.screenTileCounts[TileID.Cloud] + Main.screenTileCounts[TileID.RainCloud]) > 800
                    && player.ZoneSkyHeight;
            }
            return cond1;
        }
        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return cond1;
        }
    }
}
