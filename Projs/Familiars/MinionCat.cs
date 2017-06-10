using Terraria;
using Terraria.ID;

namespace ExpeditionsContent.Projs.Familiars
{
    class MinionCat : FamiliarMinion
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Familiar Feline");
            Main.projFrames[projectile.type] = 14;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
            ProjectileID.Sets.Homing[projectile.type] = true;

            AIPrioritiseNearPlayer = true;
            AIPrioritiseFarEnemies = true;

            // Animation Frames
            attackFrame = 1;
            attackFrameCount = 4;
            runFrame = 1;
            runFrameCount = 8;
            flyFrame = 9;
            flyFrameSpeed = 5;
            flyRotationMod = 0.5f;
            fallFrame = 13;

            // Hark! Servers! Do not gaze upon ye unloaded texture arrays, lest you be ailed by object reference errors
            if (Main.netMode == 2) return;

            drawOriginOffsetY = (Main.projectileTexture[projectile.type].Width - projectile.width) / 2;
            drawOffsetX = (Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type]) - projectile.height - 4;
        }
        public override void SetDefaults()
        {
            projectile.netImportant = true;
            projectile.width = 28;
            projectile.height = 18;

            projectile.minion = true;
            projectile.minionSlots = 1;
            projectile.penetrate = -1;
            projectile.timeLeft *= 5;
            projectile.netImportant = true;
        }
    }
}
