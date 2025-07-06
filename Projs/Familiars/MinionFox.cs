using Terraria;
using Terraria.GameContent;
using Terraria.ID;

namespace ExpeditionsContent144.Projs.Familiars
{
	class MinionFox : FamiliarMinion
	{
		public override void SetStaticDefaults()
		{
			//****DisplayName.SetDefault("Familiar Fox");
			Main.projFrames[Type] = 13;
			ProjectileID.Sets.MinionSacrificable[Type] = true;
			ProjectileID.Sets.MinionTargettingFeature[Type] = true;
			ProjectileID.Sets.CultistIsResistantTo[Type] = true; //**** was "Homming" instead of "CultistIsResistantTo"

			AIPrioritiseNearPlayer = true;
			AIPrioritiseFarEnemies = false;

			// What does the fox say? "pls don't reference null instances"
			if (Main.netMode == 2) return;

			DrawOriginOffsetY = (TextureAssets.Projectile[Type].Width() - Projectile.width) / 2;
			DrawOffsetX = (TextureAssets.Projectile[Type].Height() / Main.projFrames[Type]) - Projectile.height - 4;
		}
		public override void SetDefaults()
		{
			Projectile.netImportant = true;
			Projectile.width = 24;
			Projectile.height = 22;

			Projectile.minion = true;
			Projectile.minionSlots = 1;
			Projectile.penetrate = -1;
			Projectile.timeLeft *= 5;
			Projectile.netImportant = true;
		}
	}
}
