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
            expedition.ctgCollect = true;

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

            if (!cond3)
            {
                cond3 =
                    API.InInventory[ItemID.AdamantiteBar] ||
                    API.InInventory[ItemID.TitaniumBar];
            }

            // Appears once adamantite reached or hit elemental
            return cond3 || cond1;
        }

        public override void OnCombatWithNPC(NPC npc, bool playerGotHit)
        {
            if (!expedition.condition1Met)
                expedition.condition1Met = npc.type == NPCID.SandElemental;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
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
