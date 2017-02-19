using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.TravMerch
{
    class PostPair2PutridScent : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Trading Putrid Scent";
            SetNPCHead(NPCID.TravellingMerchant);
            expedition.difficulty = 5;
            expedition.ctgCollect = true;
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverable(ItemID.GoldCoin);
            AddDeliverableAnyOf(new int[]{
                ItemID.FleshKnuckles,
                ItemID.TendonHook,
            }, 1);

            AddRewardItem(ItemID.PutridScent);
        }
        public override string Description(bool complete)
        {
            return "Finding yourself at the center of unwanted attention? Keep unwanted suitors away with this... 'unique' aroma. Its scent is guranteed to sharpern your senses, and your weapons! ";
        }

        public override void OnNewDay(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            expedition.ResetProgress(true); //Reset after trade use
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            // Must have travelling merchant present
            if (NPC.FindFirstNPC(NPCID.TravellingMerchant) == -1) return false;

            //Won't offer unless item is held
            if (!API.InInventory[ItemID.FleshKnuckles] &&
                !API.InInventory[ItemID.TendonHook]
                ) return false;

            return NPC.downedMechBossAny && WorldGen.crimson;
        }
    }
}
