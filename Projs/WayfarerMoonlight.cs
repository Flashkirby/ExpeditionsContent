using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent144.Projs
{
	class WayfarerMoonlight : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			//****DisplayName.SetDefault("Moonlight Orb");
		}
		public override void SetDefaults()
		{
			Projectile.width = 40;
			Projectile.height = 40;
			Projectile.timeLeft = Projectile.SentryLifeTime;
			Projectile.ignoreWater = true;
			Projectile.sentry = true;
			Projectile.alpha = 0;
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
		}
		public override bool OnTileCollide(Vector2 oldVelocity) { return false; }

		public const float effectiveDistance = 500f;
		public override void AI()
		{
			Projectile.velocity = Vector2.Zero;

			AI_Summon();

			AI_ApplyBuffs();

			AI_VisualFX();
		}

		private void AI_VisualFX()
		{
			// Light up self
			Dust d;
			if (Main.time % 10 == 0)
			{
				d = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height,
					20, 0, 0, 150, default(Color), 0.1f)];
				d.fadeIn = 1.5f;
				d.velocity *= 0.5f;
			}

			// Create lighting
			float dx, dy;
			dx = Projectile.Center.X + effectiveDistance * Main.rand.NextFloatDirection();
			dy = Projectile.Center.Y + effectiveDistance * Main.rand.NextFloatDirection();
			if (InRange(new Rectangle((int)dx, (int)dy, 4, 4)))
			{
				d = Main.dust[Dust.NewDust(new Vector2(dx, dy) - Vector2.One * 2f, 0, 0, 20, 0, 0, 150, default(Color), 0.2f)];
				d.fadeIn = 2f;
				d.velocity *= 0.5f;
			}

			// Pulsate
			Projectile.alpha = (int)(50f + 50f * Math.Sin(Projectile.timeLeft * 0.1f));
		}

		private void AI_ApplyBuffs()
		{
			if (Projectile.ai[0] >= 30)
			{
				Projectile.ai[0] = 0;
			}
			else { Projectile.ai[0]++; return; }

			int buff = ModContent.BuffType<Buffs.MoonlightBuff>();
			int debuff = ModContent.BuffType<Buffs.MoonlightDeBuff>();
			for (int i = 0; i < 200; i++)
			{
				NPC npc = Main.npc[i];
				if (!npc.active) continue;
				if (npc.life <= 0) continue;
				if (!InRange(npc.getRect())) continue;
				if (npc.CanBeChasedBy(this, false))
				{
					npc.AddBuff(debuff, 60);
				}
				else if (npc.friendly && npc.townNPC)
				{
					npc.AddBuff(buff, 60);
				}
			}

			Player pown = Main.player[Projectile.owner];
			foreach (Player player in Main.player)
			{
				if (!player.active) continue;
				if (player.dead) continue;
				if (!InRange(player.getRect())) continue;
				// PVP and enemy teams or no team
				if ((player.team == 0 || pown.team != player.team)
					&& pown.hostile && player.hostile
					&& pown.whoAmI != player.whoAmI)
				{
					player.AddBuff(debuff, 60);
				}
				else
				{
					player.AddBuff(buff, 60);
				}
			}
		}

		private void AI_Summon()
		{
			if (Projectile.localAI[0] == 0f)
			{
				Projectile.localAI[0] = 1f;
				SoundEngine.PlaySound(SoundID.Item82, Projectile.position);

				Player player = Main.player[Projectile.owner];
				player.UpdateMaxTurrets();

				for (int i = 0; i < 90; i++)
				{
					Dust d = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height,
						15, 0, 0, 100, default(Color), 1.5f)];
					d.velocity *= i / 30f;
				}
			}
		}

		private bool InRange(Rectangle rect)
		{
			if ((rect.Center.ToVector2() - Projectile.Center).Length() <= effectiveDistance)
			{
				// Expensive Method
				if (Collision.CanHit(
					Projectile.position, Projectile.width, Projectile.height,
					rect.Location.ToVector2(), rect.Width, rect.Height))
				{
					return true;
				}
			}
			return false;
		}

		public override bool PreDraw(ref Color lightColor)
		{
			Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
			Color colour = lightColor = new Color(1f, 1f, 1f, 0.65f * Projectile.Opacity);

			// Pulsate
			Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition,
				null, colour * 0.5f, Projectile.rotation,
				new Vector2(texture.Width / 2, texture.Height / 2),
				Projectile.scale * (float)(1.1f + 0.05f * Math.Sin(Projectile.timeLeft * 0.15f)), SpriteEffects.None, 0f);

			// Main
			Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition,
				null, colour, Projectile.rotation,
				new Vector2(texture.Width / 2, texture.Height / 2),
				Projectile.scale, SpriteEffects.None, 0f);
			return false;
		}

	}
}
