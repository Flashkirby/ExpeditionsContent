using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent.Projs
{
    class WayBeam : ModProjectile
    {
        public const float length = 30f;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Purple Beam");
        }
        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 12;
            projectile.penetrate = 10;
            projectile.magic = true;
            projectile.friendly = true;
            
            projectile.timeLeft = 600;
            projectile.extraUpdates = 2;
        }

        public override void AI()
        {
            AI_ManageLaserFX();

            if (projectile.ai[0] <= 0)
            {
                Lighting.AddLight(projectile.position, new Vector3(0.16f, 0.05f, 0.2f));
            }
        }

        private void AI_ManageLaserFX()
        {
            // Count up to reduce laser length once collision occurs
            if (projectile.ai[0] > 0)
            {
                projectile.ai[0]++;
                float lightDivider = 3f + projectile.ai[0];
                Lighting.AddLight(projectile.position, new Vector3(
                    12 * 0.16f / lightDivider,
                    12 * 0.05f / lightDivider,
                    12 * 0.2f / lightDivider));
            }
            else
            {
                projectile.ai[1]++;
                if(projectile.ai[1] < 80)
                {
                    projectile.velocity *= 1.01f;
                }

                projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

                projectile.localAI[0] = projectile.velocity.X;
                projectile.localAI[1] = projectile.velocity.Y;
            }

            if (projectile.ai[0] > length)
            {
                projectile.timeLeft = 0;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.ai[0]++;
            projectile.velocity = new Vector2();
            return false;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 4;
            height = 4;
            return fallThrough;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            // Always crit on hitting on the ground (concentrated beam)
            if (projectile.ai[0] > 0) crit = true;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            Color colour = new Color(1f, 1f, 1f, 0.3f) * projectile.Opacity;

            int max = (int)(Math.Min(projectile.ai[1], length) - projectile.ai[0]);

            Vector2 savedVel = new Vector2(projectile.localAI[0], projectile.localAI[1]);
            Vector2 position = projectile.Center - Main.screenPosition;

            for (int i = max - 1; i >= 0; i--)
            {
                spriteBatch.Draw(
                    texture, position, null,
                    colour * (i / length), projectile.rotation,
                    new Vector2(texture.Width, texture.Height) / 2f,
                    projectile.scale, SpriteEffects.None, 0f);
                position -= savedVel;
            }

            return false;
        }
    }
}
