using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class CASnowArmy : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Winter Hostilities";
            SetNPCHead(NPCID.Guide, false);
            expedition.difficulty = 4;
            expedition.ctgExplore = true;

            expedition.conditionDescription1 = "Face the army of snowmen";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardMoney(Item.buyPrice(0, 5, 0, 0));
            AddRewardItem(ItemID.IceChest, 1);
        }
        public override string Description(bool complete)
        {
            return "If you're lucky, a present might contain a Snow Globe - using one will summon the Frost Legion. They say those who prove their might against the legion may soon be visited by a... jovial fellow. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (cond1)
            { expedition.conditionDescription2 = "Defeat the Frost Legion"; }
            else
            { expedition.conditionDescription2 = ""; }

            // Only appears during christmas
            if (!expedition.completed && !Main.xMas) return false;

            // Appears only in a hardmode world
            return Main.hardMode;
        }

        public override void OnCombatWithNPC(NPC npc, bool playerGotHit, Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!expedition.condition1Met)
            {
                expedition.condition1Met =
                    npc.type == NPCID.SnowBalla ||
                    npc.type == NPCID.SnowmanGangsta ||
                    npc.type == NPCID.MisterStabby;
            }
        }

        public override void CheckConditionCountable(Player player, ref int count, int max)
        {
            if (Main.invasionType == ExpeditionC.InvasionIDFrostLegion)
            {
                expedition.conditionCounted = Main.invasionProgress;
                expedition.conditionCountedMax = Main.invasionProgressMax;
                expedition.conditionDescriptionCountable = "Slay snowmen";
            }
            else
            {
                expedition.conditionCounted = 0;
                expedition.conditionCountedMax = 0;
                expedition.conditionDescriptionCountable = "";
            }
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (cond1 && !cond2)
            {
                cond2 = NPC.downedFrost;
            }
            return cond1 && cond2;
        }
    }
}
