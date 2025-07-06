using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent144.Items.QuestItems
{
	public class PhotoBlank : ModItem
	{
		public override void SetStaticDefaults()
		{
			/****DisplayName.SetDefault("Photo Film");
            Tooltip.SetDefault("'Contains enchanted paper, capable of preserving images'");*/
		}
		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 36;
			Item.ammo = Type;
			Item.maxStack = 99;

			Item.rare = 0;
			Item.value = Item.buyPrice(0, 0, 15, 0);
		}
	}
}
