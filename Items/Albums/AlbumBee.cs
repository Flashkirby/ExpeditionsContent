using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumBee : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Albeem, 1st ed.";
            item.toolTip = "Fetches a good price at shops";
            item.toolTip2 = "'Buzzing with bee tales'";
            item.width = 22;
            item.height = 30;
            item.maxStack = 1;

            item.rare = 1;
            item.value = Item.sellPrice(0, 6, 0, 0);
        }
    }
}
