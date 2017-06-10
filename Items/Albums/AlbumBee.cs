using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumBee : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Albeem, 1st ed.");
            Tooltip.SetDefault("'Buzzing with bee tales'"
                + AlbumAnimalFirst.Value2ToolTip(this, Item.sellPrice(0, 6, 0, 0)));
        }
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                Item.sellPrice(0, 6, 0, 0), 1, 19
                );
        }
        public override void AddRecipes()
        {
            AlbumAnimalFirst.AddCopyRecipes(this, 8);
        }
    }
}
