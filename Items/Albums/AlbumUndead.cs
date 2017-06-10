using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumUndead : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Risen from the Grave, 1st ed.");
            Tooltip.SetDefault("'It contains photos of zombies with rotten fashion sense'"
                + AlbumAnimalFirst.Value2ToolTip(this, Item.sellPrice(0, 3, 0, 0)));
        }
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                Item.sellPrice(0, 3, 0, 0), 1, 12
                );
        }
        public override void AddRecipes()
        {
            AlbumAnimalFirst.AddCopyRecipes(this, 9);
        }
    }
}
