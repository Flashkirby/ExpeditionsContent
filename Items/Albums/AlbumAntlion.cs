using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumAntlion : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Antlion Studies, 1st ed.");
            Tooltip.SetDefault("'Full of studies and diagrams about Antlions'"
                + AlbumAnimalFirst.Value2ToolTip(this, Item.sellPrice(0, 6, 0, 0)));
        }
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                Item.sellPrice(0, 6, 0, 0), 1, 17
                );
        }
        public override void AddRecipes()
        {
            AlbumAnimalFirst.AddCopyRecipes(this, 3);
        }
    }
}
