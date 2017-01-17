using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class CCForbiddenSun : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Forbidden Sands";
            SetNPCHead(NPCID.Guide);
            expedition.difficulty = 5;
            expedition.ctgSlay = true;

            expedition.conditionDescription1 = "Encounter a Sand Elemental";
            expedition.conditionDescription2 = "Craft a set of forbidden armor";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardMoney(Item.buyPrice(0, 2, 0, 0));
        }
        public override string Description(bool complete)
        {
            return "In the midst of a desert sandstorm, a Sand Elemental will occasionally form around ancient artifacts. These fragments, once left behind by defeated elementals, can be used to craft an interesting type of magic and minion boosting armor. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Only appears until plantera is defeated, or is done already
            if (!expedition.completed && NPC.downedPlantBoss) return false;

            if (!expedition.condition3Met)
            {
                expedition.condition3Met =
                    player.inventory[player.selectedItem].type == ItemID.AdamantiteBar ||
                    player.inventory[player.selectedItem].type == ItemID.TitaniumBar;
            }

            // Appears once adamantite reached and altar smashing turned in chain starts
            return expedition.condition3Met && API.FindExpedition<CBAltarBlessing>(mod).completed;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond1) cond1 = API.LastHitNPC.type == NPCID.SandElemental;
            if (!cond2)
            {
                if (player.armor[0].type == ItemID.AncientBattleArmorHat &&
                    player.armor[1].type == ItemID.AncientBattleArmorShirt &&
                    player.armor[2].type == ItemID.AncientBattleArmorPants)
                { cond2 = true; }
            }
            return cond1 && cond2;
        }
    }
}
