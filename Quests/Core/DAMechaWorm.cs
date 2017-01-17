using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class DAMechaWorm : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Metal Worm";
            SetNPCHead(NPCID.Guide);
            expedition.difficulty = 5;
            expedition.ctgSlay = true;
        }
    }
}
