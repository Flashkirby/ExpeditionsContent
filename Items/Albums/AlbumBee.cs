using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumBee : ModItem
    {
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                "Albeem, 1st ed.",
                "'Buzzing with bee tales'",
                Item.sellPrice(0, 6, 0, 0), 1, 19
                );
        }
        public override void AddRecipes()
        {
            AlbumAnimalFirst.AddCopyRecipes(this, 8);
        }
    }
}
