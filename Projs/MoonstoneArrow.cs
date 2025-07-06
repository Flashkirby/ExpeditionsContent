using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent144.Projs
{
	class MoonstoneArrow : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			//****DisplayName.SetDefault("Yutu Arrow");
		}
		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
			Projectile.extraUpdates++;
		}
		public override void PostAI()
		{
			if (Projectile.ai[0] >= 15f)
			{
				Projectile.velocity.Y -= 0.1f * 2; //Counter gravity
			}

			Lighting.AddLight(Projectile.position, new Vector3(
				0.3f, 0.4f, 0.5f));
		}
		public override void OnHitNPC(NPC target, NPC.HitInfo info, int damageDone)
		{
			target.AddBuff(ModContent.BuffType<Buffs.MoonlightDeBuff>(), ExpeditionC144.MoonDebuffTime);
		}
		public override void OnHitPlayer(Player target, Player.HurtInfo info)
		{
			target.AddBuff(ModContent.BuffType<Buffs.MoonlightDeBuff>(), ExpeditionC144.MoonDebuffTime);
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
			return true;
		}
		public override void OnKill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
			Dust d;
			for (int i = 0; i < 10; i++)
			{
				d = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 20,
					Projectile.oldVelocity.X * 0.2f, Projectile.oldVelocity.Y * 0.2f)];
				d.velocity *= -2f;
				d.scale = 0.7f;
				d.alpha = 150;
				d.fadeIn = 1f;
				d.noGravity = true;
			}
		}
		public override bool PreDraw(ref Color lightColor)
		{
			Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
			Color colour = new Color(1f, 1f, 1f, 0.7f) * Projectile.Opacity;

			float length = System.Math.Min(5, Projectile.ai[0]);
			Vector2 position = Projectile.Center - Main.screenPosition;
			position -= Projectile.velocity * length;

			float mult = 0f;
			for (int i = 0; i < length; i++)
			{
				position += Projectile.velocity;
				mult += 0.6f / length;

				if (i == (int)length - 1) mult = 1f; // actual arrow
				Main.EntitySpriteDraw(
					texture, position, null,
					colour * mult, Projectile.rotation,
					new Vector2(texture.Width / 2, Projectile.height / 2f),
					Projectile.scale,
					Projectile.spriteDirection > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally,
					0f);
			}

			return false;
		}
	}
}
