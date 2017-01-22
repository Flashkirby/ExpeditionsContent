using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class BDHellArmour : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Bat out of the Underworld";
            SetNPCHead(NPCID.Guide, false);
            expedition.difficulty = 3;
            expedition.ctgCollect = true;
            
            expedition.conditionDescription1 = "Enter the Underworld";
            expedition.conditionDescription2 = "Find a Hellforge";
            expedition.conditionDescription3 = "Equip a full set of molten armor";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardMoney(Item.buyPrice(0, 2, 0, 0));
        }
        public override string Description(bool complete)
        {
            return "The underworld is a harsh place of ash and lava. With enough caution and proper protection, you can mine the hellstone found there. Combined with obsidian at a hellforge, it makes great armor and weapons that excel in defense and melee damage. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Only appears until hardmode, or is done already
            if (!expedition.completed && Main.hardMode) return false;

            // Appears once the second main boss is defeated AND turned in
            return API.FindExpedition<BCBoss2>(mod).completed;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond1) cond1 = player.statManaMax >= 200;
            if (!cond2) cond2 = Main.screenTileCounts[TileID.Extractinator] > 0;
            if (!cond3)
            {
                if (player.armor[0].type == ItemID.JungleHat &&
                    player.armor[1].type == ItemID.JungleShirt &&
                    player.armor[2].type == ItemID.JunglePants)
                { cond3 = true; }
            }
            return cond1 && cond2 && cond3;
        }
    }
}
