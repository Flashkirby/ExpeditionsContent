using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class CCTracingSteps : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "New and Improved";
            SetNPCHead(NPCID.Guide);
            expedition.difficulty = 4;
            expedition.ctgExplore = true;

            expedition.conditionDescription1 = "Install an advanced anvil";
            expedition.conditionDescription2 = "Install a stronger forge";
            expedition.conditionDescription3 = "Equip a set of new armor";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardMoney(Item.buyPrice(0, 2, 0, 0));
        }
        public override string Description(bool complete)
        {
            return "Breaking altars blesses the world with powerful materials, but also further spreads the spirits of light and dark. Terraria won't be holding back anymore, so my best advice is to begin upgrading your equipment by mining the new ores. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Only appears until all mech boss defeated (hence hallowed), or is done already
            if (!expedition.completed && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3) return false;

            // Appears once altar smashing turned in chain starts
            return API.FindExpedition<CBAltarBlessing>(mod).completed;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond1) cond1 = Main.screenTileCounts[TileID.MythrilAnvil] > 0;
            if (!cond2) cond2 = Main.screenTileCounts[TileID.AdamantiteForge] > 0;
            if (!cond3)
            {
                if (player.armor[0].rare >= 4 &&
                    player.armor[0].rare >= 4 &&
                    player.armor[0].rare >= 4)
                { cond3 = true; }
            }
            return cond1 && cond2 && cond3;
        }
    }
}
