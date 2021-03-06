﻿using Terraria;
using Terraria.ID;

namespace ExpeditionsContent.Projs.Familiars
{
    class MinionChicken : FamiliarMinion
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Familiar Fowl");
            Main.projFrames[projectile.type] = 15;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
            ProjectileID.Sets.Homing[projectile.type] = true;

            // Animation Frames
            attackFrame = 8;
            attackFrameCount = 3;
            runFrame = 1;
            runFrameCount = 7;
            flyFrame = 11;
            flyFrameSpeed = 3;
            flyRotationMod = 0.3f;
            fallFrame = 11;

            // No servers allowed. Only authorised clients and hosts.
            if (Main.netMode == 2) return;

            drawOriginOffsetY = (Main.projectileTexture[projectile.type].Width - projectile.width) / 2;
            drawOffsetX = (Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type]) - projectile.height - 4;
        }
        public override void SetDefaults()
        {
            projectile.netImportant = true;
            projectile.width = 30;
            projectile.height = 24;

            projectile.minion = true;
            projectile.minionSlots = 1;
            projectile.penetrate = -1;
            projectile.timeLeft *= 5;
            projectile.netImportant = true;

            AIPrioritiseNearPlayer = false;
            AIPrioritiseFarEnemies = false;
        }
    }
}
