using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumAnimalFirst : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Terrarian Critters, 1st ed.";
            item.toolTip = "Fetches a good price at shops";
            item.toolTip2 = "'It's full of cute animal photos!'";
            item.width = 22;
            item.height = 30;
            item.maxStack = 1;

            item.rare = 1;
            item.value = Item.sellPrice(0, 3, 0, 0);
        }
    }
}
