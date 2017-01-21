using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items
{
    public class BrassCoin : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Vintage Coin";
            item.toolTip = "'An old coin made of brass, it has no value'";
            item.maxStack = 100;
            item.width = 16;
            item.height = 18;
            item.rare = -1;
        }
    }
}
