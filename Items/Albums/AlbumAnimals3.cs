using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumAnimals3 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terrarian Critters, 3rd ed.");
            Tooltip.SetDefault("'It's chock full of cute animal photos!'"
                + AlbumAnimalFirst.Value2ToolTip(this, Item.sellPrice(0, 8, 0, 0)));
        }
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                Item.sellPrice(0, 8, 0, 0), 2, 2
                );
        }
        public override void AddRecipes()
        {
            AlbumAnimalFirst.AddCopyRecipes(this, 3 + 6 + 3);
        }
    }
}
