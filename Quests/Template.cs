using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests
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


    }
}
