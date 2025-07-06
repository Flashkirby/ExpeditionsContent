using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent144.Items.Wayfarer
{
	public class WayfarerPike : ModItem
	{
		public override void SetStaticDefaults()
		{
			//****DisplayName.SetDefault("Wayfarer's Pike");
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.Trident);
			Item.width = 32;
			Item.height = 36;

			Item.damage -= 1;
			Item.useAnimation -= 2;
			Item.useTime -= 2;
			Item.knockBack += 0.5f;
			Item.shoot = ModContent.ProjectileType<Projs.WayfarerPike>();
			Item.shootSpeed -= 0.4f;

			Item.value = Item.buyPrice(0, 0, 50, 0);
		}
	}
}
