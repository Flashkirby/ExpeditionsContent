using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.MiscHard
{
    class TaxCollectorTally : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Count in Peace";
            SetNPCHead(NPCID.TaxCollector);
            expedition.difficulty = 4;
            expedition.ctgExplore = true;

            expedition.conditionDescriptionCountable = "Let me collect taxes";
            expedition.conditionCountedMax = Item.buyPrice(0, 10, 0, 0) / Item.buyPrice(0, 0, 0, 50);
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(ItemID.TallyCounter, 1);
        }
        public override string Description(bool complete)
        {
            return "Look, you want your share of coin? Well how about you leave me be! Here, I'll even throw in one of my doohickeys to motivate ya, not that I'd need it if you let me do my job... young people these days, tch!";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond1)
            {
                cond1 = player.taxMoney == 0;
            }
            if(cond1 && !cond2)
            {
                foreach(NPC npc in Main.npc)
                {
                    if(npc.active && npc.type == NPCID.TaxCollector)
                    {
                        cond2 = true;
                        break;
                    }
                }
            }
            return cond1 && cond2;
        }

        public override void CheckConditionCountable(Player player, ref int count, int max)
        {
            if (expedition.condition3Met)
            {
                count = max;
                return; // BREAK
            }

            count = player.taxMoney / Item.buyPrice(0, 0, 0, 50);
            if(count == max)
            {
                expedition.condition3Met = true;
            }
        }
    }
}