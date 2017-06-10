using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumCrimson2 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Disaster Report: Corruption, 2nd ed.");
            Tooltip.SetDefault("'It documents the horrors born of ancient spirits'"
                + AlbumAnimalFirst.Value2ToolTip(this, Item.sellPrice(0, 6, 0, 0)));
        }
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                Item.sellPrice(0, 6, 0, 0), 2, 35
                );
        }
        public override void AddRecipes()
        {
            AlbumAnimalFirst.AddCopyRecipes(this, 5 + 3);
        }
    }
}
