using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.TravMerch
{
    class PostPair2FleshKnuckles : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Trading Flesh Knuckles";
            SetNPCHead(NPCID.TravellingMerchant);
            expedition.difficulty = 5;
            expedition.ctgCollect = true;
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverable(ItemID.GoldCoin);
            AddDeliverableAnyOf(new int[]{
                ItemID.PutridScent,
                ItemID.WormHook,
            }, 1);

            AddRewardItem(ItemID.FleshKnuckles);
        }
        public override string Description(bool complete)
        {
            return "Come one, come all! Show your enemies no fear, for with these fleshy knuckles you'll be the center of attention! A solid choice for the aspiring invicible knight. ";
        }

        public override void OnNewDay(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            expedition.ResetProgress(true); //Reset after trade use
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Must have travelling merchant present
            if (NPC.FindFirstNPC(NPCID.TravellingMerchant) == -1) return false;

            return NPC.downedMechBossAny && !WorldGen.crimson;
        }
    }
}
