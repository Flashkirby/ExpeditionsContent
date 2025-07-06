using System;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using Expeditions144;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace ExpeditionsContent144
{
	public class NPCExplorer : GlobalNPC
	{
		public override bool InstancePerEntity { get { return true; } }
		public bool moonlight = false;

		public override void GetChat(NPC npc, ref string chat)
		{
			if (npc.type == NPCID.Guide &&
				!Main.hardMode &&
				!API.FindExpedition<Quests.Core.AAWelcomeQuest>(Mod).completed
				)
			{
				if (Main.dayTime)
				{
					switch (Main.rand.Next(2))
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

		public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
		{
			if (moonlight)
				modifiers.Defense.Flat -= 10;
		}

		public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			if (moonlight)
			{
				Texture2D texture = TextureAssets.Gore[Mod.Find<ModGore>("Gores/Moonlight").Type].Value;
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
