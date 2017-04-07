using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.QuestItems
{
    public class PhotoBlank : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Photo Film";
            item.toolTip = "'Contains enchanted paper, capable of preserving images'";
            item.width = 30;
            item.height = 36;
            item.ammo = item.type;
            item.maxStack = 99;

            item.rare = 0;
            item.value = Item.buyPrice(0, 0, 15, 0);
        }
    }
}
