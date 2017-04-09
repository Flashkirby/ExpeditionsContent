using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumCorruption : ModItem
    {
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                "Disaster Report: Corruption, 1st ed.",
                "'It contains research on the corruption's influence on wildlife'",
                Item.sellPrice(0, 6, 0, 0), 2, 32
                );
        }
        public override void AddRecipes()
        {
            AlbumAnimalFirst.AddCopyRecipes(this, 3);
        }
    }
}
