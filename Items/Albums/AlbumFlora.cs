using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumFlora : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "How to Spot a Man-eating Plant, 1st ed.";
            item.toolTip = "Fetches a good price at shops";
            item.toolTip2 = "'It contains pictures and notes on dangerous plants'";
            item.width = 22;
            item.height = 30;
            item.maxStack = 1;

            item.rare = 2;
            item.value = Item.sellPrice(0, 6, 0, 0);
        }
    }
}
