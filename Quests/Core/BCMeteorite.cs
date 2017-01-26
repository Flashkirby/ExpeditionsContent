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
            expedition.name = "Magic Space Metal";
            SetNPCHead(NPCID.Guide, false);
            expedition.difficulty = 2;
            expedition.ctgCollect = true;

            expedition.conditionDescription1 = "Craft a meteorite bar";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardMoney(Item.buyPrice(0, 2, 0, 0));
        }
        public override string Description(bool complete)
        {
            return "Destroying the " + (WorldGen.crimson ? "crimson hearts" : "shadow orbs") + " sometimes causes meteors to fall out of the sky. You will need a pickaxe of at least 50% power to mine it. Be careful though as the meteorite will be extremely hot, and without the correct protection it can cause serious injury. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Only appears after orb smashed until hardmode, or is done already
            return expedition.completed || (WorldGen.shadowOrbSmashed && !Main.hardMode);
        }

        public override void OnCraftItem(Item item, Recipe recipe, Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if(!expedition.condition1Met)
            {
                expedition.condition1Met = API.InInventory[ItemID.MeteoriteBar];
            }
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return cond1;
        }
    }
}
