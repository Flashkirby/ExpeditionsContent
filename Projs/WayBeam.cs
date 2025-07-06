using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent144.Projs
{
	class WayBeam : ModProjectile
	{
		public const float length = 30f;
		public override void SetStaticDefaults()
		{
			//****DisplayName.SetDefault("Purple Beam");
		}
		public override void SetDefaults()
		{
			Projectile.width = 12;
			Projectile.height = 12;
			Projectile.penetrate = 10;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.friendly = true;

			Projectile.timeLeft = 600;
			Projectile.extraUpdates = 2;
		}

		public override void AI()
		{
			AI_ManageLaserFX();

			if (Projectile.ai[0] <= 0)
			{
				Lighting.AddLight(Projectile.position, new Vector3(0.16f, 0.05f, 0.2f));
			}
		}

		private void AI_ManageLaserFX()
		{
			// Count up to reduce laser length once collision occurs
			if (Projectile.ai[0] > 0)
			{
				Projectile.ai[0]++;
				float lightDivider = 3f + Projectile.ai[0];
				Lighting.AddLight(Projectile.position, new Vector3(
					12 * 0.16f / lightDivider,
					12 * 0.05f / lightDivider,
					12 * 0.2f / lightDivider));
			}
			else
			{
				Projectile.ai[1]++;
				if (Projectile.ai[1] < 80)
				{
					Projectile.velocity *= 1.01f;
				}

				Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;

				Projectile.localAI[0] = Projectile.velocity.X;
				Projectile.localAI[1] = Projectile.velocity.Y;
			}

			if (Projectile.ai[0] > length)
			{
				Projectile.timeLeft = 0;
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.ai[0]++;
			Projectile.velocity = new Vector2();
			return false;
		}
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
		{
			width = 4;
			height = 4;
			return fallThrough;
		}

		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers hit)
		{
			// Always crit on hitting on the ground (concentrated beam)
			if (Projectile.ai[0] > 0) hit.SetCrit();
		}

		public override bool PreDraw(ref Color lightColor)
		{
			Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
			Color colour = new Color(1f, 1f, 1f, 0.3f) * Projectile.Opacity;

			int max = (int)(Math.Min(Projectile.ai[1], length) - Projectile.ai[0]);

			Vector2 savedVel = new Vector2(Projectile.localAI[0], Projectile.localAI[1]);
			Vector2 position = Projectile.Center - Main.screenPosition;

			for (int i = max - 1; i >= 0; i--)
			{
				Main.EntitySpriteDraw(
					texture, position, null,
					colour * (i / length), Projectile.rotation,
					new Vector2(texture.Width, texture.Height) / 2f,
					Projectile.scale, SpriteEffects.None, 0f);
				position -= savedVel;
			}

			return false;
		}
	}
}
