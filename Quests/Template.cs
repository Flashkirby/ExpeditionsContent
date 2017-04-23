using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Daily
{
    class Template : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "NAME";
            SetNPCHead(NPCID.Guide, false);
            expedition.difficulty = 0;
            expedition.ctgExplore = true;
        }
        public override void AddItemsOnLoad()
        {
        }
        public override string Description(bool complete)
        {
            return "DESCRIPTION";
        }

        public override bool CheckPrerequisites(Player player, ref bool cond1, ref bool cond2, ref bool cond3, bool condCount)
        {
            return false;
        }
    }
}
