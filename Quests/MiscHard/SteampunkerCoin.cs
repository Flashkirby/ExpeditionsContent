using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.MiscHard
{
    class SteampunkerCoin : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Old Denominations";
            SetNPCHead(NPCID.Steampunker);
            expedition.difficulty = 5;
            expedition.ctgCollect = true;
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverable(mod.ItemType<Items.QuestItems.BrassCoin>(), 1);

            AddRewardItem(ItemID.MetalDetector, 1);
        }
        public override string Description(bool complete)
        {
            return "This metal detector here is awfully poor... doesn't even work on brass. Got anything of interest? I'm all ears, 'specially if it's brass!";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if (!cond1) cond1 = API.TimeAfternoon && Main.moonPhase == 2;
            return cond1 && API.FindExpedition<TaxCollectorCoin>(mod).completed;
        }
    }
}