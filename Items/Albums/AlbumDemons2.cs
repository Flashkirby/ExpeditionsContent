using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumDemons2 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spirits and Demons, 2nd ed.");
            Tooltip.SetDefault("'It contains terrible signs of evil forces'"
                + AlbumAnimalFirst.Value2ToolTip(this, Item.sellPrice(0, 6, 0, 0)));
        }
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                Item.sellPrice(0, 6, 0, 0), 2, 21
                );
        }
        public override void AddRecipes()
        {
            AlbumAnimalFirst.AddCopyRecipes(this, 6);
        }
    }
}
