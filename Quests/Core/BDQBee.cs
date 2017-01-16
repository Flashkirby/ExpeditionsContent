using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class BDQBee : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Royal Beetdown";
            SetNPCHead(NPCID.Guide);
            expedition.difficulty = 3;
            expedition.ctgSlay = true;

            expedition.conditionDescription1 = "Face the hive guardian";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardMoney(Item.buyPrice(0, 2, 0, 0));
        }
        public override string Description(bool complete)
        {
            return "The giant bee hives of the jungle each contain a delicate larva. Slaying it will anger the dangerous queen of the hive, so make sure to prepare yourself before diving in. Should you be up for the challenge, the queen carries some very interesting potential rewards if you favor allies in combat. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (cond1)
            { expedition.conditionDescription2 = "Defeat the Queen Bee"; }
            else
            { expedition.conditionDescription2 = ""; }

            // Only appears until hardmode, or is done already
            if (!expedition.completed && Main.hardMode) return false;

            // Appears once the second main boss is defeated AND turned in
            return API.FindExpedition<BCBoss2>(mod).completed;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond1)
            {
                int type = API.LastHitNPC.type;
                cond1 = (type == NPCID.QueenBee);
            }
            if (cond1 && !cond2)
            {
                cond2 = NPC.downedQueenBee;
            }
            return cond1 && cond2;
        }
    }
}
