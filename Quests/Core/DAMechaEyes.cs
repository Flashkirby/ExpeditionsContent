using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class DAMechaEyes : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Paired Stare";
            SetNPCHead(NPCID.Guide);
            expedition.difficulty = 5;
            expedition.ctgSlay = true;
        }
    }
}
