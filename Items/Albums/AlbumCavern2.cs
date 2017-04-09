﻿using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumCavern2 : ModItem
    {
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                "Cavern Predators, 2nd ed.",
                "'It contains photos of ravenous beasts'",
                Item.sellPrice(0, 9, 0, 0), 4, 27
                );
        }
        public override void AddRecipes()
        {
            AlbumAnimalFirst.AddCopyRecipes(this, 3 + 3);
        }
    }
}
