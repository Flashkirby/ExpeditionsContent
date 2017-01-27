using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class CBAltarBlessing : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Hidden Blessings";
            SetNPCHead(NPCID.Guide, false);
            expedition.difficulty = 4;
            expedition.ctgExplore = true;
            expedition.ctgImportant = true;

            expedition.conditionDescription1 = "Smash an altar";
        }
        public override void AddItemsOnLoad()
        {
            if (WorldGen.crimson)
            {
                expedition.conditionDescription1 = "Smash a crimson altar";
            }
            else
            {
                expedition.conditionDescription1 = "Smash a demon altar";
            }

            AddRewardMoney(Item.buyPrice(0, 1, 0, 0));
        }
        public override string Description(bool complete)
        {
            return "With a pwnhammer, you can smash any "
                + (WorldGen.crimson ? "crimson" : "demon")
                + " altars you come across, which is bound to do something good. For the sake of convenience though, try to keep at least one altar intact near the town so you can craft from it. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Only appears until a mech boss is defeated (hence hallowed), or is done already
            if (!expedition.completed && NPC.downedMechBossAny) return false;

            // Appears once hardmode quest chain starts
            return API.FindExpedition<CAHardMode>(mod).completed;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond1) cond1 = Main.hardMode && !player.ZoneUnderworldHeight;
            return cond1;
        }
    }
}
