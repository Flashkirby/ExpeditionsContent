using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumCrimson : ModItem
    {
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                "Disaster Report: Crimson, 1st ed.",
                "'It contains gruesome pictures of horrifying wildlife'",
                Item.sellPrice(0, 6, 0, 0), 2, 34
                );
        }
        public override void AddRecipes()
        {
            AlbumAnimalFirst.AddCopyRecipes(this, 5);
        }
    }
}
