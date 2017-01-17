using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items
{
    public class JungleEyepiece : ModItem
    {
        /// <summary>
        /// Powerful accessory that reveals life fruit
        /// </summary>
        public override void SetDefaults()
        {
            item.name = "Enchanted Lens";
            item.toolTip = "Reveals nearby life fruit on the Full Map";
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
