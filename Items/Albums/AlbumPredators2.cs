using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumPredators2 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terrarian Monster Almanac, 2nd ed.");
            Tooltip.SetDefault("'It holds information on surface and cavern monsters'"
                + AlbumAnimalFirst.Value2ToolTip(this, Item.sellPrice(0, 35, 0, 0)));
        }
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                Item.sellPrice(0, 35, 0, 0), 2, 9
                );
        }
    }
}
