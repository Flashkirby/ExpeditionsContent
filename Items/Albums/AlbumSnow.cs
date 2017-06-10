using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumSnow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Arctic Animals, 1st ed.");
            Tooltip.SetDefault("'It contains frosted photos of furry foes'"
                + AlbumAnimalFirst.Value2ToolTip(this, Item.sellPrice(0, 3, 0, 0)));
        }
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                Item.sellPrice(0, 3, 0, 0), 1, 24
                );
        }
        public override void AddRecipes()
        {
            AlbumAnimalFirst.AddCopyRecipes(this, 2);
        }
    }
}
