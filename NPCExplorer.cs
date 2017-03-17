using System;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.Map;
using Terraria.ID;
using Terraria.ModLoader;

using Expeditions;
using Microsoft.Xna.Framework.Graphics;

namespace ExpeditionsContent
{
    public class NPCExplorer : GlobalNPC
    {
        public override void GetChat(NPC npc, ref string chat)
        {
            if (npc.type == NPCID.Guide &&
                !Main.hardMode &&
                !API.FindExpedition<Quests.Core.AAWelcomeQuest>(mod).completed
                )
            {
                if (Main.dayTime)
                {
                    switch(Main.rand.Next(2))
                    {
                        case 0:
                            chat = "You can craft an expedition board at a workbench using wood. I will post additional advice on there any time you need help. ";
                            break;
                    }
                }
            }
        }

        #region ModInfo FX
        public override void ResetEffects(NPC npc)
        {
            npc.GetModInfo<ModNPCInfo>(mod).moonlight = false;
        }

        public override bool StrikeNPC(NPC npc, ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            int tempDefence = 0;
            if (npc.GetModInfo<ModNPCInfo>(mod).moonlight) tempDefence -= 10;
            if (tempDefence != 0)
            {
                double currentDamage = Main.CalculateDamage((int)damage, defense);
                double modifiedDamage = Main.CalculateDamage((int)damage, Math.Max(0, defense + tempDefence));
                damage += modifiedDamage - currentDamage;
            }
            return true;
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (npc.GetModInfo<ModNPCInfo>(mod).moonlight)
            {
                drawColor.R = 255;
                drawColor.G = 255;
                drawColor.B = 255;
            }
        }
        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
        {
            if (npc.GetModInfo<ModNPCInfo>(mod).moonlight)
            {
                Texture2D moonlight = Main.goreTexture[mod.GetGoreSlot("Gores/Moonlight")];
                float scale = npc.scale * Math.Max(npc.width, npc.height) / 56f;
                spriteBatch.Draw(moonlight, npc.Center - Main.screenPosition, null,
                    new Color(1f, 1f, 1f, 0.3f), 0, new Vector2(moonlight.Width, moonlight.Height) / 2, scale,
                    SpriteEffects.None, 0f);
            }
        }
        #endregion
    }
}
