using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.QuestItems
{
    public class ShrineMap : ModItem
    {
        /// <summary>
        /// Powerful accessory that reveals life fruit
        /// </summary>
        public override void SetDefaults()
        {
            item.name = "Enchanted Shrine Map";
            item.toolTip = "Marks sword shrines on the world map";
            item.toolTip2 = "'Are the shrines enchanted, or the map itself?'";
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
