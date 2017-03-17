using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

using Expeditions;

namespace ExpeditionsContent
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
                API.InInventory[mod.ItemType<Items.PhotoCamera>()] ||
                API.InInventory[mod.ItemType<Items.PhotoCamPro>()];
        }

        public static PlayerExplorer Get(Player player, Mod mod)
        {
            return player.GetModPlayer<PlayerExplorer>(mod);
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

        public override void OnEnterWorld(Player player)
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
                Main.NewText("Tile @ Mouse = " + t.type + " with frame: " + t.frameX + "|" + t.frameY);
            }
            */
        }

        private void ShareTeamInfo()
        {
            if (Main.netMode == 1 && player.whoAmI == Main.myPlayer)
            {
                for (int n = 0; n < 255; n++)
                {
                    if (n != player.whoAmI && Main.player[n].active && !Main.player[n].dead && Main.player[n].team == player.team && Main.player[n].team != 0)
                    {
                        int num = 800;
                        if ((Main.player[n].Center - player.Center).Length() < (float)num)
                        {
                            // In range
                            if (Get(player, mod).accHeartCompass)
                            {
                                accHeartCompass = true;
                            }
                            if (Get(player, mod).accFruitCompass)
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
            Point p = player.Top.ToTileCoordinates();
            int tele = mod.TileType<Tiles.Telescope>();
            if (Main.screenTileCounts[tele] == 0) return;

            try
            {
                for (int y = -telescopeRange; y < telescopeRange + 1; y++)
                {
                    for (int x = -telescopeRange; x < telescopeRange + 1; x++)
                    {
                        Tile t = Main.tile[p.X + x, p.Y + y];
                        if (t.type == tele)
                        {
                            player.scope = true;
                            if (player.ZoneOverworldHeight || player.ZoneSkyHeight)
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
                player.statDefense -= 10;
            }
        }
        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if(moonlit)
            {
                Texture2D moonlight = Main.goreTexture[mod.GetGoreSlot("Gores/Moonlight")];
                Main.spriteBatch.Draw(moonlight, player.Center - Main.screenPosition, null,
                    new Color(1f, 1f, 1f, 0.3f), 0, new Vector2(moonlight.Width, moonlight.Height) / 2, 1f,
                    SpriteEffects.None, 0f);
            }
        }
    }
}
