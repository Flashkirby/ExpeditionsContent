using System;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Events;
using Expeditions;

namespace ExpeditionsContent.Quests.MiscPre
{
    class DD2InvasionT1 : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Defender Initiate";
            SetNPCHead(NPCID.DD2Bartender);
            expedition.difficulty = 3;
            expedition.ctgExplore = true;

            expedition.conditionDescription1 = "Challenge the etherian invaders and its dark magicians";
            expedition.conditionCountedMax = 5; //First invasion is 5 waves
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(ItemID.DD2ElderCrystal);
            AddRewardItem(ItemID.Ale);
        }
        public override string Description(bool complete)
        {
            return "The Old One's army are looking to take over " + Main.worldName + ", though their current forces are nothing the Talonguard couldn't handle. To challenge them, you'll have to bait them out by placing an Eternia Crystal on a stand. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (cond1)
            { expedition.conditionDescriptionCountable = "Defeat the Old One's Army"; }
            else
            { expedition.conditionDescriptionCountable = ""; }

            // Must have bartender around
            if (NPC.FindFirstNPC(NPCID.DD2Bartender) == -1) return false;
            // Doesn't appear after first mech
            if (!expedition.completed && NPC.downedMechBossAny) return false;

            return true;
        }

        public override void CheckConditionCountable(Player player, ref int count, int max)
        {
            if (DD2Event.Ongoing)
            {
                count = Main.invasionProgressWave - 1;
            }
            if (DD2Event.DownedInvasionT1) count = max;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond1) cond1 = DD2Event.Ongoing;
            return cond1;
        }
    }
}