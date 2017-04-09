using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumSlimes2 : ModItem
    {
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                "Know Your Slimes, 2nd ed.",
                "'Full of random slime trivia!'",
                Item.sellPrice(0, 6, 0, 0), 2, 5
                );
        }
        public override void AddRecipes()
        {
            AlbumAnimalFirst.AddCopyRecipes(this, 3 + 4);
        }
    }
}
