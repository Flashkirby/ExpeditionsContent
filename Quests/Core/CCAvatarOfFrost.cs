using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class CCAvatarOfFrost : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Avatar of Frost";
            SetNPCHead(NPCID.Guide);
            expedition.difficulty = 5;
            expedition.ctgSlay = true;

            expedition.conditionDescription1 = "Encounter an Ice Golem";
            expedition.conditionDescription2 = "Craft a set of frost armor";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardMoney(Item.buyPrice(0, 2, 0, 0));
        }
        public override string Description(bool complete)
        {
            return "When a blizzard is in effect, giant, magical constructs known as Ice Golems roam the surface. The cores of the golems can be used to craft a special type of armor suited to melee and ranged weapons. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Only appears until plantera is defeated, or is done already
            if (!expedition.completed && NPC.downedPlantBoss) return false;

            if(!expedition.condition3Met)
            {
                expedition.condition3Met =
                    player.inventory[player.selectedItem].type == ItemID.AdamantiteBar ||
                    player.inventory[player.selectedItem].type == ItemID.TitaniumBar;
            }

            // Appears once adamantite reached and altar smashing turned in chain starts
            return expedition.condition3Met && API.FindExpedition<CBAltarBlessing>(mod).completed;
        }

        public override void OnCombatWithNPC(NPC npc, bool playerGotHit)
        {
            if (!expedition.condition1Met)
                expedition.condition1Met = npc.type == NPCID.IceGolem;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond2)
            {
                if (player.armor[0].type == ItemID.FrostHelmet &&
                    player.armor[1].type == ItemID.FrostBreastplate &&
                    player.armor[2].type == ItemID.FrostLeggings)
                { cond2 = true; }
            }
            return cond1 && cond2;
        }
    }
}
