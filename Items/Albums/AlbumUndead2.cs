using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumUndead2 : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "No Bones About It, 1st ed.";
            item.toolTip = "Fetches a good price at shops";
            item.toolTip2 = "'The bone-rattling sequel to Risen from the Grave'";
            item.width = 22;
            item.height = 30;
            item.maxStack = 1;

            item.rare = 2;
            item.value = Item.sellPrice(0, 6, 0, 0);
        }
    }
}
