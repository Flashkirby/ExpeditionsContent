using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent144.Items.Moonstone
{
	public class MoonstoneArrow : ModItem
	{
		public override void SetStaticDefaults()
		{
			/****DisplayName.SetDefault("Yutu Arrow");
            Tooltip.SetDefault("Decreases target's defense");*/
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.WoodenArrow);

			Item.shoot = ModContent.ProjectileType<Projs.MoonstoneArrow>();

			Item.damage = 9;
			Item.rare = 2;
			Item.value = Item.buyPrice(0, 0, 0, 50);
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(50);
			recipe.AddIngredient(ModContent.ItemType<Moonstone>(), 1);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}
