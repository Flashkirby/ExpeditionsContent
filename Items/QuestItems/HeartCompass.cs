using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent144.Items.QuestItems
{
	/// <summary>
	/// Powerful accessory that reveals life crystals
	/// </summary>
	public class HeartCompass : ModItem
	{
		public override void SetStaticDefaults()
		{
			/****DisplayName.SetDefault("Heart Compass");
            Tooltip.SetDefault("Reveals nearby life crystals on the world map");*/
		}
		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 30;
			Item.rare = 2;
			Item.accessory = true;
			Item.value = Item.sellPrice(0, 2, 0, 0);
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			PlayerExplorer.Get(player, Mod).accHeartCompass = true;
		}

		public override void UpdateInventory(Player player)
		{
			PlayerExplorer.Get(player, Mod).accHeartCompass = true;
		}

	}
}
