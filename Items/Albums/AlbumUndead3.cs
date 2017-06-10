using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumUndead3 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("No Bones About It, 2nd ed.");
            Tooltip.SetDefault("'It discusses the hot topic of fashion trends amongst the undead'"
                + AlbumAnimalFirst.Value2ToolTip(this, Item.sellPrice(0, 8, 0, 0)));
        }
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                Item.sellPrice(0, 8, 0, 0), 2, 14
                );
        }
        public override void AddRecipes()
        {
            AlbumAnimalFirst.AddCopyRecipes(this, 9 + 9 + 3);
        }
    }
}
