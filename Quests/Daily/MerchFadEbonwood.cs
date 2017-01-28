using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Daily
{
    class MerchFadEbonwood : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Ebonwood Fever";
            SetNPCHead(NPCID.Merchant);
            expedition.difficulty = 1;
            expedition.ctgCollect = true;
            expedition.ctgImportant = true;
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverable(ItemID.EbonwoodChair, 1);
            AddDeliverable(ItemID.EbonwoodTable, 1);
            AddDeliverableAnyOf(new int[] {
                ItemID.EbonwoodSofa,
                ItemID.EbonwoodSink,
                ItemID.EbonwoodChandelier}, 1);
            AddRewardItem(API.ItemIDExpeditionCoupon, 1);
        }
        public override string Description(bool complete)
        {
            string clerk = NPC.GetFirstNPCNameOrNull(ExpeditionC.NPCIDClerk);
            if (clerk == "") clerk = "the clerk";
            return "There's a fad going on for ebonwood! If you can aquire a selection of furniture for me, I could make a big profit! Oh, and " + clerk + " will give you something for your troubles, I'm sure. ";
        }

        public override bool IncludeAsDaily()
        {
            return NPC.FindFirstNPC(NPCID.Merchant) >= 0;
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return API.IsDaily(expedition);
        }
    }
}
