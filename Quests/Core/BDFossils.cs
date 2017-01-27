using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class BDFossils : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Desert Palaeontology";
            SetNPCHead(NPCID.Guide, false);
            expedition.difficulty = 3;
            expedition.ctgCollect = true;

            expedition.conditionDescription1 = "Find an extractinator";
            expedition.conditionDescription2 = "Equip a full set of fossil armor";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardMoney(Item.buyPrice(0, 2, 0, 0));
            AddRewardItem(ItemID.GoldChest);
        }
        public override string Description(bool complete)
        {
            return "The underground desert contains fossils which require at least 65% pickaxe power to mine. When fed through an extractinator, some of these fossils remain sturdy enough that they can be crafted into a strong set of throwing armor. I would recommend obtaining some if you are often using these types of weapons. ";
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
            if (!cond1)
            {
                cond1 = 
                    Main.screenTileCounts[TileID.Extractinator] > 0 ||
                    API.InInventory[ItemID.Extractinator];
            }
            if (!cond2)
            {
                if (player.armor[0].type == ItemID.FossilHelm &&
                    player.armor[1].type == ItemID.FossilShirt &&
                    player.armor[2].type == ItemID.FossilPants)
                { cond2 = true; }
            }
            return cond1 && cond2;
        }
    }
}
