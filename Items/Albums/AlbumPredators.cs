using Terraria;
using Terraria.ModLoader;

namespace ExpeditionsContent.Items.Albums
{
    public class AlbumPredators : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terrarian Monster Almanac, 1st ed.");
            Tooltip.SetDefault("'It holds information about surface monsters'"
                + AlbumAnimalFirst.Value2ToolTip(this, Item.sellPrice(0, 15, 0, 0)));
        }
        public override void SetDefaults()
        {
            AlbumAnimalFirst.SetDefaultAlbum(this,
                Item.sellPrice(0, 15, 0, 0), 1, 8
                );
        }
    }
}
