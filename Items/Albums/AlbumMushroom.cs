using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumMushroom : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dangers of Spore Infestation, 1st ed.");
            Tooltip.SetDefault("'It contains strange images of mushroom-like foes'"
                + AlbumAnimalFirst.Value2ToolTip(this, Item.sellPrice(0, 6, 0, 0)));
        }
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                Item.sellPrice(0, 6, 0, 0), 2, 28
                );
        }
        public override void AddRecipes()
        {
            AlbumAnimalFirst.AddCopyRecipes(this, 3);
        }
    }
}
