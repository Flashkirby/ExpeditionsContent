using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class CopyPrint : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Photo Binder";
            item.toolTip = "Used for copying albums at a Dye Vat";
            item.width = 22;
            item.height = 30;
            item.maxStack = 99;

            item.rare = 1;
            item.value = Item.buyPrice(0, 2, 0, 0);
        }
    }
}
