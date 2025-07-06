using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent144.Items.Wayfarer
{
	public class WayfarerClaymore : ModItem
	{
		public override void SetStaticDefaults()
		{
			/****DisplayName.SetDefault("Wayfarer's Claymore");
            Tooltip.SetDefault("");*/
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.BladeofGrass);
			Item.width = 32;
			Item.height = 36;
			Item.scale = 1.15f;

			Item.damage -= 3;
			Item.useAnimation -= 8;
			Item.knockBack = 7f;
			Item.autoReuse = true;

			Item.rare = 2;
			Item.value = Item.buyPrice(0, 0, 60, 0);
		}
	}
}
