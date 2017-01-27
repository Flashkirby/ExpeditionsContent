using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class DBHallowed : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Crowning Glory";
            SetNPCHead(NPCID.Guide, false);
            expedition.difficulty = 6;
            expedition.ctgCollect = true;

            expedition.conditionDescription1 = "Equip a full set of hallowed armor";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardMoney(Item.buyPrice(0, 1, 0, 0));
        }
        public override string Description(bool complete)
        {
            return "The metal salvaged from the mechanical automatons is, perhaps unsurprisingly, a wonderful material for weapons and armor capable of supporting most types of damage. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Only appears until plantera is defeated, or is done already
            if (!expedition.completed && NPC.downedPlantBoss) return false;

            if (!cond2)
            {
                cond2 = NPC.downedMechBossAny && Main.dayTime;
            }

            // Appears once morning comes after any mech defeated has been turned in
            return cond2 && (
                API.FindExpedition<DAMechaWorm>(mod).completed ||
                API.FindExpedition<DAMechaEyes>(mod).completed ||
                API.FindExpedition<DAMechaPrime>(mod).completed);
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond1)
            {
                if ((
                    player.armor[0].type == ItemID.HallowedHelmet ||
                    player.armor[0].type == ItemID.HallowedHeadgear ||
                    player.armor[0].type == ItemID.HallowedMask
                    ) &&
                    player.armor[1].type == ItemID.HallowedPlateMail &&
                    player.armor[2].type == ItemID.HallowedGreaves)
                { cond1 = true; }
            }
            return cond1;
        }
    }
}
