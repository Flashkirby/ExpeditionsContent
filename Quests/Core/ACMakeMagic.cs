using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class ACMakeMagic : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Gift from the Stars";
            SetNPCHead(NPCID.Guide);
            expedition.difficulty = 0;
            expedition.ctgCollect = true;

            expedition.conditionDescription1 = "Reach 40 maximum base mana";
        }
        public override void AddItemsOnLoad()
        {
            AddRewardItem(ItemID.FallenStar);
        }
        public override string Description(bool complete)
        {
            return "Falling stars will appear across the world at night. If you grab enough you can make yourself a Mana Crystal which will increase your magic capacity. Be careful though, as fallen stars lying around after sunrise will vanish. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Only appears until first boss is beaten, or is done already
            if (!expedition.completed && !NPC.downedBoss1) return false;

            return API.FindExpedition<ABMapping>(mod).completed;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            cond1 = player.statManaMax >= 40;
            return cond1;
        }
    }
}
