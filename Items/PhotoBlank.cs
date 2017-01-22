using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items
{
    public class PhotoBlank : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Photo Film";
            item.toolTip = "'Delicate enchanted paper, capable of forming images'";
            item.width = 28;
            item.height = 28;
            item.ammo = item.type;
            item.maxStack = 99;

            item.rare = 0;
            item.value = Item.buyPrice(0, 0, 15, 0);
        }
    }
}
