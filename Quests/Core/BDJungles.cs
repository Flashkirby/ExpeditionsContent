using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class BDJungles : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Jungle Mystics";
            SetNPCHead(NPCID.Guide);
            expedition.difficulty = 3;
            expedition.ctgCollect = true;

            expedition.conditionDescription1 = "Reach 200 maximum base mana";
            expedition.conditionDescription2 = "Equip a set of jungle armor";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardMoney(Item.buyPrice(0, 2, 0, 0));
        }
        public override string Description(bool complete)
        {
            return "The materials found in the underground jungle can be used to craft armor that will improve your magic capability. Should you find yourself using hefty amounts of mana, this would be a good set of armor to pick up. ";
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
            if (!cond2)
            {
                if (player.armor[0].type == ItemID.JungleHat &&
                    player.armor[1].type == ItemID.JungleShirt &&
                    player.armor[2].type == ItemID.JunglePants)
                { cond2 = true; }
            }
            return cond1 && cond2;
        }
    }
}
