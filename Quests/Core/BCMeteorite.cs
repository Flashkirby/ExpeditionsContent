using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class BCMeteorite : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Magic Space Rock";
            SetNPCHead(NPCID.Guide);
            expedition.difficulty = 1;
            expedition.ctgExplore = true;

            expedition.conditionDescription1 = "Craft a meteorite bar";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardMoney(Item.buyPrice(0, 2, 0, 0));
        }
        public override string Description(bool complete)
        {
            return "Destroying the " + (WorldGen.crimson ? "crimson hearts" : "shadow orbs") + " sometimes brings meteors to align, crashing into " + Main.worldName + ". You will need a pickaxe of at least 50% power to mine it. Be careful though as the meteorite will be extremely hot, and without the correct protection it can cause serious injury. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Only appears after orb smashed until hardmode, or is done already
            return expedition.completed || (WorldGen.shadowOrbSmashed && !Main.hardMode);
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond1)
            {
                if (player.inventory[player.selectedItem].type == ItemID.MeteoriteBar)
                {
                    cond1 = true;
                }
            }
            return cond1;
        }
    }
}
