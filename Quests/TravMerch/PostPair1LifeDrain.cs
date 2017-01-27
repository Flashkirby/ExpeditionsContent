using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.TravMerch
{
    class PostPair1LifeDrain : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Life Drain";
            SetNPCHead(NPCID.TravellingMerchant);
            expedition.difficulty = 5;
            expedition.ctgCollect = true;
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverable(ItemID.GoldCoin);
            AddDeliverableAnyOf(new int[]{
                ItemID.ClingerStaff,
                ItemID.PutridScent,
            }, 1);

            AddRewardItem(ItemID.SoulDrain);
        }
        public override string Description(bool complete)
        {
            return "Whether you run headfirst into danger or sit back and do things from afar, I guarantee you will always want to keep your health in tip top shape! Here's the solution - with this wand you can harm your foes from afar AND heal yourself in the process! ";
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
