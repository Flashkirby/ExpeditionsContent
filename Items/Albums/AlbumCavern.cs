using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumCavern : ModItem
    {
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                "Cavern Predators, 1st ed.",
                "'It contains photos of cavernous beasts'",
                Item.sellPrice(0, 3, 0, 0), 1, 26
                );
        }
        public override void AddRecipes()
        {
            AlbumAnimalFirst.AddCopyRecipes(this, 3);
        }
    }
}
