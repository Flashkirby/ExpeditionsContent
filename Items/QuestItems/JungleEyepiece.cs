using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent144.Items.QuestItems
{
	/// <summary>
	/// Powerful accessory that reveals life fruit
	/// </summary>
	public class JungleEyepiece : ModItem
	{
		public override void SetStaticDefaults()
		{
			/****DisplayName.SetDefault("Enchanted Lens");
            Tooltip.SetDefault("Reveals nearby life fruit on the world map");*/
		}
		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 30;
			Item.rare = 7;
			Item.accessory = true;
			Item.value = Item.sellPrice(0, 5, 0, 0);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			PlayerExplorer.Get(player, Mod).accFruitCompass = true;
		}

		public override void UpdateInventory(Player player)
		{
			PlayerExplorer.Get(player, Mod).accFruitCompass = true;
		}

	}
}
