﻿using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumAnimals : ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Terrarian Critters, 2st ed.";
            item.toolTip = "Fetches a good price at shops";
            item.toolTip2 = "'It's full of cute animal pictures'";
            item.width = 22;
            item.height = 30;
            item.maxStack = 1;

            item.rare = 2;
            item.value = Item.sellPrice(0, 10, 0, 0);
        }
    }
}
