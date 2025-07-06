using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent144.Items.Wayfarer
{
	public class WayfarerRepeater : ModItem
	{
		public override void SetStaticDefaults()
		{
			//****DisplayName.SetDefault("Wayfarer's Repeater");
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.Musket);
			Item.width = 46;
			Item.height = 20;

			Item.damage -= 1;
			Item.useAnimation += 6;
			Item.useTime += 6; // **** was 5 instead of 6 before. Changed to prevent double fire
			Item.knockBack += 1.5f;
			Item.shootSpeed += 2f;

			Item.value = Item.buyPrice(0, 4, 0, 0);
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2();
		}
	}
}