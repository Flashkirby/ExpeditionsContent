using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class ADKingSlime : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Slimy Monarch";
            SetNPCHead(NPCID.Guide, false);
            expedition.difficulty = 1;
            expedition.ctgSlay = true;

            expedition.conditionDescription1 = "Face the crowned slime";
            // expedition.conditionDescription3 = "Experience a slime rain";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardMoney(Item.buyPrice(0, 0, 5, 0));
        }
        public override string Description(bool complete)
        {
            if(Main.player[Main.myPlayer].statLifeMax < 140 ||
                Main.player[Main.myPlayer].statDefense < 8)
            {
                return "A great and powerful slime rarely visits these lands, by sky or by sea. You are not yet ready to fight it though. I recommend equipping some better armour and increasing your number of hearts first. ";
            }
            return "A great and powerful slime rarely visits these lands, by sky or by sea. If you are up for a challenge, the slime carries many useful items that will certainly help you overcome tougher foes. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond1)
            {
                expedition.conditionDescription2 = "";
            }
            else
            { expedition.conditionDescription2 = "Defeat the King Slime"; }

            if (!cond3) cond3 = Main.slimeRain;
            return API.FindExpedition<ACUnderground>(mod).completed || cond1 || cond3;
        }

        public override void OnCombatWithNPC(NPC npc, bool playerGotHit, Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!expedition.condition1Met)
                expedition.condition1Met = npc.type == NPCID.KingSlime;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {

            if (cond1 && !cond2) cond2 = NPC.downedSlimeKing;
            return cond1 && cond2;
        }
    }
}
