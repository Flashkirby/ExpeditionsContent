using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumDemons : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spirits and Demons, 1st ed.");
            Tooltip.SetDefault("'It contains worrying signs of dark forces'"
                + AlbumAnimalFirst.Value2ToolTip(this, Item.sellPrice(0, 3, 0, 0)));
        }
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                Item.sellPrice(0, 3, 0, 0), 1, 20
                );
        }
        public override void AddRecipes()
        {
            AlbumAnimalFirst.AddCopyRecipes(this, 6);
        }
    }
}
