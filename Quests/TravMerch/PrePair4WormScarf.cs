using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.TravMerch
{
    class PrePair4WormScarf : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Trading Worm Scarf";
            SetNPCHead(NPCID.TravellingMerchant);
            expedition.difficulty = 1;
            expedition.ctgCollect = true;
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverable(ItemID.GoldCoin);
            AddDeliverable(ItemID.BrainOfConfusion);

            AddRewardItem(ItemID.WormScarf);
        }
        public override string Description(bool complete)
        {
            return "Having a tough time dealing with big hitters? This comfy scarf softens any and all blows, keeping you that much further from deaths clutches. ";
        }

        public override void OnNewDay()
        {
            expedition.ResetProgress(true); //Reset after trade use
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Must have travelling merchant present
            if (NPC.FindFirstNPC(NPCID.TravellingMerchant) == -1) return false;

            return NPC.downedBoss2 && Main.expertMode && WorldGen.crimson;
        }
    }
}
