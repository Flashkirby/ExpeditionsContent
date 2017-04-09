using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumFlora : ModItem
    {
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                "Man-Eating Plants in the Wild, 1st ed.",
                "'It contains pictures and notes on dangerous plants'",
                Item.sellPrice(0, 6, 0, 0), 2, 18
                );
        }
        public override void AddRecipes()
        {
            AlbumAnimalFirst.AddCopyRecipes(this, 2);
        }
    }
}
