using System;

using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent
{
    public class PlayerExplorer : ModPlayer
    {
        public bool familiarMinion;
        public override void ResetEffects()
        {
            familiarMinion = false;
        }
    }
}
