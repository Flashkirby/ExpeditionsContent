using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumAntlion : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Antlion Studies, 1st ed.";
            item.toolTip = "Fetches a good price at shops";
            item.toolTip2 = "'Full of pictures and diagrams about Antlions'";
            item.width = 22;
            item.height = 30;
            item.maxStack = 1;

            item.rare = 2;
            item.value = Item.sellPrice(0, 6, 0, 0);
        }
    }
}
