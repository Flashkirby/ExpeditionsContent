using Terraria;
using Terraria.ID;

namespace ExpeditionsContent.Projs.Familiars
{
    class MinionFox : FamiliarMinion
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Familiar Fox");
            Main.projFrames[projectile.type] = 13;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
            ProjectileID.Sets.Homing[projectile.type] = true;

            AIPrioritiseNearPlayer = true;
            AIPrioritiseFarEnemies = false;

            // What does the fox say? "pls don't reference null instances"
            if (Main.netMode == 2) return;

            drawOriginOffsetY = (Main.projectileTexture[projectile.type].Width - projectile.width) / 2;
            drawOffsetX = (Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type]) - projectile.height - 4;
        }
        public override void SetDefaults()
        {
            projectile.netImportant = true;
            projectile.width = 24;
            projectile.height = 22;

            projectile.minion = true;
            projectile.minionSlots = 1;
            projectile.penetrate = -1;
            projectile.timeLeft *= 5;
            projectile.netImportant = true;
        }
    }
}
