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

            expedition.conditionDescription1 = "Craft a bar";
        }
        public override void AddItemsOnLoad()
        {
        }
        public override string Description(bool complete)
        {
            return "A furnace is required to craft bars from ore. You can craft this from wood, stone and torches while standing near a workbench. Iron and lead bars can be crafted at a workbench to create an anvil, where you can craft armors and weapons. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return API.FindExpedition<AAWelcomeQuest>(mod).completed;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            if(!cond1)
            {
                int type = player.inventory[player.selectedItem].type;
                if (type == ItemID.CopperBar ||
                    type == ItemID.TinBar ||
                    type == ItemID.IronBar ||
                    type == ItemID.LeadBar ||
                    type == ItemID.SilverBar ||
                    type == ItemID.TungstenBar ||
                    type == ItemID.GoldBar ||
                    type == ItemID.PlatinumBar ||
                    type == ItemID.DemoniteBar ||
                    type == ItemID.CrimtaneBar ||
                    type == ItemID.MeteoriteBar )
                {
                    cond1 = true;
                }
            }
            return cond1;
        }
    }
}
