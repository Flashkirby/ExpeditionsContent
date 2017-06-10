using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.QuestItems
{
    public class PhotoBlank : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Photo Film");
            Tooltip.SetDefault("'Contains enchanted paper, capable of preserving images'");
        }
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 36;
            item.ammo = item.type;
            item.maxStack = 99;

            item.rare = 0;
            item.value = Item.buyPrice(0, 0, 15, 0);
        }
    }
}
