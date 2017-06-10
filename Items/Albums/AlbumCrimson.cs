using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumCrimson : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Disaster Report: Crimson, 1st ed.");
            Tooltip.SetDefault("'It contains gruesome pictures of horrifying wildlife'"
                + AlbumAnimalFirst.Value2ToolTip(this, Item.sellPrice(0, 6, 0, 0)));
        }
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                Item.sellPrice(0, 6, 0, 0), 2, 34
                );
        }
        public override void AddRecipes()
        {
            AlbumAnimalFirst.AddCopyRecipes(this, 5);
        }
    }
}
