using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent144.Items.Wayfarer
{
	public class WayfarerSword : ModItem
	{
		public override void SetStaticDefaults()
		{
			//****DisplayName.SetDefault("Wayfarer's Sword");
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.GoldBroadsword);
			Item.width = 32;
			Item.height = 36;
			Item.scale = 1.1f;

			Item.damage += 4;
			Item.useAnimation += 2;
			Item.knockBack = 6f;
			Item.autoReuse = true;

			Item.rare = 1; // So you don't lose it in lava
			Item.value = Item.buyPrice(0, 0, 50, 0);
		}
	}
}
