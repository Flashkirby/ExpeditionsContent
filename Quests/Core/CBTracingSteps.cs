using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class CBTracingSteps : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Back to Basics";
            SetNPCHead(NPCID.Guide);
            expedition.difficulty = 4;
            expedition.ctgExplore = true;
            expedition.ctgImportant = true;

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
            return "Terraria won't be holding back anymore, so my best advice is to begin upgrading your equipment. Breaking altars would be a good idea, as would opening any crates you may have lying around. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Only appears until all mech boss defeated (hence hallowed), or is done already
            if (!expedition.completed && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3) return false;

            // Appears once hardmode quest chain starts
            return API.FindExpedition<CAHardMode>(mod).completed;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond1) cond1 = Main.screenTileCounts[TileID.MythrilAnvil] > 0;
            if (!cond2) cond2 = Main.screenTileCounts[TileID.AdamantiteForge] > 0;
            if (!cond3)
            {
                if (player.armor[0].rare >= 4 &&
                    player.armor[1].rare >= 4 &&
                    player.armor[2].rare >= 4)
                { cond3 = true; }
            }
            return cond1 && cond2 && cond3;
        }
    }
}
