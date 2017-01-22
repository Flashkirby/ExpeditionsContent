using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumAnimals : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Album of Animals, 1st ed.";
            item.toolTip = "Fetches a good price at shops";
            item.toolTip2 = "'Contains photos of various animals'";
            item.width = 22;
            item.height = 30;
            item.maxStack = 1;

            item.rare = 1;
            item.value = Item.sellPrice(0, 5, 0, 0);
        }
    }
}
