using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.TravMerch
{
    class PostPair1ClingerStaff : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Trading Clinger Staff";
            SetNPCHead(NPCID.TravellingMerchant);
            expedition.difficulty = 5;
            expedition.ctgCollect = true;
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverable(ItemID.GoldCoin);
            AddDeliverableAnyOf(new int[]{
                ItemID.SoulDrain,
                ItemID.FleshKnuckles,
            }, 1);

            AddRewardItem(ItemID.ClingerStaff);
        }
        public override string Description(bool complete)
        {
            return "For wizards and witches everywhere, this is a weapon for you! Zoning out your enemies and watching your own back is as easy as ever with this staff, owing to its ability to create walls cursed flames! ";
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
            if (!API.InInventory[ItemID.SoulDrain] &&
                !API.InInventory[ItemID.FleshKnuckles]
                ) return false;

            return NPC.downedMechBossAny && WorldGen.crimson;
        }
    }
}
