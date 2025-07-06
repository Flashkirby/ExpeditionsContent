using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent144.Items.QuestItems
{
	[AutoloadEquip(EquipType.Neck)]
	public class HeartLocket : ModItem
	{
		public override void SetStaticDefaults()
		{
			/****DisplayName.SetDefault("Heart Locket");
            Tooltip.SetDefault("Temporarily increases maximum life by 20\n"
                + "'Practical yet stylish'");*/
		}
		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 22;
			Item.accessory = true;
			Item.rare = 1;
			Item.value = Item.buyPrice(0, 1, 0, 0);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.statLifeMax2 += 20;
		}
	}
}
