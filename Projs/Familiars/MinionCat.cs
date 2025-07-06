using Terraria;
using Terraria.GameContent;
using Terraria.ID;

namespace ExpeditionsContent144.Projs.Familiars
{
	class MinionCat : FamiliarMinion
	{
		public override void SetStaticDefaults()
		{
			//****DisplayName.SetDefault("Familiar Feline");
			Main.projFrames[Type] = 14;
			ProjectileID.Sets.MinionSacrificable[Type] = true;
			ProjectileID.Sets.MinionTargettingFeature[Type] = true;
			ProjectileID.Sets.CultistIsResistantTo[Type] = true; //**** was "Homming" instead of "CultistIsResistantTo"

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

			DrawOriginOffsetY = (TextureAssets.Projectile[Type].Width() - Projectile.width) / 2;
			DrawOffsetX = (TextureAssets.Projectile[Type].Height() / Main.projFrames[Type]) - Projectile.height - 4;
		}
		public override void SetDefaults()
		{
			Projectile.netImportant = true;
			Projectile.width = 28;
			Projectile.height = 18;

			Projectile.minion = true;
			Projectile.minionSlots = 1;
			Projectile.penetrate = -1;
			Projectile.timeLeft *= 5;
			Projectile.netImportant = true;
		}
	}
}
