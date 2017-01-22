using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.TravMerch
{
    class PrePair4BrainOfConfusion : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Trading Brain of Confusion";
            SetNPCHead(NPCID.TravellingMerchant);
            expedition.difficulty = 1;
            expedition.ctgCollect = true;
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverable(ItemID.GoldCoin);
            AddDeliverable(ItemID.WormScarf);

            AddRewardItem(ItemID.BrainOfConfusion);
        }
        public override string Description(bool complete)
        {
            return "Getting mobbed? Well you need this confusing item! Send your enemies running about any time they lay a scratch on you. Honestly even I don't know how this thing works! ";
        }

        public override void OnNewDay()
        {
            expedition.ResetProgress(true); //Reset after trade use
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Must have travelling merchant present
            if (NPC.FindFirstNPC(NPCID.TravellingMerchant) == -1) return false;

            return NPC.downedBoss2 && Main.expertMode && !WorldGen.crimson;
        }
    }
}
