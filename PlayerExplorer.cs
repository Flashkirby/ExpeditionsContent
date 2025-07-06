using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

using Expeditions144;
using Terraria.GameContent;

namespace ExpeditionsContent144
{
	public class PlayerExplorer : ModPlayer
	{
		public bool accHeartCompass;
		public bool accFruitCompass;
		public bool accShrineMap;
		public bool stargazer;
		public bool familiarMinion;

		public bool moonlit;

		public static bool HoldingCamera(Mod mod)
		{
			return
				API.InInventory[ModContent.ItemType<Items.QuestItems.PhotoCamera>()] ||
				API.InInventory[ModContent.ItemType<Items.QuestItems.PhotoCamPro>()];
		}

		public static PlayerExplorer Get(Player player, Mod mod)
		{
			return player.GetModPlayer<PlayerExplorer>();
		}

		public override void Initialize()
		{
			accHeartCompass = false;
			accFruitCompass = false;
			accShrineMap = false;
			stargazer = false;
			familiarMinion = false;
		}

		public override void ResetEffects()
		{
			accHeartCompass = false;
			accFruitCompass = false;
			accShrineMap = false;
			stargazer = false;
			familiarMinion = false;

			moonlit = false;

			TryTelescope();
		}

		public override void OnEnterWorld()
		{
			ModMapController.FullMapInitialise();
		}

		public override void PostUpdateEquips()
		{
			// Basically if allied player is in "info" range of 100ft
			// NOTE: Disabled because I haven't set up any net sync for the bools
			// ShareTeamInfo();

			/*
            if (player.controlHook && player.releaseHook)
            {
                Tile t = Main.tile[
                    (int)(Main.mouseX + Main.screenPosition.X) / 16,
                    (int)(Main.mouseY + Main.screenPosition.Y) / 16];
                Main.NewText("Tile @ Mouse = " + t.type + " with frame: " + t.TileFrameX + "|" + t.frameY);
            }
            */
		}

		private void ShareTeamInfo()
		{
			if (Main.netMode == 1 && Player.whoAmI == Main.myPlayer)
			{
				for (int n = 0; n < 255; n++)
				{
					if (n != Player.whoAmI && Main.player[n].active && !Main.player[n].dead && Main.player[n].team == Player.team && Main.player[n].team != 0)
					{
						int num = 800;
						if ((Main.player[n].Center - Player.Center).Length() < (float)num)
						{
							// In range
							if (Get(Player, Mod).accHeartCompass)
							{
								accHeartCompass = true;
							}
							if (Get(Player, Mod).accFruitCompass)
							{
								accFruitCompass = true;
							}
						}
					}
				}
			}
		}

		private const int telescopeRange = 2;
		private void TryTelescope()
		{
			Point p = Player.Top.ToTileCoordinates();
			int tele = ModContent.TileType<Tiles.Telescope>();
			if (Port_TileCounts.telescope == 0) return;

			try
			{
				for (int y = -telescopeRange; y < telescopeRange + 1; y++)
				{
					for (int x = -telescopeRange; x < telescopeRange + 1; x++)
					{
						Tile t = Main.tile[p.X + x, p.Y + y];
						if (t.TileType == tele)
						{
							Player.scope = true;
							if (Player.ZoneOverworldHeight || Player.ZoneSkyHeight)
							{
								stargazer = true;
							}
							break;
						}
					}
				}
			}
			catch { }
		}

		public override void PostUpdateBuffs()
		{
			if (moonlit)
			{
				Player.statDefense -= 10;
			}
		}
		public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
		{
			if (moonlit)
			{
				Texture2D moonlight = TextureAssets.Gore[ModContent.Find<ModGore>("Gores/Moonlight").Type].Value;
				Main.spriteBatch.Draw(moonlight, Player.Center - Main.screenPosition, null,
					new Color(1f, 1f, 1f, 0.3f), 0, new Vector2(moonlight.Width, moonlight.Height) / 2, 1f,
					SpriteEffects.None, 0f);
			}
		}
	}
}
