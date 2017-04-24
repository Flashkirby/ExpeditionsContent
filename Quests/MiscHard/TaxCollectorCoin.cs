using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.MiscHard
{
    class TaxCollectorCoin : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Buried Treasures";
            SetNPCHead(NPCID.TaxCollector);
            expedition.difficulty = 4;
            expedition.ctgCollect = true;
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverable(ItemID.PharaohsMask, 1);

            AddRewardItem(mod.ItemType<Items.QuestItems.BrassCoin>(), 1);
        }
        public override string Description(bool complete)
        {
            return "There's something rare I want that money apparently 'can't buy'. Well I won't stand for it, and you look like you have nothing else to do. Get me a pharaoh's mask, I need it for my collection.";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if(!cond1)
            {
                cond1 = player.ZoneDesert && (
                    Main.screenTileCounts[789] > 1 || // Ankh Banner
                    Main.screenTileCounts[790] > 1 || // Snake Banner
                    Main.screenTileCounts[791] > 1 ); // Omega Banner
            }
            return cond1;
        }
    }
}