﻿using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumBee : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Albeem, 1st ed.";
            item.toolTip = "Fetches a good price at shops";
            item.toolTip2 = "'Buzzing with beetails'";
            item.width = 22;
            item.height = 30;
            item.maxStack = 1;

            item.rare = 2;
            item.value = Item.sellPrice(0, 10, 0, 0);
        }
    }
}