using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumUndead2 : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Risen from the Grave, 1st ed.";
            item.toolTip = "Fetches a good price at shops";
            item.toolTip2 = "'It contains theories and studies about the undead'";
            item.width = 22;
            item.height = 30;
            item.maxStack = 1;

            item.rare = 2;
            item.value = Item.sellPrice(0, 6, 0, 0);
        }
    }
}
