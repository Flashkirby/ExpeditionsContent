using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.MiscPre
{
    class ArmsDealerLocket : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Sweet Nothings";
            SetNPCHead(NPCID.ArmsDealer);
            expedition.difficulty = 1;
            expedition.ctgCollect = true;
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverable(mod.ItemType<Items.QuestItems.HeartLocket>(), 1);

            AddRewardItem(ItemID.DPSMeter, 1);
        }
        public override string Description(bool complete)
        {
            string nurse = NPC.GetFirstNPCNameOrNull(NPCID.Nurse);
            if (nurse == "") nurse = "The nurse";
            return nurse + " gave you a silly little locket? Well, I'm a man of priorites, and I think I have a little arrangement that might work out. Would you be interested in this... damage meter? It's not like you need that locket anyway, am I right? ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return API.InInventory[mod.ItemType<Items.QuestItems.HeartLocket>()];
        }
    }
}
