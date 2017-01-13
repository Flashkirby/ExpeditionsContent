using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class ADHooks : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Hooked on Hooks";
            SetNPCHead(NPCID.Guide);
            expedition.difficulty = 0;
            expedition.ctgCollect = true;

            expedition.conditionDescription1 = "Equip a grappling hook";
        }
        public override void AddItemsOnLoad()
        {
        }
        public override string Description(bool complete)
        {
            return "Grappling hooks are an essential tool for exploring, you should craft one at an anvil the next time you have an opportunity. You'll need iron or lead bars to craft 3 chains, and defeat skeletons to obtain a hook. Alternatively, you can craft gem hooks from 15 gemstones of the same color. ";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return API.FindExpedition<ACUnderground>(mod).completed;
        }

        public override bool CheckConditions(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            try
            { cond1 = player.miscEquips[4].type > 0; }
            catch { cond1 = false; }
            return cond1;
        }
    }
}
