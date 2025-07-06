using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent144.Projs
{
	class Gust : ModProjectile
	{
		public const int extraSize = 16;
		public override void SetStaticDefaults()
		{
			//****DisplayName.SetDefault("Gust");
		}
		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.WaterBolt);
			Projectile.aiStyle = 0;

			Projectile.width = 28 + extraSize;
			Projectile.height = 28 + extraSize;
			Projectile.timeLeft = 180;
		}

		public override void AI()
		{
			Projectile.velocity *= 0.98f;

			if ((int)Projectile.ai[0] % 3 == 0)
			{
				int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height,
					16, Projectile.velocity.X, Projectile.velocity.Y, 200, default(Color), 0.7f);
				Main.dust[d].fadeIn = 1.5f;
				Main.dust[d].velocity *= 0.1f;

				int alpha = (int)MathHelper.Clamp(255 - Projectile.timeLeft, 0, 256);
				int g = Gore.NewGore(
					Projectile.GetSource_FromAI(),
					new Vector2(
						Projectile.position.X + Main.rand.Next(extraSize + 1),
						Projectile.position.Y + Main.rand.Next(extraSize + 1)
					),
					default(Vector2),
					Main.rand.Next(11, 14),
					1f
				);
				Main.gore[g].velocity += Projectile.velocity * 2;
				Main.gore[g].velocity *= 0.2f;
				Main.gore[g].alpha = alpha;
			}

			Projectile.ai[0]++;

			if (
				(Math.Abs(Projectile.velocity.X) < 0.2f &&
				Math.Abs(Projectile.velocity.Y) < 0.2f) ||
				Projectile.wet
				)
			{
				Projectile.Kill();
			}
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 centerThing)
		{
			width = 8;
			height = 8;
			return fallThrough;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (Projectile.velocity.X == 0) Projectile.velocity.X = Projectile.oldVelocity.X * -0.2f;
			if (Projectile.velocity.Y == 0) Projectile.velocity.Y = Projectile.oldVelocity.Y * -0.2f;
			return false;
		}
	}
}
