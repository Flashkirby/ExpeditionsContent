using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Clerk
{
    class FragileIceCave : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Caves of Ice";
            SetNPCHead(ExpeditionC.NPCIDClerk);
            expedition.difficulty = 1;
            expedition.ctgExplore = true;

            expedition.conditionDescription1 = "Discover a fragile ice cavern";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(ItemID.IceSkates);
            AddRewardItem(ItemID.IceChest);
        }
        public override string Description(bool complete)
        {
            return "Oh wow, you found a cave of clear ice? That must look amazing! I wonder how such as thing could form, but it's certainly something worth nothing, don't cha think? ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond1)
            {
                Main.NewText("ice: " + Main.screenTileCounts[TileID.BreakableIce]);
                cond1 = (Main.screenTileCounts[TileID.BreakableIce] > 500); // 500-1700+ is fragile ice cavern
            }
            return cond1;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return cond1;
        }
    }
}
