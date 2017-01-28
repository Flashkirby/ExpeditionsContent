using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Daily
{
    class MerchFadGlass : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "The New Look";
            SetNPCHead(NPCID.Merchant);
            expedition.difficulty = 2;
            expedition.ctgCollect = true;
            expedition.ctgImportant = true;
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverable(ItemID.GlassChair, 1);
            AddDeliverable(ItemID.GlassTable, 1);
            AddDeliverableAnyOf(new int[] {
                ItemID.GlassSofa,
                ItemID.GlassSink,
                ItemID.GlassChandelier}, 1);
            AddRewardItem(API.ItemIDExpeditionCoupon, 1);
        }
        public override string Description(bool complete)
        {
            string clerk = NPC.GetFirstNPCNameOrNull(ExpeditionC.NPCIDClerk);
            if (clerk == "") clerk = "the clerk";
            return "There's an fad going on for glass! If you can aquire a selection of furniture for me, I could make a big profit! Oh, and " + clerk + " will give you something for your troubles, I'm sure. ";
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
