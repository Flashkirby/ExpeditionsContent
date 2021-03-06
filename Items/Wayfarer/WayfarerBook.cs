﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Wayfarer
{
    public class WayfarerBook : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wayfarer's Wind");
            Tooltip.SetDefault("Casts a mighty gust of wind\n"
                + "'It werfs nebels'");
        }
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.WaterBolt);
            item.UseSound = SoundID.Item34;

            item.mana = 12;
            item.damage = 6;
            item.useAnimation = 45;
            item.useTime = 45;
            item.knockBack = 7f;
            item.shoot = mod.ProjectileType("Gust");
            item.shootSpeed = 7f;

            item.value = Item.buyPrice(0, 3, 0, 0);
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position, new Vector2(
                speedX + 2f * (Main.rand.NextFloat() - 0.5f),
                speedY + 2f * (Main.rand.NextFloat() - 0.5f)
                ), type, damage, knockBack, player.whoAmI);
            Projectile.NewProjectile(position, new Vector2(
                speedX + 4f * (Main.rand.NextFloat() - 0.5f),
                speedY + 4f * (Main.rand.NextFloat() - 0.5f)
                ), type, damage, knockBack, player.whoAmI);
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
    }
}
