using System;
using Terraria;
using Terraria.ID;
using Expeditions;

namespace ExpeditionsContent.Quests.Core
{
    class DAMechaPrime : ModExpedition
    {
        public override void SetDefaults()
        {
            expedition.name = "Armed and Dangerous";
            SetNPCHead(NPCID.Guide);
            expedition.difficulty = 5;
            expedition.ctgSlay = true;
        }
    }
}
