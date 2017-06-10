using System;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Expeditions;
using Microsoft.Xna.Framework.Graphics;

namespace ExpeditionsContent
{
    public class NPCExplorer : GlobalNPC
    {
        public override bool InstancePerEntity { get { return true; } }
        public bool moonlight = false;

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
            moonlight = false;
        }

        public override bool StrikeNPC(NPC npc, ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            int tempDefence = 0;
            if (moonlight) tempDefence -= 10;
            if (tempDefence != 0)
            {
                double currentDamage = Main.CalculateDamage((int)damage, defense);
                double modifiedDamage = Main.CalculateDamage((int)damage, Math.Max(0, defense + tempDefence));
                damage += modifiedDamage - currentDamage;
            }
            return true;
        }

        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
        {
            if (moonlight)
            {
                Texture2D texture = Main.goreTexture[mod.GetGoreSlot("Gores/Moonlight")];
                float scale = 0.8f * npc.scale * Math.Max(npc.width, npc.height) / 56f;
                if (scale <= 0.1f) scale = 0.1f;
                if (scale > 3) scale = 3 + (scale - 3) / 2;
                spriteBatch.Draw(texture, npc.Center - Main.screenPosition + new Vector2(0, 20), null,
                    new Color(1f, 1f, 1f, 0.3f), 0, new Vector2(texture.Width, texture.Height) / 2, scale,
                    SpriteEffects.None, 0f);
            }
        }
        #endregion
    }
}
