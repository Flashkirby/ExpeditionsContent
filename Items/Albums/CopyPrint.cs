using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent144.Items.Albums
{
	public class CopyPrint : ModItem
	{
		public override void SetStaticDefaults()
		{
			/****DisplayName.SetDefault("Photo Binder");
            Tooltip.SetDefault("Used for copying albums at a Dye Vat");*/
		}
		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 30;
			Item.maxStack = 99;

			Item.rare = 1;
			Item.value = Item.buyPrice(0, 2, 0, 0);
		}
	}
}
