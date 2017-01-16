using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class BDDungeonSkell : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "A Bone to Pick";
            SetNPCHead(NPCID.Guide);
            expedition.difficulty = 3;
            expedition.ctgSlay = true;

            expedition.conditionDescription1 = "Face the dungeon keeper";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardMoney(Item.buyPrice(0, 2, 0, 0));
        }
        public override string Description(bool complete)
        {
            string message = "A good place to head to next would be the dungeon, where the restless skeletons protect powerful treasures. ";
            if (complete) message += "The bones dropped by skeletons can also be used to craft a set of armor for ranged weapons. ";
            return message + "There is an old man there who should be able to help you gain entry. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (cond1)
            { expedition.conditionDescription2 = "Defeat Skeletron"; }
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
                cond1 = (
                    type == NPCID.SkeletronHead ||
                    type == NPCID.SkeletronHand);
            }
            if (cond1 && !cond2)
            {
                cond2 = NPC.downedQueenBee;
            }
            return cond1 && cond2;
        }
    }
}
