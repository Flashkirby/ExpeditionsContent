using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent144.Projs
{
	class VacuumOrb : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			//****DisplayName.SetDefault("Vacuum Orb");
		}
		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.WaterBolt);
			Projectile.aiStyle = 0;
			Projectile.alpha = 0;
			Projectile.penetrate = 256;

			Projectile.alpha = 100;
			Projectile.timeLeft = 600;
		}

		public const int explosionTimeLeft = 3;
		public const float explosionDelay = 20f;
		public override void AI()
		{
			int d;
			if (Projectile.ai[1] == 0f)
			{
				d = Dust.NewDust(Projectile.Center - new Vector2(4, 4), 4, 4, 173,
					Projectile.velocity.X * 10f, Projectile.velocity.Y * 10f, 0, default(Color), 2f);
				Main.dust[d].velocity *= 0.08f;
				Main.dust[d].noGravity = true;

				d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 71,
					Projectile.velocity.X, Projectile.velocity.Y);
				Main.dust[d].velocity *= 0.25f;
			}
			else
			{
				d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 71,
					0, 0, 50, default(Color), 0.5f);
				Main.dust[d].velocity *= 0.1f;
				if (Projectile.scale > 0) Projectile.scale *= 0.9f;
			}

			if (Projectile.penetrate != Projectile.maxPenetrate && Projectile.penetrate > 0)
			{
				Projectile.ai[1] = explosionDelay + explosionTimeLeft;
				Projectile.localAI[0] = Projectile.damage;
				Projectile.timeLeft = (int)explosionDelay + explosionTimeLeft;

				Projectile.penetrate = -1;
				Projectile.damage = 0;
				Projectile.tileCollide = false;
				Projectile.velocity = Projectile.velocity * 0.25f;

				SoundEngine.PlaySound(SoundID.Item24, Projectile.Center);
			}

			if (Projectile.timeLeft <= explosionTimeLeft)
			{
				if (Projectile.timeLeft == explosionTimeLeft) SoundEngine.PlaySound(SoundID.Item27, Projectile.Center);
				Projectile.damage = (int)Projectile.localAI[0];

				Projectile.hide = true;

				Projectile.Center = Projectile.BottomRight;
				Projectile.width = 64;
				Projectile.height = 64;
				Projectile.Center = Projectile.TopLeft;

				for (int i = 0; i < 5; i++)
				{
					d = Dust.NewDust(Projectile.Center - new Vector2(4, 4), 0, 0, 72 + Main.rand.Next(2),
						Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f);
					Main.dust[d].velocity *= 2f;
					Main.dust[d].noGravity = true;

					d = Dust.NewDust(Projectile.Center - new Vector2(4, 4), 0, 0, 173,
						Projectile.velocity.X * -1f, Projectile.velocity.Y * -1f, 0, default(Color), 2f);
					Main.dust[d].velocity *= 3f;
				}
			}

			Projectile.rotation += Projectile.direction;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.penetrate = 1;
			Projectile.velocity = Vector2.Zero;
			return false;
		}

		public override bool PreDraw(ref Color lightColor)
		{
			Texture2D t = TextureAssets.Projectile[Projectile.type].Value;
			Vector2 p = Projectile.position - Main.screenPosition;
			Vector2 c = new Vector2(TextureAssets.Projectile[Projectile.type].Width() / 2, TextureAssets.Projectile[Projectile.type].Height() / 2);
			Main.EntitySpriteDraw(t,
				p + c,
				null, new Color(255, 255, 255, Projectile.alpha),
				Projectile.rotation,
				c,
				Projectile.scale,
				SpriteEffects.None,
				0f);
			return false;
		}
	}
}
