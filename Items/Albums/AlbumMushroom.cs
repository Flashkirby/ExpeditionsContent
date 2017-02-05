using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumMushroom : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Dangers of Spore Infestation, 1st ed.";
            item.toolTip = "Fetches a good price at shops";
            item.toolTip2 = "'It contains strange images of msuhroom-like foes'";
            item.width = 22;
            item.height = 30;
            item.maxStack = 1;

            item.rare = 2;
            item.value = Item.sellPrice(0, 10, 0, 0);
        }
    }
}
