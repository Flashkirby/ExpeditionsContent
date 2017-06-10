using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumRare : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rare Sights, 1st ed.");
            Tooltip.SetDefault("'It contains wonderful snapshots of rare creatures'"
                + AlbumAnimalFirst.Value2ToolTip(this, Item.sellPrice(0, 10, 0, 0)));
        }
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                Item.sellPrice(0, 10, 0, 0), 3, 11
                );
        }
        public override void AddRecipes()
        {
            AlbumAnimalFirst.AddCopyRecipes(this, 5);
        }
    }
}
