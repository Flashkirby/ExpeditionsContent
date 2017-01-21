using System;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Events;
using Expeditions;

namespace ExpeditionsContent.Quests.MiscPre
{
    class DD2InvasionT2 : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Layered Assault";
            SetNPCHead(NPCID.DD2Bartender);
            expedition.difficulty = 5;
            expedition.ctgExplore = true;

            expedition.conditionDescription1 = "Challenge the etherian invaders and its mighty ogres";
            expedition.conditionCountedMax = 7; //Second & Third invasion is 7 waves
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(ItemID.DD2ElderCrystal);
            AddRewardItem(ItemID.DefenderMedal, 5);
            AddRewardItem(ItemID.Ale);
        }
        public override string Description(bool complete)
        {
            return "The Old One's upped his ante, and you'll need the power to match. This time around they've got kobolds, an unpleasant bunch at best. They're strapped with explosives, so don't let them detonate on the crystal! ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (cond1)
            { expedition.conditionDescriptionCountable = "Defeat the Old One's Army"; }
            else
            { expedition.conditionDescriptionCountable = ""; }

            // Must have bartender around
            if (NPC.FindFirstNPC(NPCID.DD2Bartender) == -1) return false;
            // Doesn't appear after geolm
            if (!expedition.completed && NPC.downedGolemBoss) return false;

            return NPC.downedMechBossAny;
        }

        public override void CheckConditionCountable(Player player, ref int count, int max)
        {
            if (DD2Event.Ongoing)
            {
                count = Main.invasionProgressWave - 1;
            }
            if (DD2Event.DownedInvasionT2) count = max;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond1) cond1 = DD2Event.Ongoing;
            return cond1;
        }
    }
}