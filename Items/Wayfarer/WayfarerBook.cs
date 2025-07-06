using ExpeditionsContent144.Projs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent144.Items.Wayfarer
{
	public class WayfarerBook : ModItem
	{
		public override void SetStaticDefaults()
		{
			/****DisplayName.SetDefault("Wayfarer's Wind");
            Tooltip.SetDefault("Casts a mighty gust of wind\n"
                + "'It werfs nebels'");*/
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.WaterBolt);
			Item.UseSound = SoundID.Item34;

			Item.mana = 12;
			Item.damage = 6;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.knockBack = 7f;
			Item.shoot = ModContent.ProjectileType<Gust>();
			Item.shootSpeed = 7f;

			Item.value = Item.buyPrice(0, 3, 0, 0);
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2();
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, position, new Vector2(
				velocity.X + 2f * (Main.rand.NextFloat() - 0.5f),
				velocity.Y + 2f * (Main.rand.NextFloat() - 0.5f)
				), type, damage, knockback, player.whoAmI);
			Projectile.NewProjectile(source, position, new Vector2(
				velocity.X + 4f * (Main.rand.NextFloat() - 0.5f),
				velocity.Y + 4f * (Main.rand.NextFloat() - 0.5f)
				), type, damage, knockback, player.whoAmI);
			return true;
		}
	}
}
