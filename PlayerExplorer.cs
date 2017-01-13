using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.Map;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent
{
    public class PlayerExplorer : ModPlayer
    {
        public bool accHeartCompass;
        public bool accFruitCompass;
        public bool familiarMinion;

        public static PlayerExplorer Get(Player player, Mod mod)
        {
            return player.GetModPlayer<PlayerExplorer>(mod);
        }

        public override void Initialize()
        {
            accHeartCompass = false;
            accFruitCompass = false;
            familiarMinion = false;
        }

        public override void ResetEffects()
        {
            accHeartCompass = false;
            accFruitCompass = false;
            familiarMinion = false;
        }

        public override void PostUpdateEquips()
        {
            // Basically if allied player is in "info" range of 100ft
            // NOTE: Disabled because I haven't set up any net sync for the bools
            // ShareTeamInfo();
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

    }
}
