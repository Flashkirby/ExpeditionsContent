﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Wayfarer
{
    public class WayfarerSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wayfarer's Sword");
        }
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.GoldBroadsword);
            item.width = 32;
            item.height = 36;
            item.scale = 1.1f;

            item.damage += 4;
            item.useAnimation += 2;
            item.knockBack = 6f;
            item.autoReuse = true;

            item.rare = 1; // So you don't lose it in lava
            item.value = Item.buyPrice(0, 0, 50, 0);
        }
    }
}
