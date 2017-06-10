using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumAnimals : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terrarian Critters, 2nd ed.");
            Tooltip.SetDefault("'It contains plenty of cute animal photos'");
        }
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                Item.sellPrice(0, 6, 0, 0), 2, 1
                );
        }
        public override void AddRecipes()
        {
            AlbumAnimalFirst.AddCopyRecipes(this, 3 + 6);
        }
    }
}
