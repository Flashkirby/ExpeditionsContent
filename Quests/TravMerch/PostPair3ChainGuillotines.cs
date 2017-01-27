using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.TravMerch
{
    class PostPair3ChainGuillotines : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Chain Guillotines";
            SetNPCHead(NPCID.TravellingMerchant);
            expedition.difficulty = 5;
            expedition.ctgCollect = true;
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverable(ItemID.GoldCoin);
            AddDeliverableAnyOf(new int[]{
                ItemID.FetidBaghnakhs,
                ItemID.DartPistol,
            }, 1);

            AddRewardItem(ItemID.PutridScent);
        }
        public override string Description(bool complete)
        {
            return "Wow your enemies with this amazing collection of blades. Give them a toss, give them a whirl, and your foes will drop like flies! ";
        }

        public override void OnNewDay(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            expedition.ResetProgress(true); //Reset after trade use
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Must have travelling merchant present
            if (NPC.FindFirstNPC(NPCID.TravellingMerchant) == -1) return false;

            return NPC.downedMechBossAny && WorldGen.crimson;
        }
    }
}
