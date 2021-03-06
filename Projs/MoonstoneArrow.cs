﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent.Projs
{
    class MoonstoneArrow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Yutu Arrow");
        }
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.WoodenArrowFriendly);
            projectile.extraUpdates++;
        }
        public override void PostAI()
        {
            if (projectile.ai[0] >= 15f)
            {
                projectile.velocity.Y -= 0.1f * 2; //Counter gravity
            }

            Lighting.AddLight(projectile.position, new Vector3(
                0.3f, 0.4f, 0.5f));
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType<Buffs.MoonlightDeBuff>(), ExpeditionC.MoonDebuffTime);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType<Buffs.MoonlightDeBuff>(), ExpeditionC.MoonDebuffTime);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType<Buffs.MoonlightDeBuff>(), ExpeditionC.MoonDebuffTime);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);
            return true;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(2, projectile.position, 27);
            Dust d;
            for(int i = 0; i < 10; i++)
            {
                d = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 20,
                    projectile.oldVelocity.X * 0.2f, projectile.oldVelocity.Y * 0.2f)];
                d.velocity *= -2f;
                d.scale = 0.7f;
                d.alpha = 150;
                d.fadeIn = 1f;
                d.noGravity = true;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            Color colour = new Color(1f, 1f, 1f, 0.7f) * projectile.Opacity;

            float length = System.Math.Min(5, projectile.ai[0]);
            Vector2 position = projectile.Center - Main.screenPosition;
            position -= projectile.velocity * length;

            float mult = 0f;
            for (int i = 0; i < length; i++)
            {
                position += projectile.velocity;
                mult += 0.6f / length;

                if (i == (int)length - 1) mult = 1f; // actual arrow
                spriteBatch.Draw(
                    texture, position, null,
                    colour * mult, projectile.rotation,
                    new Vector2(texture.Width / 2, projectile.height / 2f),
                    projectile.scale,
                    projectile.spriteDirection > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally,
                    0f);
            }

            return false;
        }
    }
}
