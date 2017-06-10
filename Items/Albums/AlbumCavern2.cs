using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumCavern2 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cavern Predators, 2nd ed.");
            Tooltip.SetDefault("'It contains photos of ravenous beasts'"
                + AlbumAnimalFirst.Value2ToolTip(this, Item.sellPrice(0, 9, 0, 0)));
        }
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                Item.sellPrice(0, 9, 0, 0), 4, 27
                );
        }
        public override void AddRecipes()
        {
            AlbumAnimalFirst.AddCopyRecipes(this, 3 + 3);
        }
    }
}
