using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.QuestItems
{
    /// <summary>
    /// Powerful accessory that reveals life fruit
    /// </summary>
    public class ShrineMap : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Enchanted Shrine Map");
            Tooltip.SetDefault("Marks sword shrines on the world map\n"
                + "'Which is enchanted, the shrine or the map?'");
        }
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 30;
            item.rare = 2;
            item.accessory = true;
            item.value = Item.sellPrice(0, 25, 0, 0);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            PlayerExplorer.Get(player, mod).accShrineMap = true;
        }

        public override void UpdateInventory(Player player)
        {
            PlayerExplorer.Get(player, mod).accShrineMap = true;
        }

    }
}
