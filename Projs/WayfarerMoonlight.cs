using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent.Projs
{
    class WayfarerMoonlight : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "Moonlight Orb";
            projectile.width = 40;
            projectile.height = 40;
            projectile.timeLeft = Projectile.SentryLifeTime;
            projectile.ignoreWater = true;
            projectile.sentry = true;
            projectile.alpha = 0;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
        }
        public override bool OnTileCollide(Vector2 oldVelocity) { return false; }

        public const float effectiveDistance = 500f;
        public override void AI()
        {
            projectile.velocity = Vector2.Zero;

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
                d = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 
                    20, 0, 0, 150, default(Color), 0.1f)];
                d.fadeIn = 1.5f;
                d.velocity *= 0.5f;
            }

            // Create lighting
            float dx, dy;
            dx = projectile.Center.X + effectiveDistance * Main.rand.NextFloatDirection();
            dy = projectile.Center.Y + effectiveDistance * Main.rand.NextFloatDirection();
            if (InRange(new Rectangle((int)dx, (int)dy, 4, 4)))
            {
                d = Main.dust[Dust.NewDust(new Vector2(dx, dy) - Vector2.One * 2f, 0, 0, 20, 0, 0, 150, default(Color), 0.2f)];
                d.fadeIn = 2f;
                d.velocity *= 0.5f;
            }

            // Pulsate
            projectile.alpha = (int)(50f + 50f * Math.Sin(projectile.timeLeft * 0.1f));
        }

        private void AI_ApplyBuffs()
        {
            if (projectile.ai[0] >= 30)
            {
                projectile.ai[0] = 0;
            }
            else { projectile.ai[0]++; return; }

            int buff = mod.BuffType<Buffs.MoonlightBuff>();
            int debuff = mod.BuffType<Buffs.MoonlightDeBuff>();
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

            Player pown = Main.player[projectile.owner];
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
            if (projectile.localAI[0] == 0f)
            {
                projectile.localAI[0] = 1f;
                Main.PlaySound(SoundID.Item82, projectile.position);

                Player player = Main.player[projectile.owner];
                player.UpdateMaxTurrets();

                for (int i = 0; i < 90; i++)
                {
                    Dust d = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height,
                        15, 0, 0, 100, default(Color), 1.5f)];
                    d.velocity *= i / 30f;
                }
            }
        }

        private bool InRange(Rectangle rect)
        {
            if ((rect.Center.ToVector2() - projectile.Center).Length() <= effectiveDistance)
            {
                // Expensive Method
                if (Collision.CanHit(
                    projectile.position, projectile.width, projectile.height, 
                    rect.Location.ToVector2(), rect.Width, rect.Height))
                {
                    return true;
                }
            }
            return false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            Color colour = lightColor = new Color(1f, 1f, 1f, 0.65f * projectile.Opacity);

            // Pulsate
            spriteBatch.Draw(texture, projectile.Center - Main.screenPosition,
                null, colour * 0.5f, projectile.rotation,
                new Vector2(texture.Width / 2, texture.Height / 2),
                projectile.scale * (float)(1.1f + 0.05f * Math.Sin(projectile.timeLeft * 0.15f)) , SpriteEffects.None, 0f);

            // Main
            spriteBatch.Draw(texture, projectile.Center - Main.screenPosition,
                null, colour, projectile.rotation,
                new Vector2(texture.Width / 2, texture.Height / 2),
                projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}
