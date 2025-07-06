using Terraria;
using Terraria.GameContent;
using Terraria.ID;

namespace ExpeditionsContent144.Projs.Familiars
{
	class MinionChicken : FamiliarMinion
	{
		public override void SetStaticDefaults()
		{
			//****DisplayName.SetDefault("Familiar Fowl");
			Main.projFrames[Type] = 15;
			ProjectileID.Sets.MinionSacrificable[Type] = true;
			ProjectileID.Sets.MinionTargettingFeature[Type] = true;
			ProjectileID.Sets.CultistIsResistantTo[Type] = true; //**** was "Homming" instead of "CultistIsResistantTo"

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

			DrawOriginOffsetY = (TextureAssets.Projectile[Type].Width() - Projectile.width) / 2;
			DrawOffsetX = (TextureAssets.Projectile[Type].Height() / Main.projFrames[Type]) - Projectile.height - 4;
		}
		public override void SetDefaults()
		{
			Projectile.netImportant = true;
			Projectile.width = 30;
			Projectile.height = 24;

			Projectile.minion = true;
			Projectile.minionSlots = 1;
			Projectile.penetrate = -1;
			Projectile.timeLeft *= 5;
			Projectile.netImportant = true;

			AIPrioritiseNearPlayer = false;
			AIPrioritiseFarEnemies = false;
		}
	}
}
