using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumAnimals3 : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Terrarian Critters, 3rd ed.";
            item.toolTip = "Fetches a good price at shops";
            item.toolTip2 = "'It's chock full of cute animal photos!'";
            item.width = 22;
            item.height = 30;
            item.maxStack = 1;

            item.rare = 2;
            item.value = Item.sellPrice(0, 8, 0, 0);
        }
    }
}
