using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumDemons : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Spirits and Demons, 1st ed.";
            item.toolTip = "Fetches a good price at shops";
            item.toolTip2 = "'It contains worrying photos of dark forces'";
            item.width = 22;
            item.height = 30;
            item.maxStack = 1;

            item.rare = 1;
            item.value = Item.sellPrice(0, 3, 0, 0);
        }
    }
}
