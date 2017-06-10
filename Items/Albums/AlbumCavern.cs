using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumCavern : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cavern Predators, 1st ed.");
            Tooltip.SetDefault("'It contains photos of cavernous beasts'"
                +AlbumAnimalFirst.Value2ToolTip(this, Item.sellPrice(0, 3, 0, 0)));
        }
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                Item.sellPrice(0, 3, 0, 0), 1, 26
                );
        }
        public override void AddRecipes()
        {
            AlbumAnimalFirst.AddCopyRecipes(this, 3);
        }
    }
}
