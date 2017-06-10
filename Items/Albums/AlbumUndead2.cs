using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumUndead2 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("No Bones About It, 1st ed.");
            Tooltip.SetDefault("'The spine-tingling sequel to Risen from the Grave'"
                + AlbumAnimalFirst.Value2ToolTip(this, Item.sellPrice(0, 6, 0, 0)));
        }
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                Item.sellPrice(0, 6, 0, 0), 2, 13
                );
        }
        public override void AddRecipes()
        {
            AlbumAnimalFirst.AddCopyRecipes(this, 9 + 9);
        }
    }
}
