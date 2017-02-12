using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class EAPumpkinMoon : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "All Hallows' Nightmare";
            SetNPCHead(NPCID.Guide, false);
            expedition.difficulty = 8;
            expedition.ctgSlay = true;

            expedition.conditionDescription1 = "Challenge the Pumpkin Moon";
            expedition.conditionDescriptionCountable = "Reach Wave 12";
            expedition.conditionCountedMax = 12;
            expedition.conditionCountedTrackQuarterCompleted = true;
        }
        public override void AddItemsOnLoad()
        {
            AddRewardMoney(Item.buyPrice(0, 5, 0, 0));
            AddRewardItem(ItemID.GoodieBag, 10);
        }
        public override string Description(bool complete)
        {
            if(!expedition.condition1Met)
            {
                string dryad = NPC.GetFirstNPCNameOrNull(NPCID.Dryad);
                if (dryad == "") dryad = "a dryad";
                return "To summon a Pumpkin Moon you will need a medallion, which requires hallowed bars, ectoplasm and pumpkins, which can be grown from the seeds stocked by " + dryad + ". During the event you will face many tough, halloween-themed enemies and bosses. ";
            }
            return "During a Pumpkin Moon, you will encounter many powerful foes. As you clear waves, stronger enemies will begin to appear, as does the chance of getting items. Try to reach at least the 12th wave before night ends. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Appears after plantera
            return NPC.downedPlantBoss;
        }

        public override void CheckConditionCountable(Player player, ref int count, int max)
        {
            if(Main.pumpkinMoon)
            {
                count = Main.invasionProgressWave;
            }
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond1) cond1 = Main.pumpkinMoon;
            return cond1;
        }
    }
}
