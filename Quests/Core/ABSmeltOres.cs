using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class ABSmeltOres : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Smelting Ore";
            SetNPCHead(NPCID.Guide);
            expedition.difficulty = 0;
            expedition.ctgCollect = true;
        }
        public override void AddItemsOnLoad()
        {
            AddDeliverableAnyOf(new int[] {
                ItemID.CopperBar,
                ItemID.TinBar,
                ItemID.IronBar,
                ItemID.LeadBar,
                ItemID.SilverBar,
                ItemID.TungstenBar,
                ItemID.GoldBar,
                ItemID.PlatinumBar
            });
        }
        public override string Description(bool complete)
        {
            return "A furnace is required to craft bars from ore. You can craft this from wood, stone and torches while standing near a workbench. Iron and lead bars can be crafted at a workbench to create an anvil, where you can craft armors and weapons. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return API.FindExpedition<AAWelcomeQuest>(mod).completed;
        }
    }
}
