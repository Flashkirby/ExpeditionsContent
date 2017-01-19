using System;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.Map;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent
{
    public class NPCExplorer : GlobalNPC
    {
        public override void GetChat(NPC npc, ref string chat)
        {
            if (npc.type == NPCID.Guide)
            {
                if (Main.dayTime)
                {
                    switch(Main.rand.Next(4))
                    {
                        case 0:
                            chat = "You can craft an expedition board at a workbench using wood. I will post additional advice on there any time you need help. ";
                            break;
                    }
                }
            }
        }
    }
}
