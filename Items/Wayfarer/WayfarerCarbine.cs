using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent144.Items.Wayfarer
{
	public class WayfarerCarbine : ModItem
	{
		public override void SetStaticDefaults()
		{
			//****DisplayName.SetDefault("Wayfarer's Carbine");
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.TheUndertaker);
			Item.width = 36;
			Item.height = 18;

			Item.damage += 1;
			Item.useAnimation += 2;
			Item.useTime += 2;
			Item.useTime = Item.useAnimation; // **** for some reason, these are not equal normally
			Item.knockBack += 1f;
			Item.shootSpeed += 1.5f;

			Item.value = Item.buyPrice(0, 3, 0, 0);
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2();
		}
	}
}