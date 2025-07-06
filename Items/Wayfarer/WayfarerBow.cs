using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent144.Items.Wayfarer
{
	public class WayfarerBow : ModItem
	{
		public override void SetStaticDefaults()
		{
			//****DisplayName.SetDefault("Wayfarer's Bow");
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.GoldBow);

			Item.knockBack += 2f;
			Item.shootSpeed += 3.5f;

			Item.rare = 1;
			Item.value = Item.buyPrice(0, 0, 50, 0);
		}
	}
}