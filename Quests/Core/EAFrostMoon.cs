using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class EAFrostMoon : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Silent Night";
            SetNPCHead(NPCID.Guide, false);
            expedition.difficulty = 8;
            expedition.ctgSlay = true;

            expedition.conditionDescription1 = "Challenge the Frost Moon";
            expedition.conditionDescriptionCountable = "Reach Wave 12";
            expedition.conditionCountedMax = 12;
            expedition.conditionCountedTrackQuarterCompleted = true;
        }
        public override void AddItemsOnLoad()
        {
            AddRewardMoney(Item.buyPrice(0, 5, 0, 0));
            AddRewardItem(ItemID.Present, 10);
        }
        public override string Description(bool complete)
        {
            if(!expedition.condition1Met)
            {
                return "You can summon a Frost Moon with a Naughty Present, which requires silk, ectoplasm and souls from Skeletron Prime. During the event you will face many tough, winter-themed enemies and bosses. ";
            }
            return "During a Frost Moon, you will encounter many powerful foes. As you clear waves, stronger enemies will begin to appear, as does the chance of getting items. Try to reach at least the 12th wave before night ends. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Appears after plantera
            return NPC.downedPlantBoss;
        }

        public override void CheckConditionCountable(Player player, ref int count, int max)
        {
            if(Main.snowMoon)
            {
                count = Main.invasionProgressWave;
            }
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond1) cond1 = Main.snowMoon;
            return cond1;
        }
    }
}
