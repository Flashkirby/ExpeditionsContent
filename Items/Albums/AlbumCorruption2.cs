using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumCorruption2 : ModItem
    {
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                "Disaster Report: Corruption, 2nd ed.",
                "'It traces the detrimental effects of releasing ancient spirits'",
                Item.sellPrice(0, 6, 0, 0), 2, 33
                );
        }
        public override void AddRecipes()
        {
            AlbumAnimalFirst.AddCopyRecipes(this, 3);
        }
    }
}
