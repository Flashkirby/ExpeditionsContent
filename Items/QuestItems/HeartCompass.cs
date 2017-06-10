using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.QuestItems
{
    /// <summary>
    /// Powerful accessory that reveals life crystals
    /// </summary>
    public class HeartCompass : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heart Compass");
            Tooltip.SetDefault("Reveals nearby life crystals on the world map");
        }
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 30;
            item.rare = 2;
            item.accessory = true;
            item.value = Item.sellPrice(0, 2, 0, 0);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            PlayerExplorer.Get(player, mod).accHeartCompass = true;
        }

        public override void UpdateInventory(Player player)
        {
            PlayerExplorer.Get(player, mod).accHeartCompass = true;
        }

    }
}
