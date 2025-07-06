using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent144.Projs
{
	class WayfarerPike : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			//****DisplayName.SetDefault("Pike");
		}
		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.Spear);
			Projectile.width = 14;
			Projectile.height = 14;
			Projectile.scale = 1.1f;
		}
		
		public const float extend = 0.9f;
		public const float retract = 1.1f;
		public override bool PreAI()
		{
			Player ply = Main.player[Projectile.owner];

			ply.heldProj = Projectile.whoAmI;

			if (Projectile.timeLeft > ply.itemAnimationMax)
				Projectile.timeLeft = ply.itemAnimationMax;

			if (Projectile.ai[0] == 0f)
			{
				Projectile.ai[0] = 4f;
				Projectile.netUpdate = true;
			}
			if (Main.player[Projectile.owner].itemAnimation < Main.player[Projectile.owner].itemAnimationMax / 3)
			{
				Projectile.ai[0] -= retract;
			}
			else
			{
				Projectile.ai[0] += extend;
			}

			Projectile.Center = ply.MountedCenter + Projectile.velocity * Projectile.ai[0];

			if (Projectile.spriteDirection == -1)
				Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI / 4;
			else
				Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI * 3 / 4;

			return false;
		}

	}
}
