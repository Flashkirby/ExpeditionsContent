using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumWater : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Aquatic Wildlife, 1st ed.";
            item.toolTip = "Fetches a good price at shops";
            item.toolTip2 = "'It contains photos of dangerous aquatic creatures'";
            item.width = 22;
            item.height = 30;
            item.maxStack = 1;

            item.rare = 1;
            item.value = Item.sellPrice(0, 5, 0, 0);
        }
    }
}
