using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumWater : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aquatic Wildlife, 1st ed.");
            Tooltip.SetDefault("'It contains photos of colourful aquatic creatures'"
                + AlbumAnimalFirst.Value2ToolTip(this, Item.sellPrice(0, 3, 0, 0)));
        }
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                Item.sellPrice(0, 3, 0, 0), 1, 16
                );
        }
        public override void AddRecipes()
        {
            AlbumAnimalFirst.AddCopyRecipes(this, 6);
        }
    }
}
