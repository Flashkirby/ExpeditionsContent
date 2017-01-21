using System;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Events;
using Expeditions;

namespace ExpeditionsContent.Quests.MiscPre
{
    class DD2InvasionT3 : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Monster Hunter";
            SetNPCHead(NPCID.DD2Bartender);
            expedition.difficulty = 8;
            expedition.ctgExplore = true;

            expedition.conditionDescription1 = "Challenge the etherian invaders and its ever-watchful wyvern";
            expedition.conditionCountedMax = 7; //Second & Third invasion is 7 waves
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(ItemID.DD2ElderCrystal);
            AddRewardItem(ItemID.DefenderMedal, 25);
            AddRewardPrefix(ItemID.AleThrowingGlove, 82); //http://terraria.gamepedia.com/Prefix_IDs
            AddRewardItem(ItemID.Ale);
        }
        public override string Description(bool complete)
        {
            return "You'd better watch out, it seems as though the Old One has recruited even the queen of wyverns into the invasion force. This must be the full force of their army - show me how powerful a Terrarian champion can be! ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (cond1)
            { expedition.conditionDescriptionCountable = "Defeat the Old One's Army"; }
            else
            { expedition.conditionDescriptionCountable = ""; }

            // Must have bartender around
            if (NPC.FindFirstNPC(NPCID.DD2Bartender) == -1) return false;

            return NPC.downedGolemBoss;
        }

        public override void CheckConditionCountable(Player player, ref int count, int max)
        {
            if (DD2Event.Ongoing)
            {
                count = Main.invasionProgressWave - 1;
            }
            if (DD2Event.DownedInvasionT3) count = max;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond1) cond1 = DD2Event.Ongoing;
            return cond1;
        }
    }
}