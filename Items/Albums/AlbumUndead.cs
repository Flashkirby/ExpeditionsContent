using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumUndead : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Risen from the Grave, 1st ed.";
            item.toolTip = "Fetches a good price at shops";
            item.toolTip2 = "'It contains photos of zombies with rotten fashion sense'";
            item.width = 22;
            item.height = 30;
            item.maxStack = 1;

            item.rare = 1;
            item.value = Item.sellPrice(0, 3, 0, 0);
        }
    }
}
