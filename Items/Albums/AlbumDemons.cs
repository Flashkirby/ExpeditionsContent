﻿using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumDemons : ModItem
    {
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                "Spirits and Demons, 1st ed.",
                "'It contains worrying signs of dark forces'",
                Item.sellPrice(0, 3, 0, 0), 1, 20
                );
        }
        public override void AddRecipes()
        {
            AlbumAnimalFirst.AddCopyRecipes(this, 6);
        }
    }
}
