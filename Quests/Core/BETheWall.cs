using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class BETheWall : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "The Final Test";
            SetNPCHead(NPCID.Guide);
            expedition.difficulty = 3;
            expedition.ctgSlay = true;
            expedition.ctgImportant = true;

            expedition.conditionDescription1 = "Challenge the keeper of the underworld";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardMoney(Item.buyPrice(0, 2, 0, 0));
        }
        public override string Description(bool complete)
        {
            string message = "When you are ready to challenge the keeper of the underworld, you will have to make a living sacrifice. Everything you need for it can be found in the underworld. ";
            if (!expedition.completed)
            {
                if (expedition.condition1Met)
                {
                    message += "If you are having trouble, consider stocking up on potions and building a bridge to make navigating the underworld less perilous. ";
                }
                else
                {
                    message += "I wish you good luck. ";
                }
            }
            return message;
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (cond1)
            { expedition.conditionDescription2 = "Defeat the Wall of Flesh"; }
            else
            { expedition.conditionDescription2 = ""; }

            // Only appears until an altar is smashed, or is done already
            if (!expedition.completed && WorldGen.altarCount > 0) return false;

            // Appears once any late prehard quest is complete
            return 
                API.FindExpedition<BDJungles>(mod).completed ||
                API.FindExpedition<BDFossils>(mod).completed ||
                API.FindExpedition<BDQBee>(mod).completed ||
                API.FindExpedition<BDDungeonSkell>(mod).completed ||
                API.FindExpedition<BDHellArmour>(mod).completed
                ;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond1)
            {
                if (player.FindBuffIndex(BuffID.Horrified) != -1) cond1 = true;
            }
            if (cond1 && !cond2)
            {
                cond2 = Main.hardMode && player.ZoneUnderworldHeight;
            }
            return cond1 && cond2;
        }
    }
}
