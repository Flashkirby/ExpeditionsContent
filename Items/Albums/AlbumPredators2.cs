using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumPredators2 : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Monster Almanac, 2nd ed.";
            item.toolTip = "Fetches a great price at shops";
            item.toolTip2 = "'It contains all kinds of monster information'";
            item.width = 22;
            item.height = 30;
            item.maxStack = 1;

            item.rare = 3;
            item.value = Item.sellPrice(0, 30, 0, 0);
        }
    }
}
