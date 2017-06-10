using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumSlimes2 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Know Your Slimes, 2nd ed.");
            Tooltip.SetDefault("'Full of random slime trivia!'"
                + AlbumAnimalFirst.Value2ToolTip(this, Item.sellPrice(0, 6, 0, 0)));
        }
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                Item.sellPrice(0, 6, 0, 0), 2, 5
                );
        }
        public override void AddRecipes()
        {
            AlbumAnimalFirst.AddCopyRecipes(this, 3 + 4);
        }
    }
}
