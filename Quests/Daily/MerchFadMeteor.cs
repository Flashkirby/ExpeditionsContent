using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Daily
{
    class MerchFadMeteor : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Meteoric Interest";
            SetNPCHead(NPCID.Merchant);
            expedition.difficulty = 2;
            expedition.ctgCollect = true;
            expedition.ctgImportant = true;
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverable(ItemID.MeteoriteChair, 1);
            AddDeliverable(ItemID.MeteoriteTable, 1);
            AddDeliverableAnyOf(new int[] {
                ItemID.MeteoriteSofa,
                ItemID.MeteoriteSink,
                ItemID.MeteoriteChandelier}, 1);
            AddRewardItem(API.ItemIDExpeditionCoupon, 1);
        }
        public override string Description(bool complete)
        {
            string clerk = NPC.GetFirstNPCNameOrNull(ExpeditionC.NPCIDClerk);
            if (clerk == "") clerk = "the clerk";
            return "There's an fad going on for meteorite! If you can aquire a selection of furniture for me, I could make a big profit! Oh, and " + clerk + " will give you something for your troubles, I'm sure. ";
        }

        public override bool IncludeAsDaily()
        {
            return NPC.FindFirstNPC(NPCID.Merchant) >= 0 && WorldGen.shadowOrbSmashed;
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return API.IsDaily(expedition);
        }
    }
}
