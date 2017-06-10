using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.QuestItems
{
    /// <summary>
    /// Powerful accessory that reveals life fruit
    /// </summary>
    public class JungleEyepiece : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Enchanted Lens");
            Tooltip.SetDefault("Reveals nearby life fruit on the world map");
        }
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 30;
            item.rare = 7;
            item.accessory = true;
            item.value = Item.sellPrice(0, 5, 0, 0);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            PlayerExplorer.Get(player, mod).accFruitCompass = true;
        }

        public override void UpdateInventory(Player player)
        {
            PlayerExplorer.Get(player, mod).accFruitCompass = true;
        }

    }
}
